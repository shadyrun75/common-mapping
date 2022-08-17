using common_mapping.Interfaces;
using System.Text.Json.Serialization;

namespace common_mapping.Models.API
{
    public class MapType : IMapType
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public MapType()
        {

        }

        public MapType(IMapType value)
        {
            Id = value.Id;
            Name = value.Name;
            Description = value.Description;
        }

        public override string ToString()
        {
            var temp = $"[{Id}] {Name}";
            if (Description?.Length > 0)
                temp += $" ({Description})";
            return temp;
        }
    }
}
