using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisClassWS : JEVisClass
    {

        private static ILog logger = LogManager.GetLogger(typeof(JEVisClassWS));
        private EventListenerList listeners = new EventListenerList();
        //    private String name = "";
        private JEVisDataSourceWS ds = null;
        private List<JEVisAttributeType> types = null;
        //    private String description = "";
        //    private bool isUnique = false;
        private Image image = null;
        private JsonJEVisClass json;
        private List<JEVisClassRelationship> relations = new List<JEVisClassRelationship>();
        private bool iconChanged = false;

        public JEVisClassWS(JEVisDataSourceWS ds, JsonJEVisClass json)
        {
            this.ds = ds;
            this.json = json;
        }

        /**
         * TMP solution
         * <p>
         * TODO: remove, does not belong here
         */
        public static Image GetImage(String icon, double height, double width)
        {
            ImageView image = new ImageView(GetImage(icon));
            image.fitHeightProperty().set(height);
            image.fitWidthProperty().set(width);
            return image;
        }

        /**
         * TMP solution
         * <p>
         * TODO: remove, does not belong here
         */
        public static Image GetImage(String icon)
        {
            try
            {
                return new Image(JEVisClassWS.getResourceAsStream("/" + icon));
            }
            catch (Exception ex)
            {
                logger.info("Could not load icon: /icons/{}", icon);
                return new Image(JEVisClassWS.getResourceAsStream("/icons/1393355905_image-missing.png"));
            }
        }

        public void AddEventListener(JEVisEventListener listener)
        {
            listeners.add(JEVisEventListener, listener);
        }

        public void RemoveEventListener(JEVisEventListener listener)
        {
            listeners.remove(JEVisEventListener, listener);
        }

        public void NotifyListeners(JEVisEvent jEVisEvent)
        {

            for (JEVisEventListener l : listeners.getListeners(JEVisEventListener))
            {
                l.fireEvent(jEVisEvent);
            }
        }


        public bool DeleteType(String type)
        {
            //TODO re-implement
            return false;
        }

        public String GetName()
        {
            return this.json.GetName();
        }

        public void SetName(String name)
        {
            this.json.SetName(name);
        }


        public BufferedImage GetIcon()
        {
            if (image == null)
            {
                image = ds.getClassIcon(json.getName());
            }

            if (image == null)
            {
                image = SwingFXUtils.fromFXImage(JEVisClassWS.getImage("1472562626_unknown.png", 60, 60).getImage(), null);
                iconChanged = true;
            }
            return image;

        }


        public void SetIcon(File icon)
        {
            try
            {
                this.image = ImageIO.read(icon);
                iconChanged = true;
                //            logger.info("set icon from file: " + _icon.getWidth());
            }
            catch (IOException ex)
            {
                logger.catching(ex);
            }
        }


        public void SetIcon(BufferedImage icon)
        {
            this.image = icon;
            iconChanged = true;

        }


        public String GetDescription()
        {
            return json.GetDescription();
        }


        public void SetDescription(String description)
        {
            json.SetDescription(description);
        }


        public List<JEVisAttributeType> GetAttributeTypes()
        {

            if (types == null && json.GetAttributeTypes() != null)
            {
                types = new List<JEVisAttributeType>();
                foreach (JsonAttributeType t in json.GetAttributeTypes())
                {
                    types.Add(new JEVisTypeWS(ds, t, GetName()));
                }

                types = types.OrderBy(o => o.GetGUIPosition()).ToList();
            }
            if (types == null)
            {
                types = new List<JEVisAttributeType>();
            }

            return types;
        }


        public JEVisAttributeType GetAttributeType(String typename)
        {

            foreach (JEVisAttributeType type in GetAttributeTypes())
            {
                if (type.getJEVisClassName().Equals(typename))
                {
                    return type;
                }
            }

            return null;

        }


        public JEVisAttributeType BuildType(String name)
        {
            JEVisAttributeType newType = new JEVisTypeWS(ds, name, GetName());
            GetAttributeTypes().Add(newType);//not save, waht will happen if the user does not commit() the type
            return newType;

        }


        public JEVisClass GetInheritance()
        {
            foreach (JEVisClassRelationship crel in GetRelationships())
            {
                try
                {
                    if (crel.IsRelationshipType(JEVisConstants.ClassRelationship.INHERIT) && crel.GetStart().GetName().Equals(GetName()))
                    {
                        return crel.GetEnd();
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(string.Format("Error in relationship {0}", crel, ex));
                }
            }
            return null;
        }


        public List<JEVisClass> GetHeirs()
        {
            List<JEVisClass> heirs = new List<JEVisClass>();
            foreach (JEVisClassRelationship cr in GetRelationships(JEVisConstants.ClassRelationship.INHERIT, JEVisConstants.Direction.BACKWARD))
            {
                try
                {
                    heirs.Add(cr.GetStart());
                    heirs.AddRange(cr.GetStart().GetHeirs());
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
            return heirs;
        }


        public List<JEVisClass> GetValidParents()
        {
            List<JEVisClass> validParents = new List<JEVisClass>();

            if (GetInheritance() != null)
            {
                validParents.AddRange(GetInheritance().GetValidParents());
            }

            foreach (JEVisClassRelationship rel in GetRelationships())
            {
                try
                {
                    if (rel.IsRelationshipType(JEVisConstants.ClassRelationship.OK_PARENT)
                            && rel.GetStart().Equals(this))
                    {
                        if (!validParents.Contains(rel.GetOtherClass(this)))
                        {
                            validParents.Add(rel.GetOtherClass(this));
                        }
                        validParents.AddRange(rel.GetOtherClass(this).getHeirs());

                    }
                }
                catch (Exception ex)
                {

                }
            }


            validParents.Sort();

            return validParents;
        }


        public List<JEVisClass> GetValidChildren()
        {
            List<JEVisClass> validParents = new List<>();
            foreach (JEVisClassRelationship rel in GetRelationships())
            {
                try
                {
                    if (rel.IsRelationshipType(JEVisConstants.ClassRelationship.OK_PARENT)
                            && rel.GetEnd().Equals(this))
                    {
                        if (!validParents.Contains(rel.GetOtherClass(this)))
                        {
                            if (!validParents.Contains(rel.GetOtherClass(this)))
                            {
                                validParents.Add(rel.GetOtherClass(this));
                            }
                            //We do not want heirs, every class has added by rule to have more control
                            //validParents.addAll(rel.getOtherClass(this).getHeirs());
                        }

                    }


                }
                catch (Exception ex)
                {
                    logger.Error(string.Format("An JEClassRelationship had an error for '{0}': {1}", GetName(), ex));
                }
            }
            //Special rule, for order purpose its allows to create on directory under him self.
            if (ds.getJEVisClass("Directory").getHeirs().contains(this) && !validParents.Contains(this))
            {
                validParents.Add(this);
            }


            validParents.Sort();
            return validParents;
        }


        public bool IsAllowedUnder(JEVisClass jevisClass)
        {
            List<JEVisClass> valid = GetValidParents();
            foreach (JEVisClass pClass in valid)
            {
                if (pClass.GetName().Equals(jevisClass.GetName()))
                {
                    return true;
                }
            }
            return false;
        }


        public bool IsUnique()
        {
            return json.getUnique();
        }


        public void IsUnique(bool unique)
        {
            json.setUnique(unique);
        }


        public bool Delete()
        {
            return ds.deleteClass(GetName());
        }


        public List<JEVisClassRelationship> GetRelationships()
        {
            if (relations.Count == 0 && json.GetRelationships() != null)
            {
                foreach (JsonClassRelationship crel in json.GetRelationships())
                {
                    relations.Add(new JEVisClassRelationshipWS(ds, crel));
                }
            }

            if (relations == null)
            {
                relations = new List<JEVisClassRelationship>();
            }

            return relations;
        }


        public List<JEVisClassRelationship> GetRelationships(int type)
        {
            List<JEVisClassRelationship> tmp = new List<JEVisClassRelationship>();

            foreach (JEVisClassRelationship cr in GetRelationships())
            {
                if (cr.IsRelationshipType(type))
                {
                    tmp.Add(cr);
                }
            }

            return tmp;
        }


        public List<JEVisClassRelationship> GetRelationships(int type, int direction)
        {
            List<JEVisClassRelationship> tmp = new List<JEVisClassRelationship>();

            foreach (JEVisClassRelationship cr in GetRelationships(type))
            {
                if (direction == JEVisConstants.Direction.FORWARD && cr.GetStart().equals(this))
                {
                    tmp.Add(cr);
                }
                else if (direction == JEVisConstants.Direction.BACKWARD && cr.GetEnd().equals(this))
                {
                    tmp.Add(cr);
                }
            }

            return tmp;
        }


        public JEVisClassRelationship BuildRelationship(JEVisClass jclass, int type, int direction)
        {
            JEVisClassRelationship rel;
            if (direction == JEVisConstants.Direction.FORWARD)
            {//this to otherClass
                rel = ds.buildClassRelationship(this.GetName(), jclass.GetName(), type);
            }
            else
            {
                rel = ds.buildClassRelationship(jclass.GetName(), this.GetName(), type);
            }

            return rel;
        }


        public void DeleteRelationship(JEVisClassRelationship rel)
        {
            ds.deleteClassRelationship(rel.GetStartName(), rel.GetEndName(), rel.GetClassType());

        }


        public JEVisDataSource GetDataSource()
        {
            return ds;
        }


        private void CommitIconToWS()
        {
            try
            {
                logger.Info("post icon");
                ByteArrayOutputStream baos = new ByteArrayOutputStream();
                ImageIO.write(GetIcon(), "png", baos);
                baos.flush();
                byte[] imageInByte = baos.toByteArray();
                baos.close();

                String resource = REQUEST.API_PATH_V1
                        + REQUEST.CLASSES.PATH
                        + GetName() + "/"
                        + REQUEST.CLASSES.ICON.PATH;

                HttpURLConnection connection = ds.getHTTPConnection().getPostIconConnection(resource);


                OutputStream os = connection.getOutputStream();

                os.write(imageInByte);
                os.flush();
                os.close();

                //
                int responseCode = connection.getResponseCode();
                logger.Error("commit icon: {}", responseCode);



                catch (Exception ex)
            {
                logger.Error(ex);
            }
        }


        public void Commit()
        {
            try
            {

                String resource = REQUEST.API_PATH_V1
                        + REQUEST.CLASSES.PATH
                        + GetName();

                //            Gson gson = new Gson();

                StringBuffer response = ds.getHTTPConnection().postRequest(resource, this.ds.getObjectMapper().writeValueAsString(json));

                this.json = this.ds.getObjectMapper().readValue(response.ToString(), JsonJEVisClass);

                if (iconChanged)
                {
                    CommitIconToWS();
                }

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }


            ds.reloadClasses();
        }





        public bool HasChanged()
        {
            //TODO: class compare
            return false;
        }


        public int CompareTo(JEVisClass o)
        {
            try
            {
                return GetName().CompareTo(o.GetName());
            }
            catch (JEVisException ex)
            {
                return 1;
            }
        }


        override public bool Equals(Object o)
        {
            try
            {
                /**
                 * cast needs to be removed
                 */

                if (o.GetType() is JEVisClass)
                {
                    JEVisClass obj = (JEVisClass)o;
                    if (obj.GetName().Equals(GetName()))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info("error, cannot compare objects");
                return false;
            }
            return false;
        }

        void JEVisCommittable.RollBack()
        {
            throw new NotImplementedException();
        }
    }
}



