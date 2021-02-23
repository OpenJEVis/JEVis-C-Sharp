using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace JEAPI
{
    public interface JEVisClass : JEVisComponent, JEVisCommittable, IComparable<JEVisClass>
    {

        /**
         * Get the name of this JEVisClass. Every class-name is unique.
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        String GetName();

        /**
         * Set The name of this JEVisClass. The class-name has to be unique.
         *
         * @param name
         * @throws org.jevis.api.JEVisException
         */
        void SetName(String name);

        /**
         * Get the Icon representing this JEVisClass.
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        Bitmap GetIcon();

        /**
         * Set the Icon representing this JEVisClass
         *
         * @param icon
         * @throws org.jevis.api.JEVisException
         */
        void SetIcon(Bitmap icon);

        /**
         * Set the Icon representing this JEVisClass
         *
         * @param icon
         * @throws org.jevis.api.JEVisException
         */
        void SetIcon(File icon);

        /**
         * Get the description for this JEVisClass. The description is a help text
         * for the end user.
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        String GetDescription();

        /**
         * Set the description for this JEVisClass.
         *
         * @param discription
         * @throws org.jevis.api.JEVisException
         */
        void SetDescription(String discription);

        /**
         * Get all types which are present for this JEVisClass
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisAttributeType> GetAttributeTypes();

        /**
         * Get a specific type by its unique name.
         *
         * @param typename
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisAttributeType GetAttributeType(String typename);

        /**
         * Build and add a new type under this JEVisClass. Every type has to be
         * unique under a JEVisClass.
         *
         * @param name
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisAttributeType BuildType(String name);

        /**
         * Get the inheritance class. This JEVisClass will inherit all types from
         * the parent class. If the JEVIsClass does not have an inheritance it will
         * return NULL
         *
         * @return Inheritance JEVisClass, null if it does not have an inheritance
         * @throws org.jevis.api.JEVisException
         */
        JEVisClass GetInheritance();

        /**
         * Get all heir classes.
         *
         * @return List of the heirs of this JEVisClass
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisClass> GetHeirs();

        /**
         * Get the list of all parents this class is allowed under
         *
         * @return List of valid parents, empty list if none exists
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisClass> GetValidParents();

        /**
         * Get the list of all classes where an object from this class can be
         * created under.
         *
         * @return
         * @throws JEVisException
         */
        List<JEVisClass> GetValidChildren();

        /**
         * Check if this JEVisClass is allowed under the given JEVisClass
         *
         * @param jevisClass
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool IsAllowedUnder(JEVisClass jevisClass);

        /**
         * Check if this JEVisClass has to be unique under one JEVisObject. This
         * function allows to control the structure of the JEVis tree
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool IsUnique();

        /**
         * Set if the JEVisClass is unique under another JEVisObject
         *
         * @param unique
         * @throws org.jevis.api.JEVisException
         */
        void IsUnique(bool unique);

        /**
         * Delete this JEVisClass.
         *
         * @deprecated Use JEVisDataSource.deleteClass
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool Delete();

        /**
         * Delete an JEVisType of this class
         *
         * @param type
         * @return
         * @throws org.jevis.api.JEVisException
         */
        bool DeleteType(String type);

        /**
         * Return all relationships this class has
         *
         * @return A list of relationships
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisClassRelationship> GetRelationships();

        /**
         * Return all relationships from the given type
         *
         * @param type
         * @return all relationships from the given type
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisClassRelationship> GetRelationships(int type);

        /**
         * Return all relationships from the given type and direction
         *
         * @param type {@link org.jevis.jeapi.JEVisConstants.Relationship}
         * @param direction if Forward this class has to be the start, if Backward
         * the class has to be the end
         * @return all relationships from the given type and direction
         * @throws org.jevis.api.JEVisException
         */
        List<JEVisClassRelationship> GetRelationships(int type, int direction);

        /**
         * Create and commit relationship to another JEVisClass
         *
         * @param jclass
         * @param type {@link org.jevis.jeapi.JEVisConstants.Relationship}
         * @param direction {@link org.jevis.jeapi.JEVisConstants.Direction}
         * @return the new relationship
         * @throws org.jevis.api.JEVisException
         */
        JEVisClassRelationship BuildRelationship(JEVisClass jclass, int type, int direction);

        /**
         * Delete a relationship for this class
         *
         * @param rel
         * @throws JEVisException
         */
        void DeleteRelationship(JEVisClassRelationship rel);

        void AddEventListener(JEVisEventListener listener);
        void RemoveEventListener(JEVisEventListener listener);

        /**
         *
         * @param event
         */
        public void NotifyListeners(JEVisEvent jEVisEvent);
    }
}

