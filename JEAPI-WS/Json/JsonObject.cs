using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonObject
    {
        private String Name { get; set; }
        public String GetName() { return Name; }
        public void SetName(String name) { Name = name; }
        private long Id { get; set; }
        public long GetId() { return Id; }
        public void SetId(long id) { Id = id; }
        private String JEVisClass { get; set; }
        public String GetJEVisClass() { return JEVisClass; }
        public void SetJEVisClass(String jevisClass) { JEVisClass = jevisClass; }
        private long Parent { get; set; }
        public long GetParent() { return Parent; }
        public void SetParent(long parent) { Parent = parent; }
        private List<JsonRelationship> Relationships { get; set; }
        public List<JsonRelationship> GetRelationships() { return Relationships; }
        public void SetRelationShips(List<JsonRelationship> relationships) { Relationships = relationships; }
        private List<JsonObject> Objects { get; set; }
        public List<JsonObject> GetObjects() { return Objects; }
        public void SetObjects(List<JsonObject> objects) { Objects = objects; }
        private List<JsonAttribute> Attributes { get; set; }
        public List<JsonAttribute> GetAttributes() { return Attributes; }
        public void SetAttributes(List<JsonAttribute> attributes) { Attributes = attributes; }

        private bool Public { get; set; }
        public bool IsPublic() { return Public; }
        public void IsPublic(bool isPublic) { Public = isPublic; }

        private List<JsonI18n> I18n { get; set; }
        public List<JsonI18n> GetI18n() { return I18n; }
        public void SetI18n(List<JsonI18n> i18n) { I18n = i18n; }
    }
}
