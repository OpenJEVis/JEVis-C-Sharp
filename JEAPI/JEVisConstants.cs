using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public interface JEVisConstants
    {

        interface PrimitiveType
        {

            const int STRING = 0;
            const int DOUBLE = 1;
            const int LONG = 2;
            const int FILE = 3;
            const int BOOLEAN = 4;
            const int SELECTION = 5;
            const int MULTI_SELECTION = 6;
            const int PASSWORD_PBKDF2 = 7;
        }

        interface DisplayType
        {

            const int TEXT_FIELD = 1;
            const int TEXT_PASSWORD = 2;
            const int NUMBER = 3;
            const int DATE = 3;
        }

        interface Direction
        {

            /**
             * From Object to target
             */
            const int FORWARD = 0;
            /**
             * From Target to object
             */
            const int BACKWARD = 1;
        }

        interface ObjectRelationship
        {

            /**
             * From child to parent
             */
            const int PARENT = 1;
            /**
             * From link to original
             */
            const int LINK = 2;
            /**
             * From group to root
             */
            const int ROOT = 3;
            /**
             * From object to source
             */
            const int SOURCE = 4;
            /**
             * From object to service
             */
            const int SERVICE = 5;
            /**
             * from object to input
             */
            const int INPUT = 6;
            /**
             * From object to data
             */
            const int DATA = 7;
            /**
             * from nested to parent
             */
            const int NESTED_CLASS = 8;

            /**
             * From object to group
             */
            const int OWNER = 100;
            /**
             * From user to group
             */
            const int MEMBER_READ = 101;
            /**
             * From user to group
             */
            const int MEMBER_WRITE = 102;
            /**
             * From user to group
             */
            const int MEMBER_EXECUTE = 103;
            /**
             * From user to group
             */
            const int MEMBER_CREATE = 104;
            /**
             * From user to group
             */
            const int MEMBER_DELETE = 105;
            /**
             * From role to user
             */
            const int ROLE_MEMBER = 200;
            /**
             * From role to group
             */
            const int ROLE_READ = 201;
            /**
             * From role to group
             */
            const int ROLE_WRITE = 202;
            /**
             * From role to group
             */
            const int ROLE_EXECUTE = 203;
            /**
             * From role to group
             */
            const int ROLE_CREATE = 204;
            /**
             * From role to group
             */
            const int ROLE_DELETE = 205;

        }

        interface ClassRelationship
        {

            /**
             * From subclass to superclass
             */
            const int INHERIT = 0;
            /**
             * From host to nested
             */
            const int NESTED = 1;//better name = integrated?
            /**
             * From class to possible parent
             */
            const int OK_PARENT = 3;
        }

        interface Class
        {

            const String USER = "User";
            const String GROUP = "Group";
        }

        interface Attribute
        {

            const String USER_EMAIL = "Email";
          const String USER_SYS_ADMIN = "Sys Admin";
            const String USER_PASSWORD = "Password";
            const String USER_ENABLED = "Enabled";
            const String USER_FIRST_NAME = "First Name";
            const String USER_LAST_NAME = "Last Name";

        }

        interface Validity
        {

            const int LAST = 0;
            const int AT_DATE = 1;
        }

    }
}
