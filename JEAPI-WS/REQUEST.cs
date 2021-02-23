using System;

namespace JEAPI_WS
{
    public interface REQUEST
    {

        static String API_PATH_V1 = "JEWebService/v1/";


        interface ATTRIBUTES
        {
            static String PATH = "attributes/";
        }

        interface OBJECTS
        {

            static String PATH = "objects/";

            interface OPTIONS
            {

                static String INCLUDE_RELATIONSHIPS = "rel=";
                static String ONLY_ROOT = "root=";
                static String INCLUDE_CHILDREN = "includeChildren=";
            }

            interface ATTRIBUTES
            {

                static String PATH = "attributes/";

                interface OPTIONS
                {

                    static String INCLUDE_RELATIONSHIPS = "rel=true";
                }

                interface SAMPLES
                {

                    static String PATH = "samples/";

                    interface OPTIONS
                    {

                        static String FROM = "from=";
                        static String UNTIL = "until=";
                        static String LATEST = "onlyLatest=";
                        static String customWorkDay = "cwd=";
                        static String aggregationPeriod = "ap=";
                        static String manipulationMode = "mm=";
                        static String LIMIT = "limit";
                    }

                    interface FILES
                    {

                        static String PATH = "files/";

                        interface OPTIONS
                        {

                            static String FILENAME = "filename=";
                            static String TIMESTAMP = "timestamp=";
                        }
                    }

                }

            }
        }

        interface CLASS_ICONS
        {

            static String PATH = "classicons/";
        }

        interface CLASSES
        {

            static String PATH = "classes/";

            interface OPTIONS
            {

                static String INCLUDE_RELATIONSHIPS = "rel=true";
            }

            interface ICON
            {

                static String PATH = "icon/";
            }


        }


        interface RELATIONSHIPS
        {

            static String PATH = "relationships/";

            interface OPTIONS
            {

                static String INCLUDE_RELATIONSHIPS = "rel=true";
                static String FROM = "from=";
                static String TO = "to=";
                static String TYPE = "type=";
            }
        }
        //
        //    public interface CLASS_RELATIONSHIPS {
        //
        //        public static String PATH = "classrelationships/";
        //
        //        public interface OPTIONS {
        //
        //            public static String INCLUDE_RELATIONSHIPS = "rel=true";
        //            public static String FROM = "from=";
        //            public static String TO = "to=";
        //            public static String TYPE = "type=";
        //        }
        //
        //    }

        interface JEVISUSER
        {

            static String PATH = "user/";

            interface OPTIONS
            {

                static String INCLUDE_RELATIONSHIPS = "rel=true";
            }
        }

    }
}
