using Microsoft.EntityFrameworkCore.Sqlite;
using common_mapping.Interfaces;

namespace common_mapping.Mappings.SQLite
{
    internal class Mapping : IMapping
    {
        private readonly Context _context;

        public IMapTypeController Types { get; }

        public IMapLinkController Links { get; }

        public IMapItemController Items { get; }

        public Mapping(Settings.SQLite setup)
        {
            _context = new Context(setup);
            Types = new MapTypeController(_context);
            Links = new MapLinkController(_context);
            Items = new MapItemController(_context);
        }
    }
}
