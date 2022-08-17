namespace common_mapping.Interfaces
{
    /// <summary>
    /// Model for link two types of mapping 
    /// </summary>
    public interface IMapLink
    {
        public int Id { get; set; }
        public int SourceId { get; set; }
        public int TargetId { get; set; }
    }
}
