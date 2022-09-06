using Microsoft.AspNetCore.Mvc;
using common_mapping.Interfaces;
using common_mapping.Models.API;
using common_mapping.Enums;

namespace common_mapping.API.Controllers
{
    [ApiController]
    [Route("Items")]
    public class MappingController : Controller
    {
        private readonly ILogger<MappingController> _logger;
        private readonly IMapping _mapping;
        public MappingController(ILogger<MappingController> logger, IMapping mapping)
        {
            (_logger, _mapping) = (logger, mapping);
        }

        [HttpPost("delete")]
        public void Delete(string linkCode, string sourceValue)
        {
            _mapping.Delete(linkCode, sourceValue);
        }

        [HttpPost("insert")]
        public void Insert(string linkCode, string sourceValue, string targetValue)
        {
            _mapping.Insert(new MapItem(linkCode, sourceValue, targetValue));
        }

        [HttpGet("getByValue")]
        public IEnumerable<MapItem> GetByValue(string linkCode, string searchValue, MapFields searchField = MapFields.Source) => 
            _mapping.Get(linkCode, searchValue, searchField).Select(x => new MapItem(x));

        [HttpGet("get")]
        public IEnumerable<MapItem> Get(string linkCode) => _mapping.Get(linkCode).Select(x => new MapItem(x));

        [HttpGet("getByLike")]
        public IEnumerable<string> GetByLike(string linkCode, string searchValue, MapFields searchField = MapFields.Source) => 
            _mapping.GetByLike(linkCode, searchValue, searchField);


        [HttpGet("getLinks")]
        public IEnumerable<string> GetLinks() => _mapping.GetLinks();
    }
}
