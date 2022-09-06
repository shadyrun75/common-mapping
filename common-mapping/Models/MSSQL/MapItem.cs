using common_mapping.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace common_mapping.Models.MSSQL
{
    internal class MapItem : IMapItem
    {
        public Int64 Id { get; set; } = 0;
        public string LinkCode { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }

        public MapItem()
        {

        }

        public MapItem(string linkCode, string sourceValue)
        {
            (LinkCode, SourceValue) = (linkCode, sourceValue);
        }

        public MapItem(IMapItem value)
        {
            LinkCode = value.LinkCode;
            SourceValue = value.SourceValue;
            TargetValue = value.TargetValue;
        }

        public MapItem(string linkCode, string sourceValue, string targetValue)
        {
            (LinkCode, SourceValue, TargetValue) = (linkCode, sourceValue, targetValue);
        }

        public override string ToString()
        {
            return $"{LinkCode}: {SourceValue} => {TargetValue}";
        }
    }
}
