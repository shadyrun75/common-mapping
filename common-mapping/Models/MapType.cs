using common_mapping.Interfaces;

namespace common_mapping.Models
{
    public class MapType : IMapType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            var temp = $"[{Id}] {Name}";
            if (Description?.Length > 0)
                temp += $" ({Description})";
            return temp;
        }
    }
}
