using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisRelationshipWS : JEVisRelationship
    {

        private JEVisObject startObj = null;
        private JEVisObject endObject = null;
        private int type;
        private JEVisDataSourceWS ds;
        private static ILog logger = LogManager.GetLogger(typeof(JEVisRelationshipWS));
        private JsonRelationship json;

        public JEVisRelationshipWS(JEVisDataSourceWS ds, JsonRelationship json)
        {
            //        logger.trace("New Relationship: {}->{}", json.getFrom(), json.getTo());
            this.ds = ds;
            this.json = json;

            type = json.GetType();

        }

        public JEVisObject GetStartObject()
        {
            return ds.GetObject(json.GetFrom());
        }

        public JEVisObject GetEndObject()
        {
            return ds.GetObject(json.GetTo());
        }

        public JEVisObject[] GetObjects()
        {
            return new JEVisObject[] { GetStartObject(), GetEndObject() };
        }

        public JEVisObject GetOtherObject(JEVisObject jEVisObject)
        {
            if (jEVisObject.GetID() == GetStartID())
            {
                return GetEndObject();
            }
            else
            {
                return GetStartObject();
            }
        }

        public int GetRelationshipType()
        {
            return type;
        }

        public bool IsRelationshipType(int type)
        {
            return (this.type == type);
        }

        public long GetStartID()
        {
            return json.GetFrom();
        }

        public long GetEndID()
        {
            return json.GetTo();
        }

        public void Delete()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }

        public string ToString()
        {
            return "JEVisRelationshipWS{" + "startobj=" + json.GetFrom() + ", endobj=" + json.GetTo() + ", type=" + json.GetType() + '}';
        }

    }
}
