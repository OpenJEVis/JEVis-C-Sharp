using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisTypeWS : JEVisAttributeType
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(JEVisTypeWS));
        private String jclass = null;
        private JEVisDataSourceWS ds;
        private JsonAttributeType json;

        public JEVisTypeWS(JEVisDataSourceWS ds, JsonAttributeType json, String jclass)
        {
            this.jclass = jclass;
            this.ds = ds;
            this.json = json;
        }

        public JEVisTypeWS(JEVisDataSourceWS ds, String name, String jclass)
        {
            this.json = new JsonAttributeType();
            this.json.SetName(name);
            this.jclass = jclass;
            this.ds = ds;
        }


        public String GetName()
        {
            return json.GetName();
        }


        public void SetName(String name)
        {
            json.SetName(name);
        }


        public int GetPrimitiveType()
        {
            return json.GetPrimitiveType();
        }


        public void SetPrimitiveType(int type)
        {
            json.SetPrimitiveType(type);
        }


        public String GetGUIDisplayType()
        {
            return json.GetGuiType();
        }


        public void SetGUIDisplayType(String type)
        {
            json.SetGuiType(type);
        }


        public int GetGUIPosition()
        {
            return json.GetGuiPosition();
        }


        public void SetGUIPosition(int pos)
        {
            json.SetGUIPosition(pos);
        }


        public JEVisClass GetJEVisClass()
        {
            return ds.getJEVisClass(jclass);
        }


        public int GetValidity()
        {
            return json.GetValidity();
        }


        public void SetValidity(int validity)
        {
            json.SetValidity(validity);
        }


        public String GetConfigurationValue()
        {
            return "";
        }


        public void SetConfigurationValue(String value)
        {
        }


        public JEVisUnit GetUnit()
        {
            return new JEVisUnitImp(Unit.ONE);
        }


        public void SetUnit(JEVisUnit unit)
        {
        }


        public String GetAlternativeSymbol()
        {
            return "";
        }


        public void SetAlternativeSymbol(String symbol)
        {
        }


        public String GetDescription()
        {
            return json.GetDescription();
        }


        public void SetDescription(String description)
        {
            json.SetDescription(description);
        }


        public bool Delete()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return false;
        }


        public JEVisDataSource GetDataSource()
        {
            return ds;
        }


        public void Commit()
        {
            logger.Error("Not supported yet.");
        }


        public void RollBack()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }


        public bool HasChanged()
        {
            //TODO: has Changed
            return false;
        }


        public int CompareTo(JEVisAttributeType o
        )
        {
            try
            {
                return GetName().CompareTo(o.GetName());
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        public String getJEVisClassName()
        {
            return jclass;
        }


        public bool IsInherited()
        {
            return json.IsInherited();
        }


        public void IsInherited(bool inherited)
        {
            json.IsInherited(inherited);
        }


        public String ToString()
        {
            return "JEVisTypeWS{" +
                    "jclass='" + jclass + '\'' +
                    ", json=" + json +
                    '}';
        }


        public bool equals(Object obj
        )
        {
            try
            {
                if (obj == null)
                {
                    return false;
                }
                if (GetType() != obj.GetType())
                {
                    return false;
                }
                /**
                 * cast needs to be removed
                 */
                JEVisAttributeType other = (JEVisAttributeType)obj;
                if ((this.GetName() == null) ? (other.GetName() != null) : !this.GetName().Equals(other.GetName()))
                {
                    return false;
                }
                return this.GetJEVisClass() == other.GetJEVisClass() || (this.GetJEVisClass() != null && this.GetJEVisClass().Equals(other.GetJEVisClass()));
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
