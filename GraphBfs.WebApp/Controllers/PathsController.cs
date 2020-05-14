using GraphBfs.Dtos;
using GraphBfs.RepositoriesContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PathsController : Controller
    {
        private readonly ILogger<PathsController> _logger;
        private readonly IPathRepository _pathRepository;

        public PathsController(ILogger<PathsController> logger, IPathRepository pathRepository)
        {
            _logger = logger;
            _pathRepository = pathRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PathDto>> Get()
        {
            var results = await _pathRepository.GetPaths();

            return results;
        }

        [HttpPost]
        public async Task<PathDto> Create(PathDto path)
        {
            var result = await _pathRepository.InsertPath(path);
            return result;
        }

        [HttpPut]
        public async Task Update(PathDto path)
        {
            await _pathRepository.UpdatePath(path);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _pathRepository.DeletePath(id);
        }
    }
}