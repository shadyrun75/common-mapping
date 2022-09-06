﻿using common_mapping.Interfaces;
using common_mapping.Enums;
using common_mapping.Utils;
using common_mapping.Models.MSSQL;

namespace common_mapping.Mappings.MSSQL
{
    internal class Mapping : IMapping
    {
        private readonly Context _context;

        public Mapping(Settings.MSSQL setup)
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
            var temp = _context.Items.Where(x => x.LinkCode == linkCode && x.SourceValue == sourceValue).ToList();
            if (temp.Count > 0)
                _context.Items.RemoveRange(temp);
            _context.Items.Add(new MapItem(linkCode, sourceValue, targetValue));
            _context.SaveChanges();
        }

        public void Delete(IMapItem value)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            var temp = _context.Items.Where(x => x.LinkCode == value.LinkCode && x.SourceValue == value.SourceValue).ToList();
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
                return _context.Items.Where(x => x.LinkCode == linkCode && x.SourceValue == searchValue);
            return _context.Items.Where(x => x.LinkCode == linkCode && x.TargetValue == searchValue);
        }

        public IEnumerable<IMapItem> Get(string linkCode)
        {
            return _context.Items.Where(_x => _x.LinkCode == linkCode);
        }

        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields type = MapFields.Source)
        {
            var temp = _context.Items.Where(x => x.LinkCode == linkCode);
            var result = GetValueByLike.GetLike(temp, searchValue, type);
            return result;
        }

        public IEnumerable<string> GetLinks()
        {
            return _context.Items.Select(x => x.LinkCode).Distinct();
        }
    }
}
