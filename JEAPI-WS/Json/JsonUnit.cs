using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonUnit
    {
        private String Formula { get; set; }
        public String GetFormula() { return Formula; }
        public void SetFormula(String formula) { Formula = formula; }
        private String Label { get; set; }
        public String GetLabel() { return Label; }
        public void SetLabel(String label) { Label = label; }
        private String Prefix { get; set; }
        public String GetPrefix() { return Prefix; }
        public void SetPrefix(String prefix) { Prefix = prefix; }
    }
}
