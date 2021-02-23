using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonRelationship
    {

        private long From { get; set; }
        public long GetFrom() { return From; }
        public void setFrom(long from) { this.From = from; }
        private long To { get; set; }
        public long GetTo() { return To; }
        public void SetTo(long to) { this.To = to; }
        private int Type { get; set; }
        public new int GetType() { return Type; }
        public void SetType(int type) { this.Type = type; }

        public JsonRelationship()
        {
        }


        public String toString()
        {
            return "JsonRelationship{" +
                    "from=" + From +
                    ", to=" + To +
                    ", type=" + Type +
                    '}';
        }
    }
}
