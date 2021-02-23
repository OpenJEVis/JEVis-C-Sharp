using JEAPI;
using JEAPI_WS.Json;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS
{
    public class JEVisSampleWS : JEVisSample
    {

        public static DateTimeFormatter sampleDTF = ISODateTimeFormat.dateTime();
        public static NumberFormat nf = NumberFormat.getInstance(Locale.US);
        private static ILog logger = LogManager.GetLogger(typeof(JEVisSampleWS));
        private JEVisAttribute attribute;
        private JsonSample json;
        private JEVisDataSourceWS ds;
        private JEVisFile file = null;
        private Object valueObj { get; set; }
        private DateTime tsObj = DateTime.MinValue;

        public JEVisSampleWS(JEVisDataSourceWS ds, JsonSample json, JEVisAttribute att)
        {
            this.attribute = att;
            this.ds = ds;
            this.json = json;
            ObjectifyValue();
            ObjectifyTimeStamp();
        }

        public JEVisSampleWS(JEVisDataSourceWS ds, JsonSample json, JEVisAttribute att, JEVisFile file)
        {
            this.attribute = att;
            this.ds = ds;
            this.json = json;
            this.file = file;
            try
            {
                SetValue(file.getFilename());
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }

        private void ObjectifyTimeStamp()
        {
            tsObj = sampleDTF.parseDateTime(json.GetTs());
        }

        private void ObjectifyValue()
        {
            try
            {
                valueObj = null;
                if (json.GetValue() != null)
                {
                    if (attribute.GetType().GetPrimitiveType() == JEVisConstants.PrimitiveType.DOUBLE)
                    {
                        valueObj = Double.Parse(json.GetValue());
                        return;
                    }
                    else if (attribute.GetType().GetPrimitiveType() == JEVisConstants.PrimitiveType.LONG)
                    {
                        valueObj = long.Parse(json.GetValue());
                        return;
                    }
                    else if (attribute.GetType().GetPrimitiveType() == JEVisConstants.PrimitiveType.BOOLEAN)
                    {
                        valueObj = GetValueAsBool();
                        return;
                    }
                }
                else
                {
                    valueObj = null;
                }

            }
            catch (Exception ex)
            {
                logger.Error(string.Format("Error while casting Attribute Type: '{0}' in {1}", PrettyError.getJEVisLineFilter(ex), json));
                valueObj = null;
            }
            valueObj = json.GetValue();

        }

        public DateTime GetTimestamp()
        {
            return tsObj;
        }

        public Object GetValue()
        {
            if (valueObj != null)
            {
                return valueObj;
            }

            //fallback to String
            return json.GetValue();
        }

        private bool ValidateValue(Object value)
        {
            try
            {
                if (GetAttribute().getPrimitiveType() == JEVisConstants.PrimitiveType.DOUBLE)
                {
                    Double.Parse(value.ToString());
                }
                else if (GetAttribute().getPrimitiveType() == JEVisConstants.PrimitiveType.long)
                {
                    long.Parse(value.ToString());
                }
            }
            catch (Exception ex)
            {
                //            throw new ClassCastException("Value object does not match the PrimitiveType of the Attribute: " + this.ToString());
                return false;
            }
            return true;
        }

        public void SetValue(Object value)
        {
            //logger.debug("setValue: {} Value: {}", getAttribute().getName(), value);
            try
            {
                //TODO validateValue(value)
                valueObj = value;
                if (value == null)
                {
                    json.SetValue("");
                }
                else
                {
                    json.SetValue(value.ToString());
                }


            }
            catch (Exception ex)
            {
                logger.Error("Value object does not match the PrimitiveType of the Attribute: " + this.ToString());
            }
        }

        public long GetValueAsLong()
        {
            try
            {
                if (typeof(valueObj) == typeof(long))
                    return long.Parse(valueObj);
                else return nf.parse(GetValueAsString()).longValue();
            }
            catch (Exception ex)
            {
                return 0l;
            }
        }


        public long GetValueAsLong(JEVisUnit unit)
        {
            double lValue = GetValueAsLong().doubleValue();
            Double dValue = GetUnit().convertTo(unit, lValue);
            return dValue.longValue();
        }


        public Double GetValueAsDouble()
        {
            if (typeof(valueObj) == typeof(Double)) 
                return Double.Parse(valueObj); 
            else return Double.Parse(GetValueAsString());
        }


        public Double GetValueAsDouble(JEVisUnit unit)
        {
            return GetUnit().convertTo(unit, GetValueAsDouble());
        }


        public bool GetValueAsBool()
        {
            if (typeof(valueObj) == typeof(bool))
            {
                return (bool)valueObj;
            }
            else
            {
                if (json.GetValue().Equals("1"))
                {
                    return true;
                }
                else if (json.GetValue().Equals("0"))
                {
                    return false;
                }

                return bool.Parse(GetValueAsString());
            }
        }


        public String GetValueAsString()
        {
            return json.GetValue();
        }


        public JEVisFile GetValueAsFile()
        {

            if (file != null && file.getBytes() != null)
            {
                return file;
            }
            else
            {
                try
                {
                    String resource = REQUEST.API_PATH_V1
                            + REQUEST.OBJECTS.PATH
                            + GetAttribute().GetObjectID() + "/"
                            + REQUEST.OBJECTS.ATTRIBUTES.PATH
                            + GetAttribute().GetName() + "/"
                            + REQUEST.OBJECTS.ATTRIBUTES.SAMPLES.PATH
                            + REQUEST.OBJECTS.ATTRIBUTES.SAMPLES.FILES.PATH
                            + HTTPConnection.FMT.print(GetTimestamp());

                    byte[] response = ds.GetHTTPConnection().getByteRequest(resource);

                    file = new JEVisFileImp(GetValueAsString(), response);
                    return file;

                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return null;
                }
            }

        }


        public JEVisSelection GetValueAsSelection()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return null;
        }


        public JEVisMultiSelection GetValueAsMultiSelection()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return null;
        }


        public void SetValue(Object value, JEVisUnit unit)
        {
            if (typeof(value) == typeof(Double)) {
                json.setValue(unit.convertTo(GetUnit(), Double.Parse(value.ToString())) + "");
            } else if (typeof(value) == typeof(long)) {
                json.setValue(unit.convertTo(GetUnit(), long.Parse(value.ToString())) + "");
            } else
            {
                logger.Error("ClassCastException");
            }

        }


        public JEVisAttribute GetAttribute()
        {
            return attribute;
        }


        public String GetNote()
        {
            if (json.GetNote() == null)
            {
                return "";
            }
            else
            {
                return json.GetNote();
            }
        }


        public void SetNote(String note)
        {
            json.SetNote(note);
        }


        public JEVisUnit GetUnit()
        {
            return GetAttribute().GetInputUnit();
        }


        public JEVisDataSource GetDataSource()
        {
            return ds;
        }


        public void Commit()
        {
            logger.Debug(string.Format("Commit: {0} {1}", GetTimestamp(), GetValueAsString()));
            List<JEVisSample> tmp = new List<JEVisSample>();
            tmp.Add(this);
            GetAttribute().AddSamples(tmp);
            //        ds.reloadAttribute(getAttribute());
            //        try {
            //            JEVisObjectWS obj = (JEVisObjectWS) getAttribute().getObject();
            //            obj.notifyListeners(new JEVisEvent(this, JEVisEvent.TYPE.ATTRIBUTE_UPDATE));
            //        } catch (Exception ex) {
            //            logger.error(ex);
            //        }
        }


        public void RollBack()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
        }


        public bool HasChanged()
        {
            logger.Error("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
            return false;
        }


        public String ToString()
        {
            return "JEVisSampleWS{ ts:" + GetTimestamp() + " Value: " + GetValueAsString() + "}";
        }
    }
}
