namespace common_mapping.Interfaces
{
    /// <summary>
    /// Controller of link mapping
    /// </summary>
    public interface IMapLinkController
    {
        public IMapLink? Get(int id);
        public IEnumerable<IMapLink> Get();
        public IEnumerable<IMapLink> Get(int offset, int limit);
        public void Insert(IMapLink value);
        public void Update(IMapLink value);
        public void Delete(IMapLink value);
        public void Delete(int id);
    }
}
