using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonJEVisClass
    {
        private String Name { get; set; }
        public String GetName() { return Name; }
        public void SetName(String name) { Name = name; }
        private String Description { get; set; }
        public String GetDescription() { return Description; }
        public void SetDescription(String description) { Description = description; }
        private bool Unique { get; set; }
        public bool IsUnique() { return Unique; }
        public void IsUnique(bool isUnique) { Unique = isUnique; }
        private List<JsonClassRelationship> Relationships { get; set; }
        public List<JsonClassRelationship> GetRelationships() { return Relationships; }
        public void SetRelationships(List<JsonClassRelationship> relationships) { Relationships = relationships; }
        private List<JsonAttributeType> AttributeTypes { get; set; }
        public List<JsonAttributeType> GetAttributeTypes() { return AttributeTypes; }
        public void SetAttributeTypes(List<JsonAttributeType> attributeTypes) { AttributeTypes = attributeTypes; }
    }
}
