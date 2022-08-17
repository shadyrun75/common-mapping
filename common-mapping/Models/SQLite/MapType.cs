using common_mapping.Interfaces;

namespace common_mapping.Models.SQLite
{
    internal class MapType : IMapType
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
