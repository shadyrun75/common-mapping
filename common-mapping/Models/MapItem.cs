using common_mapping.Interfaces;

namespace common_mapping.Models
{
    public class MapItem : IMapItem
    {
        public int LinkId { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public MapItem()
        {

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
