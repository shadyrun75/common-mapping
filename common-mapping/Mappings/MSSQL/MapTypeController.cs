using common_mapping.Interfaces;
using common_mapping.Models.MSSQL;

namespace common_mapping.Mappings.MSSQL
{
    internal class MapTypeController : IMapTypeController
    {
        private readonly Context _context;
        public MapTypeController(Context context)
        {
            _context = context;
        }
        public void Insert(IMapType value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            _context.Types.Add(new MapType(value));
            _context.SaveChanges();
        }

        public void Update(IMapType value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var type = _context.Types.FirstOrDefault(x => x.Id == value.Id);
            if (type == null)
                throw new Exception($"MapType {value.Id} is not found");
            type.Name = value.Name;
            type.Description = value.Description;
            _context.SaveChanges();
        }

        public void Delete(IMapType value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var type = _context.Types.FirstOrDefault(x => x.Id == value.Id);
            if (type == null)
                throw new Exception($"MapType {value.Id} is not found");
            _context.Types.Remove(type);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            Delete(new MapType { Id = id });
        }

        public IEnumerable<IMapType> Get()
        {
            return Get(0, 0);
        }

        public IEnumerable<IMapType> Get(int offset = 0, int limit = 0)
        {
            if ((offset <= 0) && (limit <= 0))
                return _context.Types.ToList();
            return _context.Types.Take(limit).Skip(offset * limit);
        }

        public IMapType? Get(int id)
        {
            return _context.Types.FirstOrDefault(x => x.Id == id);
        }
    }
}
