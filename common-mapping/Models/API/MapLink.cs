using common_mapping.Interfaces;
using System.Text.Json.Serialization;

namespace common_mapping.Models.API
{
    public class MapLink : IMapLink
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("sourceId")]
        public int SourceId { get; set; }
        [JsonPropertyName("targetId")]
        public int TargetId { get; set; }

        public MapLink()
        {

        }

        public MapLink(IMapLink value)
        {
            Id = value.Id;
            SourceId = value.SourceId;
            TargetId = value.TargetId;
        }

        public override string ToString()
        {
            return $"[LinkId {Id}] {SourceId} => {TargetId}";
        }
    }
}
