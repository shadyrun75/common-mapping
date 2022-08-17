using common_mapping.Interfaces;

namespace common_mapping.Models
{
    public class MapLink : IMapLink
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int TargetId { get; set; }
        
        public override string ToString()
        {
            return $"[LinkId {Id}] {SourceId} => {TargetId}";
        }
    }
}
