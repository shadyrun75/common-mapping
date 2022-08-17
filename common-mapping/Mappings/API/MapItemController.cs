using common_mapping.Enums;
using common_mapping.Interfaces;
using common_mapping.Models.API;

namespace common_mapping.Mappings.API
{
    public class MapItemController : IMapItemController
    {
        private readonly Client _client;
        public MapItemController(Client client)
        {
            _client = client;
        }

        public void Delete(IMapItem value)
        {
            Delete(value.LinkId, value.SourceValue);
        }

        public void Delete(int linkId, string sourceValue)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkId", linkId.ToString());
            query.Add("sourceValue", sourceValue);
            _client.DoRequest<object>("items/delete", HttpMethod.Post, query);
        }

        public IEnumerable<IMapItem> Get(int linkId, string searchValue, MapFields type = MapFields.Source)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkId", linkId.ToString());
            query.Add("searchValue", searchValue);
            query.Add("whatFieldFind", type.ToString());
            return _client.DoRequest<MapItem[]>("items/getByValue", HttpMethod.Get, query);
        }

        public IEnumerable<IMapItem> Get(int linkId)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkId", linkId.ToString());
            return _client.DoRequest<MapItem[]>("items/get", HttpMethod.Get, query);
        }

        public void Insert(IMapItem value)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkId", value.LinkId.ToString());
            query.Add("sourceValue", value.SourceValue);
            query.Add("targetValue", value.TargetValue);
            _client.DoRequest<object>("items/insert", HttpMethod.Post, query);
        }
    }
}
