using Microsoft.EntityFrameworkCore;
using GraphBfs.Models;
using GraphBfs.RepositoriesContracts;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GraphBfs.Dtos;
using System.Threading.Tasks;

namespace GraphBfs.Repository
{
    public class CityRepository: ICityRepository
    {
        private readonly RepositoriesContext _context;
        private readonly Mapper _mapper;

        public CityRepository(RepositoriesContext context)
        {
            _context = context;
            _mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<City, CityDto>().ReverseMap()));
        }

        public async Task<IEnumerable<CityDto>> GetCities()
        {
            var results =  await _context.Cities.ToListAsync();
            return _mapper.Map<IEnumerable<City>, IEnumerable<CityDto>>(results);
        }

        public async Task<CityDto> GetCityByID(int cityId)
        {
            var result = await _context.Cities.FindAsync(cityId);
            return _mapper.Map<City,CityDto>(result);
        }

        public async Task<CityDto> InsertCity(CityDto city)
        {
            var cityEntity = _mapper.Map<CityDto, City>(city);
            await _context.Cities.AddAsync(cityEntity);
            await _context.SaveChangesAsync();
            var cityDto = _mapper.Map<City, CityDto>(cityEntity);

            return cityDto;
        }

        public async Task DeleteCity(int cityId)
        {
            var city = await _context.Cities.FindAsync(cityId);
            var paths = _context.Paths.Where(x => x.InitialCity.Id == cityId || x.EndCity.Id == cityId);
            _context.Paths.RemoveRange(paths);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCity(CityDto city)
        {
            var cityEntity = _mapper.Map<CityDto, City>(city);
            _context.Cities.Update(cityEntity);
            await _context.SaveChangesAsync();
        }
    }
}
