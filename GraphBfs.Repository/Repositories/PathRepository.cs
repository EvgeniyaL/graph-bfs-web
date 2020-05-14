using AutoMapper;
using GraphBfs.Dtos;
using GraphBfs.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using GraphBfs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.Repository
{
    public class PathRepository : IPathRepository
    {
        private readonly RepositoriesContext _context;
        private readonly Mapper _mapper;

        public PathRepository(RepositoriesContext context)
        {
            _context = context;
            _mapper = new Mapper(new MapperConfiguration(x => { x.CreateMap<Path, PathDto>().ReverseMap();  x.CreateMap<City, CityDto>().ReverseMap(); }));
        }

        public async Task<IEnumerable<PathDto>> GetPaths()
        {
            var results = await _context.Paths.Include(x=>x.InitialCity).Include(x => x.EndCity).ToListAsync();
            return _mapper.Map<IEnumerable<Path>, IEnumerable<PathDto>>(results);
        }

        public async Task<PathDto> GetPathByID(int pathId)
        {
            var result = await _context.Paths.FindAsync(pathId);
            return _mapper.Map<Path, PathDto>(result);
        }

        public async Task<PathDto> InsertPath(PathDto path)
        {
            var pathEntity = _mapper.Map<PathDto, Path>(path);
            _context.Cities.Attach(pathEntity.InitialCity);
            _context.Cities.Attach(pathEntity.EndCity);
            await _context.Paths.AddAsync(pathEntity);
            await _context.SaveChangesAsync();

            var pathDto = _mapper.Map<Path, PathDto>(pathEntity);
            return pathDto;
        }

        public async Task DeletePath(int pathId)
        {
            var path = await _context.Paths.FindAsync(pathId);
            _context.Paths.Remove(path);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePath(PathDto path)
        {
            var pathEntity = _mapper.Map<PathDto, Path>(path);
            _context.Cities.Attach(pathEntity.InitialCity);
            _context.Cities.Attach(pathEntity.EndCity);
            _context.Update(pathEntity);
            await _context.SaveChangesAsync();
        }
    }
}
