using common_mapping.Interfaces;
using Microsoft.Extensions.Logging;

namespace common_mapping.Mappings.API
{
    public class Mapping : IMapping
    {
        private readonly Client _client;
        public Mapping(Settings.API setup, ILogger logger)
        {
            _client = new Client(setup, logger);
            Types = new MapTypeController(_client);
            Links = new MapLinkController(_client);
            Items = new MapItemController(_client);
        }
        public IMapTypeController Types { get; } 

        public IMapLinkController Links { get; } 

        public IMapItemController Items { get; }
    }
}
