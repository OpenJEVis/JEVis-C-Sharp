using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisDataSource
    {

        /**
         * Initialize the datasource with the given configuration, will overwrite
         * the existing configuration
         *
         * @param config
         * @throws IllegalArgumentException
         */
        void Init(List<JEVisOption> config);

        /**
         * Returns the configuration in use.
         *
         * @return
         */
        List<JEVisOption> GetConfiguration();

        /**
         * Replace the current configuration
         *
         * @param config
         */
        void SetConfiguration(List<JEVisOption> config);

        /**
         * Build a JEVisClass with the given name.
         *
         * @param name of the class. The class has to be unique on the system.
         * @return JEVisClass
         * @throws JEVisException
         */
        JEVisClass buildClass(String name);

        /**
         * Create a symbolic link to a JEVisObject
         *
         * @param name         Name of the new created link.
         * @param parent       Parent JEVisObject under which the new link will be created
         * @param linkedObject Target JEVisObject where the link links to
         * @return
         * @throws JEVisException
         */
        JEVisObject buildLink(String name, JEVisObject parent, JEVisObject linkedObject);

        /**
         * Creates an ClassRelationship between tweo objects.
         *
         * @param fromClass
         * @param toClass
         * @param type
         * @return
         * @throws JEVisException
         */
        JEVisClassRelationship buildClassRelationship(String fromClass, String toClass, int type);

        /**
         * Creates an Relationship between two classes.
         *
         * @param fromObject
         * @param toObject
         * @param type
         * @return
         * @throws JEVisException
         */
        JEVisRelationship buildRelationship(long fromObject, long toObject, int type);

        /**
         * Get all root objects for the current user.
         *
         * @return List of all root-JEVisObjects
         * @throws JEVisException
         */
        List<JEVisObject> getRootObjects();

        /**
         * Get all JEVisObjects from a JEVisClass
         *
         * @param jevisClass
         * @param addheirs   if true all heirs of the JEVisClass will also be included
         * @return
         * @throws JEVisException
         */
        List<JEVisObject> getObjects(JEVisClass jevisClass, bool addheirs);

        /**
         * Get a JEVisObject by its unique ID
         *
         * @param id
         * @return
         * @throws JEVisException
         */
        JEVisObject getObject(long id);

        List<JEVisObject> getObjects();

        /**
         * Get a JEVisClass by its name
         *
         * @param name Name of the JEVisClass
         * @return
         * @throws JEVisException
         */
        JEVisClass getJEVisClass(String name);

        /**
         * Return all JEVisCalsses on the System
         *
         * @return
         * @throws JEVisException
         */
        List<JEVisClass> getJEVisClasses();

        /**
         * Return the user currently logged in.
         *
         * @return
         * @throws JEVisException
         */
        JEVisUser getCurrentUser();

        /**
         * Returns all relationships of a certain type.
         *
         * @param type type of the relationship
         * @return
         * @throws JEVisException
         * @see JEVisConstants
         */
        List<JEVisRelationship> getRelationships(int type);

        /**
         * Returns all relationships for and Object ID
         *
         * @param objectID
         * @return
         * @throws JEVisException
         */
        List<JEVisRelationship> getRelationships(long objectID);

        List<JEVisRelationship> getRelationships();

        List<JEVisClassRelationship> getClassRelationships();

        List<JEVisClassRelationship> getClassRelationships(String jclass);

        void getAttributes();

        /**
         * Connect to the DataSource as a JEVis user
         *
         * @param username
         * @param password
         * @return
         * @throws JEVisException
         */
        bool connect(String username, String password);

        /**
         * Close the DataSource connection
         *
         * @return
         * @throws JEVisException
         */
        bool disconnect();

        /**
         * Try to reconnect with the same user if the connection was lost
         *
         * @return
         * @throws JEVisException
         */
        bool reconnect();

        /**
         * Return the JEAPI implementation informations like name & version number
         *
         * @return
         */
        JEVisInfo getInfo();

        /**
         * Check if the connection is still alive returns true if the connection is
         * still alive
         *
         * @return
         * @throws JEVisException
         */
        bool isConnectionAlive();

        /**
         * Returns a list of all additional JEVisUnits installed on this system. The
         * static default list of the most common units can be found in the project
         * JECommons.
         *
         * @return list of all additional JEVisUnits
         */
        List<JEVisUnit> getUnits();

        /**
         * Returns all Attributes for an Object
         *
         * @param objectID
         * @return
         * @throws JEVisException
         */
        List<JEVisAttribute> getAttributes(long objectID);

        /**
         * Returns all Types for an JEVisClass
         *
         * @param className
         * @return
         * @throws JEVisException
         */
        List<JEVisAttributeType> getTypes(String className);

        /**
         * Delete an Objects
         *
         * @param objectID
         * @return
         */
        bool deleteObject(long objectID);

        /**
         * Delete an JEVisClass
         *
         * @param jclass
         * @return
         * @throws JEVisException
         */
        bool deleteClass(String jclass);

        /**
         * Delete an Object Relationship
         *
         * @param fromObject
         * @param toObject
         * @param type
         * @return
         * @throws JEVisException
         */
        bool deleteRelationship(long fromObject, long toObject, int type);

        /**
         * Delete an Class Relationship
         *
         * @param fromClass
         * @param toClass
         * @param type
         * @return
         * @throws JEVisException
         */
        bool deleteClassRelationship(String fromClass, String toClass, int type);

        /**
         * Request the datasource to preload some data. For example the mostly
         * static JEVisClasses, JEVisTypes
         *
         * @throws JEVisException
         */
        void preload();

        /**
         * Request the reloading of all Attributes.
         *
         * @return
         * @throws JEVisException
         */
        void reloadAttributes();

        /**
         * request the reload of an attribute
         *
         * @throws JEVisException
         */
        void reloadAttribute(JEVisAttribute attribute);

        /**
         * Request to reload all attributes for the object
         *
         * @param object
         */
        void reloadAttribute(JEVisObject jevisObject);

        void reloadObject(JEVisObject jevisObject);

        /**
         * Clears the internal cache
         */
        void clearCache();
    }
}
