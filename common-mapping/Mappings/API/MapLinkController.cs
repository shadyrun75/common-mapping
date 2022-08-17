using common_mapping.Interfaces;
using common_mapping.Models.API;

namespace common_mapping.Mappings.API
{
    public class MapLinkController : IMapLinkController
    {
        private readonly Client _client;
        public MapLinkController(Client client)
        {
            _client = client;
        }

        public void Delete(IMapLink value)
        {
            _client.DoRequest<object>("links/delete", HttpMethod.Post, null, value);
        }

        public void Delete(int id)
        {
            var query = new Dictionary<string, string>();
            query.Add("id", id.ToString());
            _client.DoRequest<object>("links/deletebyid", HttpMethod.Post, query);
        }

        public IMapLink? Get(int id)
        {
            var query = new Dictionary<string, string>();
            query.Add("id", id.ToString());
            var result = _client.DoRequest<MapLink>("links/getById", HttpMethod.Get, query);
            return result;
        }

        public IEnumerable<IMapLink> Get()
        {
            var result = _client.DoRequest<MapLink[]>("links/getAll", HttpMethod.Get);
            return result;
        }

        public IEnumerable<IMapLink> Get(int offset, int limit)
        {
            var query = new Dictionary<string, string>();
            query.Add("offset", offset.ToString());
            query.Add("limit", limit.ToString());
            var result = _client.DoRequest<IEnumerable<IMapLink>>("links/get", HttpMethod.Get, query);
            return result;
        }

        public void Insert(IMapLink value)
        {
            _client.DoRequest<object>("links/add", HttpMethod.Post, null, value);
        }

        public void Update(IMapLink value)
        {
            _client.DoRequest<object>("links/update", HttpMethod.Post, null, value);
        }
    }
}
