using common_mapping.Enums;
using common_mapping.Interfaces;
using common_mapping.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace common_mapping
{
    public class Mapping : IMapping
    {
        private IMapping _mapping;
        public Mapping(IOptions<Setup> setup, ILogger logger = null)
        {
            if (setup == null)  
                throw new ArgumentNullException(nameof(setup));
            switch (setup.Value.ConnectionType)
            {
                case ConnectionType.Database_MSSQL: 
                    _mapping = new Mappings.MSSQL.Mapping(setup.Value.MSSQL); 
                    break;
                case ConnectionType.Database_SQLite:
                    if (setup.Value.SQLite == null)
                        throw new Exception("Settings of SQLite is null!");
                    _mapping = new Mappings.SQLite.Mapping(setup.Value.SQLite); 
                    break;
                case ConnectionType.API: _mapping = new Mappings.API.Mapping(setup.Value.API, logger); break;
                default: throw new Exception($"Unsupported {setup.Value.ConnectionType} type of mapping connection");
            }
        }

        public void Delete(IMapItem value)
        {
            _mapping.Delete(value);
        }

        public void Delete(string linkCode, string sourceValue)
        {
            _mapping.Delete(linkCode, sourceValue);
        }

        public IEnumerable<IMapItem> Get(string linkCode)
        {
            return _mapping.Get(linkCode);
        }

        public IEnumerable<IMapItem> Get(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            return _mapping.Get(linkCode, searchValue, type);
        }

        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            return _mapping.GetByLike(linkCode, searchValue, type);
        }

        public IEnumerable<string> GetLinks()
        {
            return _mapping.GetLinks();
        }

        public void Insert(IMapItem value)
        {
            _mapping.Insert(value);
        }

        public void Insert(string linkCode, string sourceValue, string targetValue)
        {
            _mapping.Insert(linkCode, sourceValue, targetValue);
        }
    }
}