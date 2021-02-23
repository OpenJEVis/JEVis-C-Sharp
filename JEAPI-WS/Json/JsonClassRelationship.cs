using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonClassRelationship
    {

        private String Start { get; set; }
        public String GetStart() { return Start; }
        public void SetStart(String start) { Start = start; }
        private String End { get; set; }
        public String GetEnd() { return End; }
        public void setEnd(String end) { End = end; }
        private int ClassType { get; set; }
        public int GetClassType() { return ClassType; }
        public void SetClassType(int type) { ClassType = type; }

        public JsonClassRelationship()
        {
        }

        override public bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            JsonClassRelationship that = (JsonClassRelationship)o;

            if (GetClassType() != that.GetClassType()) return false;
            if (!GetStart().Equals(that.GetStart())) return false;
            return GetEnd().Equals(that.GetEnd());
        }

        override public int GetHashCode()
        {
            int result = GetStart().GetHashCode();
            result = 31 * result + GetEnd().GetHashCode();
            result = 31 * result + GetClassType();
            return result;
        }
    }
}
