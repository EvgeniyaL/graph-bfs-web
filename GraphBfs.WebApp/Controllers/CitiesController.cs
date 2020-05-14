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
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityRepository _cityRepository;

        public CitiesController(ILogger<CitiesController> logger, ICityRepository cityRepository)
        {
            _logger = logger;
            _cityRepository = cityRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<CityDto>> Get()
        {
            var results = await _cityRepository.GetCities();
            return results;
        }

        [HttpPost]
        public async Task<CityDto> Create(CityDto city)
        {
            var result =  await _cityRepository.InsertCity(city);
            return result;
        }

        [HttpPut]
        public async Task Update(CityDto city)
        {
            await _cityRepository.UpdateCity(city);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _cityRepository.DeleteCity(id);
        }
    }
}
