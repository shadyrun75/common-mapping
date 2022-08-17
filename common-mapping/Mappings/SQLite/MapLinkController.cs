using Microsoft.EntityFrameworkCore;
using common_mapping.Interfaces;
using common_mapping.Models.SQLite;

namespace common_mapping.Mappings.SQLite
{
    internal class MapLinkController : IMapLinkController
    {
        private readonly Context _context;
        public MapLinkController(Context context)
        {
            _context = context;
        }
        public IEnumerable<IMapLink> GetMapLinks()
        {
            return _context.Links.ToList();
        }

        public void Insert(IMapLink value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var source = _context.Types.FirstOrDefault(x => x.Id == value.SourceId);
            if (source == null)
                throw new Exception($"MapType {value.SourceId} is not found");
            var target = _context.Types.FirstOrDefault(x => x.Id == value.TargetId);
            if (target == null)
                throw new Exception($"MapType {value.TargetId} is not found");
            MapLink link = new MapLink()
            {
                Id = value.Id,
                SourceId = source.Id,
                TargetId = target.Id,
                Source = source,
                Target = target

            };
            _context.Links.Add(link);
            _context.SaveChanges();
        }

        public void Update(IMapLink value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            
            var source = _context.Types.FirstOrDefault(x => x.Id == value.SourceId);
            if (source == null)
                throw new Exception($"MapType {value.SourceId} is not found");

            var target = _context.Types.FirstOrDefault(x => x.Id == value.TargetId);
            if (target == null)
                throw new Exception($"MapType {value.TargetId} is not found");

            var link = _context.Links.Include(u => u.Source).Include(u => u.Target).FirstOrDefault(x => x.Id == value.Id);
            if (link == null)
                throw new Exception($"Link {value.Id} is not found");
            link.Source = source;
            link.Target = target;
            link.SourceId = value.SourceId;
            link.TargetId = value.TargetId;

            _context.Links.Update(link);
            _context.SaveChanges();
        }

        public void Delete(IMapLink value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var link = _context.Links.FirstOrDefault(x => x.Id == value.Id);
            if (link == null)
                throw new Exception($"Link {value.Id} is not found");
            _context.Links.Remove(link);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Delete(new MapLink() { Id = id });
        }

        public IMapLink? Get(int id)
        {
            return _context.Links.Include(u => u.Source).Include(u => u.Target).FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<IMapLink> Get(int offset = 0, int limit = 0)
        {
            if ((offset <= 0) && (limit <= 0))
                return _context.Links.Include(u => u.Source).Include(u => u.Target).ToList();
            return _context.Links.Include(u => u.Source).Include(u => u.Target).Take(limit).Skip(offset * limit);
        }

        public IEnumerable<IMapLink> Get()
        {
            return Get(0, 0);
        }
    }
}
