using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonAttributeType
    {

        private String Name { get; set; }
        public String GetName() { return Name; }
        public void SetName(String name) { Name = name; }
        private int PrimitiveType { get; set; }
        public int GetPrimitiveType() { return PrimitiveType; }
        public void SetPrimitiveType(int primitiveType) { PrimitiveType = primitiveType; }
        private String GuiType { get; set; }
        public String GetGuiType() { return GuiType; }
        public void SetGuiType(String guiType) { GuiType = guiType; }
        private int GUIPosition { get; set; }
        public int GetGuiPosition() { return GUIPosition; }
        public void SetGUIPosition(int guiPosition) { GUIPosition = guiPosition; }
        private int Validity { get; set; }
        public int GetValidity() { return Validity; }
        public void SetValidity(int validity) { Validity = validity; }
        private String Description { get; set; }
        public String GetDescription() { return Description; }
        public void SetDescription(String description) { Description = description; }
        private bool Inherited { get; set; }
        public bool IsInherited() { return Inherited; }
        public void IsInherited(bool inherited) { Inherited = inherited; }
        private String JevisClass { get; set; }
        public String GetJEVisClass() { return JevisClass; }
        public void SetJevisClass(String jevisClass) { JevisClass = jevisClass; }

        public JsonAttributeType()
        {
        }

    }
}
