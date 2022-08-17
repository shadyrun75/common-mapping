namespace common_mapping.Interfaces
{
    /// <summary>
    /// Controller of type mapping
    /// </summary>
    public interface IMapTypeController
    {
        public IMapType? Get(int id);
        public IEnumerable<IMapType> Get();
        public IEnumerable<IMapType> Get(int offset, int limit);
        public void Insert(IMapType value);
        public void Update(IMapType value);
        public void Delete(IMapType value);
        public void Delete(int id);

    }
}
