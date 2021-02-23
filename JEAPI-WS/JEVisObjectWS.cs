using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisObjectWS : JEVisObject
    {
        private static ILog logger = LogManager.GetLogger(typeof(JEVisObjectWS));

        private EventListenerList listeners = new EventListenerList();
        private JEVisDataSourceWS ds { get; set; }
        private JsonObject json { get; set; }


        public JEVisObjectWS(JEVisDataSourceWS ds, JsonObject json)
        {
            this.ds = ds;
            this.json = json;
        }

        public void Update(JsonObject json)
        {
            this.json = json;
        }



        public void AddEventListener(JEVisEventListener listener)
        {
            this.listeners.add(listener);
        }


        public void RemoveEventListener(JEVisEventListener listener)
        {
            this.listeners.remove(listener);
        }


        public void NotifyListeners(JEVisEvent jEVisEvent)
        {
            logger.Debug(string.Format("Object event[{0}] listeners: {1} event:", GetId(), this.listeners.getListenerCount(), jEVisEvent.getType()));
            foreach (JEVisEventListener l in this.listeners.getListeners(JEVisEventListener))
            {
                l.fireEvent(jEVisEvent);
            }
        }


        public String GetName()
        {
            return GetLocalName(I18n.getInstance().getLocale().getLanguage());
        }


        public void SetName(String name)
        {
            this.json.SetName(name);
        }


        public String GetLocalName(String key)
        {
            if (key.Equals("default"))
            {
                return this.json.GetName();
            }

            if (json.GetI18n().Count != 0)
            {
                foreach (JsonI18n jsonI18n in json.GetI18n())
                {
                    if (jsonI18n.GetKey().Equals(key))
                    {
                        return jsonI18n.GetValue();
                    }
                }
            }

            return json.GetName();
        }


        public Dictionary<String, String> GetLocalNameList()
        {
            Dictionary<String, String> names = new Dictionary<string, string>();
            foreach (JsonI18n jsonI18n in json.GetI18n())
            {
                names.Add(jsonI18n.GetKey(), jsonI18n.GetValue());
            }

            return names;
        }


        public void SetLocalName(String key, String name)
        {

            if (json.GetI18n().Count != 0)
            {
                foreach (JsonI18n jsonI18n in json.GetI18n())
                {
                    if (jsonI18n.GetKey().Equals(key))
                    {
                        jsonI18n.SetValue(name);
                    }
                }
            }
            else
            {
                JsonI18n newI18n = new JsonI18n();
                newI18n.SetKey(key);
                newI18n.SetValue(name);

                json.GetI18n().Add(newI18n);
            }
        }


        public void SetLocalNames(Dictionary<String, String> translation)
        {
            json.GetI18n().Clear();

            foreach (KeyValuePair<string, string> entry in translation)
            {
                JsonI18n jsonI18n = new JsonI18n();
                jsonI18n.SetKey(entry.Key);
                jsonI18n.SetValue(entry.Value);
                json.GetI18n().Add(jsonI18n);
            }

        }


        public long GetId()
        {
            return this.json.GetId();
        }

        private bool IsLink()
        {
            return this.json.GetJEVisClass().Equals("Link");
        }


        public JEVisClass GetJEVisClass()
        {
            if (IsLink())
            {
                JEVisObject linkedObject = GetLinkedObject();
                if (linkedObject != null)
                {
                    return linkedObject.GetJEVisClass();
                }
            }
            return this.ds.GetJEVisClass(this.json.GetJEVisClass());
        }


        public List<JEVisObject> GetParents()
        {
            List<JEVisObject> parents = new List<JEVisObject>();
            try
            {
                foreach (JEVisRelationship rel in GetRelationships())
                {
                    if (rel.GetRelationshipType() == 1)
                    {
                        if (rel.GetStartObject().GetId().equals(GetId()))
                        {
                            if (rel.GetEndObject() != null && !parents.Contains(rel.GetEndObject()))
                            {
                                parents.Add(rel.GetEndObject());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(string.Format("Missing or unacceptable parent: {0}", GetId()));
            }

            return parents;

        }


        public String GetJEVisClassName()
        {
            return this.json.GetJEVisClass();
        }


        public List<JEVisObject> GetChildren()
        {
            List<JEVisObject> children = new List<JEVisObject>();
            try
            {
                foreach (JEVisRelationship rel in GetRelationships())
                {
                    try
                    {
                        long id = rel.GetEndID();
                        if ((rel.GetRelationshipType() == JEVisConstants.ObjectRelationship.PARENT) && (id.Equals(GetId())))
                        {
                            if (rel.GetStartObject() != null && !children.Contains(rel.GetStartObject()))
                            {
                                children.Add(rel.GetStartObject());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("Could not get all children", ex);
            }
            logger.Debug(string.Format("Child.size: {0}.{1}", GetId(), children.Count));
            return children;
        }

        /**
         * TODO: this seems to not work properly needs testing
         *
         * @param jclass
         * @param inherit Include inherited classes
         * @return
         * @throws JEVisException
         */

        public List<JEVisObject> GetChildren(JEVisClass jclass, bool inherit)
        {
            List<JEVisObject> filterList = new List<JEVisObject>();
            foreach (JEVisObject obj in GetChildren())
            {
                try
                {

                    JEVisClass oClass = obj.GetJEVisClass();
                    if (oClass != null && oClass.Equals(jclass))
                    {
                        filterList.Add(obj);
                    }
                    else if (oClass != null)
                    {
                        HashSet<JEVisClass> inheritanceClasses = GetInheritanceClasses(new HashSet<JEVisClass>(), oClass);
                        foreach (JEVisClass curClass in inheritanceClasses)
                        {
                            if (curClass.Equals(jclass))
                            {
                                filterList.Add(obj);
                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            logger.Debug(string.Format("[{0}] getChildren: \n{1}\n", GetId(), filterList));

            return filterList;
        }


        public List<JEVisAttribute> GetAttributes()
        {
            if (IsLink())
            {
                JEVisObject linkedObject = GetLinkedObject();
                if (linkedObject != null)
                {
                    return this.ds.GetAttributes(linkedObject.SetId());
                }
            }

            return this.ds.GetAttributes(GetId());
        }

        //    public List<JEVisAttribute> getAttributesWS() {
        //        return this.ds.getAttributes(getID());
        //    }


        public JEVisAttribute GetAttribute(JEVisAttributeType jevisType)
        {
            //TODO not optimal, getAttribute() will not cached if we call all this in a loop we do N Webserive calls
            foreach (JEVisAttribute att in GetAttributes())
            {
                if (att.GetName().Equals(jevisType.GetName()))
                {
                    return att;
                }
            }
            return null;
        }


        public JEVisAttribute GetAttribute(String jevisType)
        {
            if (jevisType == null)
            {
                return null;
            }

            foreach (JEVisAttribute att in GetAttributes())
            {
                if (att.GetName().Equals(jevisType))
                {
                    return att;
                }
            }

            return null;
        }


        public bool Delete()
        {

            return this.ds.DeleteObject(GetId());
        }



        public JEVisObject BuildObject(String name, JEVisClass jevisClass)
        {
            logger.Debug(string.Format("buildObject: {0} {1}", name, jevisClass.GetName()));
            JsonObject newJson = new JsonObject();
            newJson.SetName(name);
            newJson.SetJEVisClass(jevisClass.GetName());
            newJson.SetParent(GetId());


            return new JEVisObjectWS(this.ds, newJson);
        }


        public JEVisObject GetLinkedObject()
        {
            try
            {
                foreach (JEVisRelationship relationship in GetRelationships(JEVisConstants.ObjectRelationship.LINK, JEVisConstants.Direction.FORWARD))
                {
                    try
                    {
                        JEVisObject originalObj = relationship.GetEndObject();
                        if (originalObj != null)
                        {
                            return originalObj;
                        }
                        /** TODO: maybe return some spezial object of the user has not access to the otherObject **/
                    }
                    catch (Exception e)
                    {
                        logger.Error(e);
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
            }

            return null;
        }


        public JEVisRelationship BuildRelationship(JEVisObject otherObj, int type, int direction)
        {
            JEVisRelationship rel;
            if (direction == JEVisConstants.Direction.FORWARD)
            {
                rel = this.ds.BuildRelationship(GetId(), otherObj.SetId(), type);

                if (type == JEVisConstants.ObjectRelationship.PARENT)
                {
                    otherObj.NotifyListeners(new JEVisEvent(rel.GetEndObject(), JEVisEvent.TYPE.OBJECT_NEW_CHILD, rel.GetStartObject()));
                }

            }
            else
            {
                rel = otherObj.BuildRelationship(this, type, JEVisConstants.Direction.FORWARD);
            }


            return rel;
        }


        public void DeleteRelationship(JEVisRelationship rel)
        {
            this.ds.DeleteRelationship(rel.GetStartID(), rel.GetEndID(), rel.GetRelationshipType());

            /**
             * Delete form cache and other objects
             */
            if (rel.GetRelationshipType() == JEVisConstants.ObjectRelationship.PARENT)
            {
                rel.GetEndObject().NotifyListeners(new JEVisEvent(rel.GetEndObject(), JEVisEvent.TYPE.OBJECT_CHILD_DELETED, rel.GetStartObject().getID()));

            }

            NotifyListeners(new JEVisEvent(this, JEVisEvent.TYPE.OBJECT_UPDATED, this));
        }


        public List<JEVisRelationship> GetRelationships()
        {
            return this.ds.GetRelationships(GetId());
        }


        public List<JEVisRelationship> GetRelationships(int type)
        {
            List<JEVisRelationship> filter = new List<JEVisRelationship>();
            foreach (JEVisRelationship rel in GetRelationships())
            {
                if (rel.IsRelationshipType(type))
                {
                    filter.Add(rel);
                }
            }

            return filter;
        }


        public List<JEVisRelationship> GetRelationships(int type, int direction)
        {
            List<JEVisRelationship> filter = new List<JEVisRelationship>();
            foreach (JEVisRelationship rel in GetRelationships())
            {
                if (rel.IsRelationshipType(type))
                {
                    if (rel.GetStartObject().Equals(this) && direction == JEVisConstants.Direction.FORWARD)
                    {
                        filter.Add(rel);
                    }
                    else if (rel.GetEndObject().Equals(this) && direction == JEVisConstants.Direction.BACKWARD)
                    {
                        filter.Add(rel);
                    }
                }
            }

            return filter;
        }


        public List<JEVisClass> GetAllowedChildrenClasses()
        {
            List<JEVisClass> allowedChildren = new List<JEVisClass>();
            foreach (JEVisClass vp in GetJEVisClass().GetValidChildren())
            {
                if (vp.IsUnique())
                {
                    if (GetChildren(vp, false).Count == 0)
                    {
                        allowedChildren.Add(vp);
                    }
                }
                else
                {
                    allowedChildren.Add(vp);
                }
            }

            return allowedChildren;
        }


        public bool IsAllowedUnder(JEVisObject otherObject)
        {
            bool classIsAllowedUnderClass = GetJEVisClass().IsAllowedUnder(otherObject.GetJEVisClass());
            bool isDirectory = this.ds.GetJEVisClass("Directory").getHeirs().contains(this.GetJEVisClass());
            bool isUnique = GetJEVisClass().IsUnique();
            bool isAlreadyUnderParent = false;
            if (otherObject.GetParents() != null)
            {
                foreach (JEVisObject child in otherObject.GetChildren())
                {
                    try
                    {
                        if (child.GetJEVisClassName().Equals(GetJEVisClassName()))
                        {
                            isAlreadyUnderParent = true;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            /**
             * first check if its allowed to be created under other object class
             */
            if (classIsAllowedUnderClass)
            {
                if (!isUnique)
                {
                    /**
                     * if its not unique its always allowed to be created
                     */
                    return true;
                }
                else /**
             *  if it is a directory and its of the same class of its parent its allowed to be created
             */if (!isAlreadyUnderParent)
                {
                    /**
                     * if it is unique and not already created its allowed to be created
                     */
                    return true;
                }
                else return isDirectory && otherObject.GetJEVisClassName().Equals(GetJEVisClassName());
            }

            return false;
        }


        public JEVisDataSource GetDataSource()
        {
            return this.ds;
        }


        public void Commit()
        {
            try
            {
                Console.WriteLine("Object.commit()");
                //            Gson gson = new Gson();
                //            logger.trace("Commit: {}", gson.toJson(this.json));
                //            Benchmark benchmark = new Benchmark();
                String resource = REQUEST.API_PATH_V1
                        + REQUEST.OBJECTS.PATH;

                bool update = false;

                if (this.json.GetId() > 0)
                {//update existing
                    resource += GetId();
                    //                resource += REQUEST.OBJECTS.OPTIONS.INCLUDE_RELATIONSHIPS;
                    //                resource += "true";
                    update = true;
                }

                //            StringBuffer response = ;
                //            //TODO: remove the relationship from the post json, like in the Webservice JSonFactory

                JsonObject newJson = this.ds.getObjectMapper().readValue(this.ds.getHTTPConnection().postRequest(resource, this.ds.getObjectMapper().writeValueAsString(this.json)).toString(), JsonObject.GetType());
                //            JsonObject newJson = gson.fromJson(response.toString(), JsonObject.class);
                logger.Debug("commit object ID: {} public: {}", newJson.getId(), newJson.getisPublic());
                this.json = newJson;
                //            benchmark.printBenchmarkDetail("After ws call");
                //            this.ds.reloadRelationships();
                this.ds.reloadRelationships(this.json.getId());
                //            benchmark.printBenchmarkDetail("After reloadRel");
                /** reload object to be sure all events will be handled and the cache is working correctly **/
                this.ds.addToObjectCache(this);
                if (update)
                {
                    NotifyListeners(new JEVisEvent(this, JEVisEvent.TYPE.OBJECT_UPDATED, this));
                }
                else
                {
                    this.ds.reloadAttribute(this);
                    if (!GetParents().isEmpty())
                    {
                        try
                        {
                            GetParents().get(0).notifyListeners(new JEVisEvent(this, JEVisEvent.TYPE.OBJECT_NEW_CHILD, this));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                        }
                    }
                }


                //            benchmark.printBenchmarkDetail("done commit");
            }
            catch (JsonParseException ex)
            {
                throw new JEVisException("Json parse exception. Could not commit to server", 8236341, ex);
            }
            catch (JsonMappingException ex)
            {
                throw new JEVisException("Json mapping exception. Could not commit to server", 8236342, ex);
            }
            catch (JsonProcessingException ex)
            {
                throw new JEVisException("Json processing exception. Could not commit to server", 8236343, ex);
            }
            catch (IOException ex)
            {
                throw new JEVisException("IO exception. Could not commit to server", 8236344, ex);
            }
            catch (Exception ex)
            {
                throw new JEVisException("Could not commit to server", 8236348, ex);
            }

        }


        public void RollBack()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }


        public bool HasChanged()
        {
            //TODO hasChanged
            return true;
        }


        public int CompareTo(JEVisObject o)
        {

            return GetId().CompareTo(o.SetId());
        }


        public bool Equals(Object o)
        {
            try
            {
                if (o instanceof JEVisObject) {
                    return ((JEVisObject)o).SetId().equals(getID());
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        private HashSet<JEVisClass> GetInheritanceClasses(HashSet<JEVisClass> hashSet, JEVisClass obj)
        {
            try
            {
                JEVisClass inheritance = obj.GetInheritance();
                if (inheritance == null)
                {
                    return hashSet;
                }
                else
                {
                    hashSet.Add(inheritance);
                    return GetInheritanceClasses(hashSet, inheritance);
                }
            }
            catch (JEVisException ex)
            {
                logger.Error(ex);
            }
            return hashSet;
        }


        public bool IsPublic()
        {
            return this.json.IsPublic();
        }


        public void IsPublic(bool ispublic)
        {
            this.json.IsPublic(ispublic);
        }



        override public String ToString()
        {
            return "JEVisObjectWS [ id: '" + getID() + "' name: '" + getName() + "' jclass: '" + getJEVisClassName() + "']";
        }

        public long SetId()
        {
            throw new NotImplementedException();
        }
    }
}

