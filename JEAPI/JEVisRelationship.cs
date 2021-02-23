using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisRelationship
    {

        /**
         * Returns the start node of the relationship.
         *
         * @return Start node of the relationship
         * @throws org.jevis.api.JEVisException
         */
        JEVisObject GetStartObject();

        /**
         * Returns the ID of the start Object. This function is resource saveing
         * against the getStartObject() because it has not have to initialize the
         * JEVIsObject
         *
         * @return
         */
        long GetStartID();

        /**
         * returns the ID of the end Obejct. This function is resource saveing
         * against the getStartObject() because it has not have to initialize the
         * JEVIsObject
         *
         * @return
         */
        long GetEndID();

        /**
         * Returns the end node of the relationship.
         *
         * @return the end node of the relationship
         * @throws org.jevis.api.JEVisException
         */
        JEVisObject GetEndObject();

        /**
         * Returns both Objects of this relationship
         *
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisObject[] GetObjects();

        /**
         * Returns the other Object of this relationship.
         *
         * @param object the other JEVisObject
         * @return
         * @throws org.jevis.api.JEVisException
         */
        JEVisObject GetOtherObject(JEVisObject jEVisObject);

        /**
         * Returns the type of this relationship
         *
         * @throws org.jevis.api.JEVisException
         * @see org.jevis.jeapi.JEVisConstants.Relationship
         * @return Type of this relationship
         */
        int GetRelationshipType();

        /**
         * Checks if this relationship is from the given type
         *
         * @throws org.jevis.api.JEVisException
         * @see org.jevis.jeapi.JEVisConstants.Relationship
         * @param type
         * @return
         */
        bool IsRelationshipType(int relationshipType);

        /**
         * Delete this relationship
         */
        void Delete();
    }
}
