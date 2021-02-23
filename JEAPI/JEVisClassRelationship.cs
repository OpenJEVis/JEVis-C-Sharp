using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisClassRelationship : JEVisComponent
    {

        /**
         * Returns the start JEVisClass of this relationship
         *
         * @return start JEVisCalss
         * @throws JEVisException
         */
        JEVisClass GetStart();

        String GetStartName();

        /**
         * Returns the end JEVisClass of this relationship
         *
         * @return end JEVisClass
         * @throws JEVisException
         */
        JEVisClass GetEnd();

        String GetEndName();

        /**
         * Returns the type of the relationship.
         *
         * @return the type
         * @throws JEVisException
         */
        int GetClassType();

        /**
         * Returns both JEVisClass
         *
         * @return both JEVisClass
         * @throws JEVisException
         */
        JEVisClass[]
        GetJEVisClasses();

        /**
         * Returns the other JEVIClass
         *
         * @param jclass
         * @return the other JEVIClass
         * @throws JEVisException
         */
        JEVisClass GetOtherClass(JEVisClass jclass);

        /**
         * Returns the name of the other Class in this relationship
         *
         * @param name
         * @return
         * @throws JEVisException
         */
        String GetOtherClassName(String name);

        /**
         * Check the type
         *
         * @param type The type to compare to
         * @return <CODE>true</CODE> if its the same type
         * @throws JEVisException
         */
        bool IsRelationshipType(int type);

        /**
         * return if this Relationship is inherited from another class
         *
         * @return CODE>true</CODE> if is inherited
         * @throws JEVisException
         */
        bool IsInherited();

    }
}
