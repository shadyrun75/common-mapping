using common_mapping.Interfaces;
using System.Text.Json.Serialization;

namespace common_mapping.Models.API
{
    public class MapItem : IMapItem
    {
        [JsonPropertyName("linkCode")]
        public string LinkCode { get; set; }
        [JsonPropertyName("sourceValue")]
        public string SourceValue { get; set; }
        [JsonPropertyName("targetValue")]
        public string TargetValue { get; set; }

        public MapItem()
        {

        }

        public MapItem(IMapItem value)
        {
            LinkCode = value.LinkCode;
            SourceValue = value.SourceValue;
            TargetValue = value.TargetValue;
        }

        public MapItem(string linkCode, string sourceValue, string targetValue = "")
        {
            (LinkCode, SourceValue, TargetValue) = (linkCode, sourceValue, targetValue);
        }

        public override string ToString()
        {
            return $"{LinkCode}: {SourceValue} => {TargetValue}";
        }
    }
}
