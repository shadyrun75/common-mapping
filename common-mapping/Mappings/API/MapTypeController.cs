using common_mapping.Interfaces;
using common_mapping.Models.API;

namespace common_mapping.Mappings.API
{
    public class MapTypeController : IMapTypeController
    {
        private readonly Client _client;
        public MapTypeController(Client client)
        {
            _client = client;
        }

        public void Delete(IMapType value)
        {
            _client.DoRequest<object>("types/delete", HttpMethod.Post, null, value);
        }

        public void Delete(int id)
        {
            var query = new Dictionary<string, string>();
            query.Add("id", id.ToString());
            _client.DoRequest<object>("types/deletebyid", HttpMethod.Post, query);
        }

        public IMapType? Get(int id)
        {
            var query = new Dictionary<string, string>();
            query.Add("id", id.ToString());
            var result = _client.DoRequest<MapType>("types/getById", HttpMethod.Get, query);
            return result;
        }

        public IEnumerable<IMapType> Get()
        {
            var result = _client.DoRequest<MapType[]>("types/getAll", HttpMethod.Get);
            return result;
        }

        public IEnumerable<IMapType> Get(int offset, int limit)
        {
            var query = new Dictionary<string, string>();
            query.Add("offset", offset.ToString());
            query.Add("limit", limit.ToString());
            var result = _client.DoRequest<IEnumerable<MapType>>("types/get", HttpMethod.Get, query);
            return result;
        }

        public void Insert(IMapType value)
        {
            _client.DoRequest<object>("types/add", HttpMethod.Post, null, value);
        }

        public void Update(IMapType value)
        {
            _client.DoRequest<object>("types/update", HttpMethod.Post, null, value);
        }
    }
}
