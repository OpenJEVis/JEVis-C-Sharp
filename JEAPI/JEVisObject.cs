using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisObject : JEVisComponent, JEVisCommittable, IComparable<JEVisObject>
    {

        /**
         * Returns the name of the JEVisObject entity as String based on the set local.
         * Return default name if no translation for the local exist.
         *
         * Names are not unique in the JEVis system. For a unique identifier use the
         * ID.
         *
         * @return Name as String
         */
        String GetName();

        /**
         * Returns the name of the JEVisObject for the given ISO 639 local code.
         * <pre>
         * new Locale("he").getLanguage()
         *    ...
         * </pre>
         *
         * @param key
         * @return
         */
        String GetLocalName(String key);

        /**
         * Set the local name of the JEVisObject. The key is local name as ISO 639.
         *
         * <pre>
         * new Locale("he").getLanguage()
         *    ...
         * </pre>
         *
         * @param key
         * @param name
         */
        void SetLocalName(String key, String name);

        void SetLocalNames(Dictionary<String, String> translation);

        /** returns a Mit with all localissations for the name. The key is the  ISO 639 language name.
         * <pre>
         * new Locale("he").getLanguage()
         *    ...
         * </pre>
         *
         * @return
         */
        Dictionary<String, String> GetLocalNameList();


        /**
         * Set the name of the JEVisObject.
         *
         * @param name
         * @throws org.jevis.api.JEVisException
         */
        void SetName(String name);

        /**
         * Returns the unique identifier of this JEVisObject entity. The same ID
         * cannot appear twice on the same JEVis server. Its possible and very
         * likely that the same ID appear on different JEVis server.
         *
         * The ID cannot be set by the client and will be given from the server.
         *
         * @return identifier as Long
         */
        long SetId();

        /**
         * Returns the JEVis Object Type of this JEVisObject entity. Every
         * JEVisObject is from a certain Type which describes the object attributes
         * and its handling.
         *
         * There can be unlimited JEVisObject entities from one JEVisObjectType.
         *
         * @return JEVisCalss
         * @throws org.jevis.api.JEVisException
         */
        JEVisClass GetJEVisClass();

        /**
         * Returns the JEVis Class name.
         *
         * @return
         * @throws JEVisException
         */
        String GetJEVisClassName();

        /**
         * Returns hierarchy parent of this JEVisObject entity.
         *
         * The JEVisObject is stored in tree like structure where every JEVisObject
         * can have an unlimited amount of JEVisObject-children but just one
         * JEVisObject-parent.
         *
         * @return Parent as JEVisObject
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisObject> GetParents();

        /**
         * Set the parent JEVisObject.
         *
         * @param object parent as JEVisObject
         */
        //    void setParent(JEVisObject object);
        /**
         * move this JEVisObject to an other parent
         */
        //    void moveTo(JEVisObject newParent);
        /**
         * Returns all hierarchy children as a list of JEVisObject. The List will be
         * empty if this JEobject has no children.
         *
         * @return List of all JEVisObject children
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisObject> GetChildren();

        /**
         * Returns all children as a List of JEVisObject from the certain given
         * JEVisObjectType or all JEVisObjectTypes which inherit the type.
         *
         * @param type Requested type as JEVisObjectType
         * @param inherit Include inherited classes
         * @return List of all accessible JEVisObject from the same Type or inherit
         * type.
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisObject> GetChildren(JEVisClass type, bool inherit);

        /**
         * Returns a List of all JEVisAttributes of this JEVisObject. If a
         * JEVisAttribute is not set the default value will be returned.
         *
         * @return List of JEVisAttributes which this JEVisObject has.
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisAttribute> GetAttributes();

        /**
         * Returns an specific JEVisAttribute which is of the given JEVisType. If
         * the JEVisAttribute is not set the default value will be returned.
         *
         * Will return an Exception if the JEAttributeType is not allowed unter the
         * JEVisObjectType
         *
         * @param type
         * @return JEAttribute from the given JEAttributeType
         * @throws org.jevis.api.JEVisException
         */
        JEVisAttribute GetAttribute(JEVisAttributeType type);

        /**
         * Returns a specific JEVisAttribute. If the JEVisAttribute is not set the
         * default value will be returned.
         *
         * Will return an Exception if the JEVisType is not allowed under the
         * JEVisObjectType
         *
         * @param type
         * @return JEVisAttribute from the given JEVisAttributeType
         * @throws org.jevis.api.JEVisException
         */
        JEVisAttribute GetAttribute(String type);

        /**
         * Delete this JEVisObject on the JEVis Server. This JEVisObject will be set
         * to null and will be removed from the child list of the parent.
         *
         * If this JEVisObject is a link it will only delete the link an not the
         * linked reference.
         *
         * All linked references will also be deleted.
         *
         * @return true if the delete was successful
         * @throws org.jevis.api.JEVisException
         */
        bool Delete();

        /**
         * Build n new JEVisObject from the given JEVisObjectType and name under
         * this JEVisObject.
         *
         * Throws Exception if the JEVisObjectType is not allowed under this
         * JEVisObject.
         *
         * @param name of the new created JEVisObject
         * @param type JEVisObjectType of the new created JEVisObject
         * @return new created JEVisObject
         * @throws org.jevis.api.JEVisException
         */
        JEVisObject BuildObject(String name, JEVisClass type);

        /**
         * Get the JEVisObject this JEVisObject points to
         *
         * @return Linked JEVisOBject or null if its not linked
         * @throws JEVisException
         */
        JEVisObject GetLinkedObject();

        /**
         * Create and commit a new Relationship
         *
         * @param obj
         * @param type {@link org.jevis.jeapi.JEVisConstants.Relationship}
         * @param direction {@link org.jevis.jeapi.JEVisConstants.Direction}
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisRelationship BuildRelationship(JEVisObject obj, int type, int direction);

        /**
         * Delete a relationship for this object
         *
         * @param rel
         * @throws JEVisException
         */
        void DeleteRelationship(JEVisRelationship rel);

        /**
         * Return all relationships this object has
         *
         * @return
         * @throws JEVisException
         */
        List<JEVisRelationship> GetRelationships();

        /**
         * Return all relationships from the given type
         *
         * @param type
         * @return
         * @throws JEVisException
         */
        List<JEVisRelationship> GetRelationships(int type);

        /**
         * Return all relationships from the given type
         *
         * @param type {@link org.jevis.jeapi.JEVisConstants.Relationship}
         * @param direction if Forward the the Class has to be the start, if
         * Backward the class has to be the end
         * @return
         * @throws JEVisException
         */
        List<JEVisRelationship> GetRelationships(int type, int direction);

        /**
         * Return a list of all JEVisClasses who are allowed under this JEVisbject.
         *
         * @return
         * @throws JEVisException
         */
        List<JEVisClass> GetAllowedChildrenClasses();

        /**
         * Check if this object is allowed under the given object.
         *
         * @param otherObject
         * @return true if the object is allowed under the other object
         *
         * @throws JEVisException
         */
        bool IsAllowedUnder(JEVisObject otherObject);

        /**
         * Returns true if this Object can be read by everyone. Public Objects will
         * be used for System wide configuration.
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool IsPublic();

        /**
         * Set if this Object can ne read by everyone.
         *
         * @param ispublic
         * @throws org.jevis.api.JEVisException
         */
        void IsPublic(bool ispublic);

        void AddEventListener(JEVisEventListener listener);
        void RemoveEventListener(JEVisEventListener listener);

        /**
         *
         * @param event
         */
        public void NotifyListeners(JEVisEvent jEVisEvent);


    }
}