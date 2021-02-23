using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonI18n
    {
        private String Key { get; set; }
        public String GetKey() { return Key; }
        public void SetKey(String key) { Key = key; }
        private String Value { get; set; }
        public String GetValue() { return Value; }
        public void SetValue(String value) { Value = value; }
        private String Lang { get; set; }
        public String GetLang() { return Lang; }
        public void SetLang(String lang) { Lang = lang; }
    }
}
