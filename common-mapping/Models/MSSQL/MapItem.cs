using common_mapping.Interfaces;

namespace common_mapping.Models.MSSQL
{
    internal class MapItem : IMapItem
    {
        public Int64 Id { get; set; }
        public int LinkId { get; set; }
        public virtual MapLink Link { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public MapItem()
        {

        }

        public MapItem(IMapItem value)
        {
            Id = 0;
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
