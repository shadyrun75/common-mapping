using Microsoft.EntityFrameworkCore;
using common_mapping.Interfaces;
using common_mapping.Models.SQLite;
using common_mapping.Enums;

namespace common_mapping.Mappings.SQLite
{
    internal class MapItemController : IMapItemController
    {
        private readonly Context _context;
        public MapItemController(Context context)
        {
            _context = context;
        }

        public void Insert(IMapItem value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.SourceValue.Length == 0)
                throw new ArgumentException("SourceValue");
            if (value.TargetValue.Length == 0)
                throw new ArgumentException("TargetValue");
            var temp = _context.Items.Where(x => x.LinkId == value.LinkId && x.SourceValue == value.SourceValue).ToList();
            if (temp.Count > 0)
                _context.Items.RemoveRange(temp);
            _context.Items.Add(new MapItem(value));
            _context.SaveChanges();
        }

        public void Delete(IMapItem value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var temp = _context.Items.Where(x => x.LinkId == value.LinkId && x.SourceValue == value.SourceValue).ToList();
            if (temp.Count > 0)
            {
                _context.Items.RemoveRange(temp);
                _context.SaveChanges();
            }
        }

        public void Delete(int linkId, string sourceValue)
        {
            Delete(new MapItem(linkId, sourceValue));
        }
        public IEnumerable<IMapItem> Get(int linkId, string searchValue, MapFields type = MapFields.Source)
        {
            if (type == MapFields.Source)
                return _context.Items.Where(x => x.LinkId == linkId && x.SourceValue == searchValue);
            return _context.Items.Where(x => x.LinkId == linkId && x.TargetValue == searchValue);
        }

        public IEnumerable<IMapItem> GetDetailed(int linkId, string searchValue, MapFields type = MapFields.Source)
        {
            if (type == MapFields.Source)
                return _context.Items.Include(x => x.Link).Where(x => x.LinkId == linkId && x.SourceValue == searchValue);
            return _context.Items.Include(x => x.Link).Where(x => x.LinkId == linkId && x.TargetValue == searchValue);
        }

        public IEnumerable<IMapItem> Get(int linkId)
        {
            return _context.Items.Where(_x => _x.LinkId == linkId);
        }

        public IEnumerable<IMapItem> GetDetailed(int linkId)
        {
            return _context.Items.Include(x => x.Link.Source).Include(y => y.Link.Target).Where(_x => _x.LinkId == linkId);
        }
    }
}
