using common_mapping.Enums;

namespace common_mapping.Interfaces
{
    /// <summary>
    /// Controller of item mapping
    /// </summary>
    public interface IMapItemController
    {
        public IEnumerable<IMapItem> Get(int linkId, string searchValue, MapFields type = MapFields.Source);
        public IEnumerable<IMapItem> Get(int linkId);
        public void Insert(IMapItem value);
        public void Delete(IMapItem value);
        public void Delete(int linkId, string sourceValue);
    }
}
