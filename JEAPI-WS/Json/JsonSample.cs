using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonSample
    {

        private String Ts { get; set; }
        public String GetTs() { return Ts; }
        public void SetTs(String ts) { Ts = ts; }
        private String Value { get; set; }
        public String GetValue() { return Value; }
        public void SetValue(String value) { Value = value; }
        private String Note { get; set; }
        public String GetNote() { return Note; }
        public void SetNote(String note) { Note = note; }

        public JsonSample()
        {
        }

    }
}
