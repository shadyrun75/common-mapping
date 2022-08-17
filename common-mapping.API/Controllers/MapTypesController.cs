using Microsoft.AspNetCore.Mvc;
using common_mapping.Interfaces;
using common_mapping.Models.API;

namespace common_mapping.API.Controllers
{
    [ApiController]
    [Route("Types")]
    public class MapTypesController : Controller
    {
        private readonly ILogger<MapTypesController> _logger;
        private readonly IMapping _mapping;
        public MapTypesController(ILogger<MapTypesController> logger, IMapping mapping)
        {
            (_logger, _mapping) = (logger, mapping);
        }

        [HttpGet("getAll")]
        public IEnumerable<MapType> Get() => _mapping.Types.Get().Select(x => new MapType(x));

        [HttpGet("getById")]
        public MapType Get(int id) => new MapType(_mapping.Types.Get(id));

        [HttpGet("get")]
        public IEnumerable<MapType> Get(int offset, int limit) => _mapping.Types.Get(offset, limit).Select(x => new MapType(x));

        [HttpPost("add")]
        public void Add(MapType value)
        {
            _mapping.Types.Insert(value);
        }

        [HttpPost("update")]
        public void Update(MapType value)
        {
            _mapping.Types.Update(value);
        }

        [HttpPost("delete")]
        public void Delete(MapType value)
        {
            _mapping.Types.Delete(value);
        }

        [HttpPost("deleteById")]
        public void Delete(int id)
        {
            _mapping.Types.Delete(id);
        }
    }
}
