using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisClassRelationshipWS : JEVisClassRelationship
    {

        private JEVisDataSourceWS ds;
        private JsonClassRelationship json;
        private static readonly ILog logger = LogManager.GetLogger(typeof(JEVisClassRelationshipWS));

        public JEVisClassRelationshipWS(JEVisDataSourceWS ds, JsonClassRelationship json)
        {
            this.ds = ds;
            this.json = json;
        }

        public String GetStartName()
        {
            return json.GetStart();
        }

        public String GetEndName()
        {
            return json.GetEnd();
        }

        public String GetOtherClassName(String name)
        {
            if (json.GetStart().Equals(name))
            {
                return json.GetEnd();
            }
            else
            {
                return json.GetStart();
            }
        }

        public JEVisClass GetStart()
        {
            return ds.getJEVisClass(json.GetStart);
        }


        public JEVisClass GetEnd()
        {
            return ds.getJEVisClass(json.GetEnd);
        }


        public int GetClassType()
        {
            return json.GetClassType();
        }


        public JEVisClass[] GetJEVisClasses()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return null;
        }


        public JEVisClass GetOtherClass(JEVisClass jclass)
        {
            if (GetStart().Equals(jclass))
            {
                return GetEnd();
            }
            else
            {
                return GetStart();
            }
        }


        public bool IsRelationshipType(int type)
        {
            return type == GetClassType();
        }


        public bool IsInherited()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return false;
        }


        public JEVisDataSource GetDataSource()
        {
            return ds;
        }


        public String ToString()
        {
            return "JEVisClassRelationshipWS{" +
                    "from=" + json.GetStart() +
                    "  to=" + json.GetEnd() +
                    "  type=" + json.GetType() +
                    '}';
        }
    }
}
