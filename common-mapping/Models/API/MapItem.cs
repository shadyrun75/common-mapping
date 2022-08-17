using common_mapping.Interfaces;
using System.Text.Json.Serialization;

namespace common_mapping.Models.API
{
    public class MapItem : IMapItem
    {
        [JsonPropertyName("linkId")]
        public int LinkId { get; set; }
        [JsonPropertyName("sourceValue")]
        public string SourceValue { get; set; }
        [JsonPropertyName("targetValue")]
        public string TargetValue { get; set; }

        public MapItem()
        {

        }

        public MapItem(IMapItem value)
        {
            LinkId = value.LinkId;
            SourceValue = value.SourceValue;
            TargetValue = value.TargetValue;
        }

        public MapItem(int linkId, string sourceValue, string targetValue = "")
        {
            (LinkId, SourceValue, TargetValue) = (linkId, sourceValue, targetValue);
        }

        public override string ToString()
        {
            return $"{LinkId}: {SourceValue} => {TargetValue}";
        }
    }
}
