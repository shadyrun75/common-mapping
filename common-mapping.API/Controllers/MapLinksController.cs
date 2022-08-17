using Microsoft.AspNetCore.Mvc;
using common_mapping.Interfaces;
using common_mapping.Models.API;

namespace common_mapping.API.Controllers
{
    [ApiController]
    [Route("Links")]
    public class MapLinksController : Controller
    {
        private readonly ILogger<MapLinksController> _logger;
        private readonly IMapping _mapping;
        public MapLinksController(ILogger<MapLinksController> logger, IMapping mapping)
        {
            (_logger, _mapping) = (logger, mapping);
        }

        [HttpGet("getAll")]
        public IEnumerable<MapLink> Get() => _mapping.Links.Get().Select(x => new MapLink(x));

        [HttpGet("getById")]
        public MapLink Get(int id) => new MapLink(_mapping.Links.Get(id));

        [HttpGet("get")]
        public IEnumerable<MapLink> Get(int offset, int limit) => _mapping.Links.Get(offset, limit).Select(x => new MapLink(x));

        [HttpPost("add")]
        public void Add(MapLink value)
        {
            _mapping.Links.Insert(value);
        }

        [HttpPost("update")]
        public void Update(MapLink value)
        {
            _mapping.Links.Update(value);
        }

        [HttpPost("delete")]
        public void Delete(MapLink value)
        {
            _mapping.Links.Delete(value);
        }

        [HttpPost("deleteById")]
        public void Delete(int id)
        {
            _mapping.Links.Delete(id);
        }
    }
}
