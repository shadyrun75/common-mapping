using common_mapping.Interfaces;

namespace common_mapping.Models.MSSQL
{
    internal class MapLink : IMapLink
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        public virtual MapType? Source { get; set; }
        public virtual MapType? Target { get; set; }

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
