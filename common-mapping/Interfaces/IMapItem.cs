namespace common_mapping.Interfaces
{
    /// <summary>
    /// Item of mapping with two values based on linkId
    /// </summary>
    public interface IMapItem
    {
        public string LinkCode { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
    }
}
