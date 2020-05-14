using GraphBfs.Dtos;
using GraphBfs.HandlersContracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GraphBfs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogisticCenterController: ControllerBase
    {
        private readonly ILogisticCenterHandler _logisticCenterHandler;

        public LogisticCenterController(ILogisticCenterHandler logisticCenterHandler)
        {
            _logisticCenterHandler = logisticCenterHandler;
        }

        [HttpGet]
        public async Task<LogisticCenterDto> Get()
        {
            var results = await _logisticCenterHandler.ProcessLogisticCenter();

            return results;
        }
    }
}
