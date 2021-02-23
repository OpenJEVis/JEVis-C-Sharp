using System;
using System.Collections.Generic;
using System.Text;

namespace JEAPI_WS.Json
{
    public class JsonAttribute
    {
        private String AttributeType { get; set; }
        public String GetAttributeType() { return AttributeType; }
        public void SetAttributeType(String attributeType) { AttributeType = attributeType; }
        private String Begins { get; set; }
        public String GetBegins() { return Begins; }
        public void SetBegins(String begins) { Begins = begins; }
        private String Ends { get; set; }
        public String GetEnds() { return Ends; }
        public void SetEnds(String ends) { Ends = ends; }
        private JsonUnit InputUnit { get; set; }
        public JsonUnit GetInputUnit() { return InputUnit; }
        public void SetInputUnit(JsonUnit inputUnit) { InputUnit = inputUnit; }
        private JsonUnit DisplayUnit { get; set; }
        public JsonUnit GetDisplayUnit() { return DisplayUnit; }
        public void SetDisplayUnit(JsonUnit displayUnit) { DisplayUnit = displayUnit; }
        private long SampleCount { get; set; }
        public long GetSampleCount() { return SampleCount; }
        public void SetSampleCount(long sampleCount) { SampleCount = sampleCount; }
        private JsonSample LatestValue { get; set; }
        public JsonSample GetLatestValue() { return LatestValue; }
        public void SetLatestValue(JsonSample latestValue) { LatestValue = latestValue; }
        private String InputSampleRate { get; set; }
        public String GetInputSampleRate() { return InputSampleRate; }
        public void SetInputSampleRate(String inputSampleRate) { InputSampleRate = inputSampleRate; }
        private String DisplaySampleRate { get; set; }
        public String GetDisplaySampleRate() { return DisplaySampleRate; }
        public void SetDisplaySampleRate(String displaySampleRate) { DisplaySampleRate = displaySampleRate; }
        private int PrimitiveType { get; set; }
        public int GetPrimitiveType() { return PrimitiveType; }
        public void SetPrimitiveType(int primitiveType) { PrimitiveType = primitiveType; }
        private long ObjectId { get; set; }
        public long GetObjectId() { return ObjectId; }
        public void SetObjectId(long objectId) { ObjectId = objectId; }
    }
}
