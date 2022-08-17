namespace common_mapping.Interfaces
{
    /// <summary>
    /// Item of mapping with two values based on linkId
    /// </summary>
    public interface IMapItem
    {
        public int LinkId { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
    }
}
