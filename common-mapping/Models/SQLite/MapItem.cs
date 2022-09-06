using common_mapping.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace common_mapping.Models.SQLite
{
    internal class MapItem : IMapItem
    {
        public Int64 Id { get; set; } = 0;
        [NotMapped]
        public string LinkCode { get; set; }
        public Int32 LinkId { get; set; } = 0;
        public virtual MapLink Link { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public MapItem()
        {

        }

        public MapItem(string linkId, string sourceValue)
        {
            (LinkCode, SourceValue) = (linkId, sourceValue);
        }

        public MapItem(IMapItem value, Int32 linkId)
        {
            LinkCode = value.LinkCode;
            SourceValue = value.SourceValue;
            TargetValue = value.TargetValue;
            LinkId = linkId;
        }

        public MapItem(string linkCode, string sourceValue, string targetValue, Int32 linkId)
        {
            (LinkCode, SourceValue, TargetValue, LinkId) = (linkCode, sourceValue, targetValue, linkId);
        }

        public override string ToString()
        {
            return $"{Link.Code}: {SourceValue} => {TargetValue}";
        }
    }
}
