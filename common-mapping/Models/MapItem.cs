using common_mapping.Interfaces;

namespace common_mapping.Models
{
    public class MapItem : IMapItem
    {
        public string LinkCode { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public MapItem()
        {

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
