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

        public IMapTypeController Types => _mapping.Types;

        public IMapLinkController Links => _mapping.Links;

        public IMapItemController Items => _mapping.Items;

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
    }
}