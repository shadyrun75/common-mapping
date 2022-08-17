namespace common_mapping.Interfaces
{
    /// <summary>
    /// Main controller of mapping
    /// </summary>
    public interface IMapping
    {
        IMapTypeController Types { get; }
        IMapLinkController Links { get; }
        IMapItemController Items { get; }
    }
}
