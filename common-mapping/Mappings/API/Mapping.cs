using common_mapping.Enums;
using common_mapping.Interfaces;
using common_mapping.Models.API;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace common_mapping.Mappings.API
{
    public class Mapping : IMapping
    {
        private readonly Client _client;
        public Mapping(Settings.API setup, ILogger logger)
        {
            _client = new Client(setup, logger);
        }

        public void Insert(IMapItem value)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkCode", value.LinkCode);
            query.Add("sourceValue", value.SourceValue);
            query.Add("targetValue", value.TargetValue);
            _client.DoRequest<object>("items/insert", HttpMethod.Post, query);
        }

        public void Insert(string linkCode, string sourceValue, string targetValue)
        {
            throw new NotImplementedException();
        }

        public void Delete(IMapItem value)
        {
            Delete(value.LinkCode, value.SourceValue);
        }

        public void Delete(string linkCode, string sourceValue)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkCode", linkCode);
            query.Add("sourceValue", sourceValue);
            _client.DoRequest<object>("items/delete", HttpMethod.Post, query);
        }

        public IEnumerable<IMapItem> Get(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkCode", linkCode);
            query.Add("searchValue", searchValue);
            query.Add("whatFieldFind", type.ToString());
            return _client.DoRequest<MapItem[]>("items/getByValue", HttpMethod.Get, query);
        }

        public IEnumerable<IMapItem> Get(string linkCode)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkCode", linkCode);
            return _client.DoRequest<MapItem[]>("items/get", HttpMethod.Get, query);
        }

        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            var query = new Dictionary<string, string>();
            query.Add("linkCode", linkCode);
            query.Add("searchValue", searchValue);
            query.Add("whatFieldFind", type.ToString());
            return _client.DoRequest<IEnumerable<string>>("items/getByLike", HttpMethod.Get, query);
        }

        public IEnumerable<string> GetLinks()
        {
            return _client.DoRequest<string[]>("items/getLinks", HttpMethod.Get);
        }        
    }
}
