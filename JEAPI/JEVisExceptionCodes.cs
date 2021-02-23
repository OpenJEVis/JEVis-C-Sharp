using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI
{
    public class JEVisExceptionCodes
    {
        //------ Range 1000-1999 Datasource problems -----//
        public static int DATASOURCE_FAILD = 1000;
        public static int DATASOURCE_FAILD_MYSQL = 1001;
        //------ Range 2000-2999 User rights problems -----//
        public static int UNAUTHORIZED = 2000;
        //------ Range 3000-3999 parameter problems -----//
        public static int EMPTY_PARAMETER = 3000;
    }
}
