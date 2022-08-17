namespace common_mapping.Interfaces
{
    /// <summary>
    /// Model for type of mapping
    /// </summary>
    public interface IMapType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
