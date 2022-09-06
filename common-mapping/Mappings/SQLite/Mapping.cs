using common_mapping.Interfaces;
using common_mapping.Enums;
using common_mapping.Utils;
using common_mapping.Models.SQLite;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace common_mapping.Mappings.SQLite
{
    internal class Mapping : IMapping
    {
        private readonly Context _context;

        public Mapping(Settings.SQLite setup)
        {
            _context = new Context(setup);
        }

        public void Insert(IMapItem value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            Insert(value.LinkCode, value.SourceValue, value.TargetValue);
        }

        public void Insert(string linkCode, string sourceValue, string targetValue)
        {            
            if (sourceValue.Length == 0)
                throw new ArgumentException("SourceValue");
            if (targetValue.Length == 0)
                throw new ArgumentException("TargetValue");
            var link = _context.Links.FirstOrDefault(x => x.Code == linkCode);
            if (link == null)
            {
                _context.Links.Add(new MapLink() { Code = linkCode });
                _context.SaveChanges();
                link = _context.Links.First(x => x.Code == linkCode);
            }
            var temp = _context.Items.Where(x => x.LinkId == link.Id && x.SourceValue == sourceValue).ToList();
            if (temp.Count > 0)
                _context.Items.RemoveRange(temp);
            _context.Items.Add(new MapItem(linkCode, sourceValue, targetValue, link.Id));
            _context.SaveChanges();
        }

        public void Delete(IMapItem value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var link = _context.Links.FirstOrDefault(x => x.Code == value.LinkCode);
            if (link == null)
                return;
            var temp = _context.Items.Where(x => x.LinkId == link.Id && x.SourceValue == value.SourceValue).ToList();
            if (temp.Count > 0)
            {
                _context.Items.RemoveRange(temp);
                _context.SaveChanges();
            }
        }

        public void Delete(string linkCode, string sourceValue)
        {
            Delete(new MapItem(linkCode, sourceValue));
        }
        public IEnumerable<IMapItem> Get(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            if (type == MapFields.Source)
                return _context.Items.Include(x => x.Link).Where(_x => _x.Link.Code == linkCode && _x.SourceValue == searchValue);
            return _context.Items.Include(x => x.Link).Where(_x => _x.Link.Code == linkCode && _x.TargetValue == searchValue);
        }

        public IEnumerable<IMapItem> Get(string linkCode)
        {
            return _context.Items.Include(x => x.Link).Where(_x => _x.Link.Code == linkCode);
        }

        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            var temp = _context.Items.Include(y => y.Link).Where(x => x.Link.Code == linkCode);
            var result = GetValueByLike.GetLike(temp, searchValue, type);
            return result;
        }

        public IEnumerable<string> GetLinks()
        {
            return _context.Links.Select(x => x.Code).Distinct();
        }
    }
}
