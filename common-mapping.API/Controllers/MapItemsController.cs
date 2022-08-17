using Microsoft.AspNetCore.Mvc;
using common_mapping.Interfaces;
using common_mapping.Models.API;
using common_mapping.Enums;

namespace common_mapping.API.Controllers
{
    [ApiController]
    [Route("Items")]
    public class MapItemController : Controller
    {
        private readonly ILogger<MapItemController> _logger;
        private readonly IMapping _mapping;
        public MapItemController(ILogger<MapItemController> logger, IMapping mapping)
        {
            (_logger, _mapping) = (logger, mapping);
        }

        [HttpPost("delete")]
        public void Delete(int linkId, string sourceValue)
        {
            _mapping.Items.Delete(linkId, sourceValue);
        }

        [HttpPost("insert")]
        public void Insert(int linkId, string sourceValue, string targetValue)
        {
            _mapping.Items.Insert(new MapItem(linkId, sourceValue, targetValue));
        }

        [HttpGet("getByValue")]
        public IEnumerable<MapItem> GetByValue(int linkId, string searchValue, MapFields whatFieldFind = MapFields.Target) => _mapping.Items.Get(linkId, searchValue, whatFieldFind)
            .Select(x => new MapItem(x));

        [HttpGet("get")]
        public IEnumerable<MapItem> Get(int linkId) => _mapping.Items.Get(linkId).Select(x => new MapItem(x));
    }
}
