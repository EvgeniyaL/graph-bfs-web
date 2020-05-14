using AutoMapper;
using GraphBfs.Dtos;
using GraphBfs.RepositoriesContracts;
using Microsoft.EntityFrameworkCore;
using GraphBfs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.Repository
{
    public class LogisticCenterRepository : ILogisticCenterRepository
    {
        private readonly RepositoriesContext _context;
        private readonly Mapper _mapper;

        public LogisticCenterRepository(RepositoriesContext context)
        {
            _context = context;
            _mapper = new Mapper(new MapperConfiguration(x => x.CreateMap<LogisticCenter, LogisticCenterDto>().ReverseMap()));
        }

        public async Task<IEnumerable<LogisticCenterDto>> GetLogisticCenters()
        {
            var results = await _context.LogisticCenters.ToListAsync();
            return _mapper.Map<IEnumerable<LogisticCenter>, IEnumerable<LogisticCenterDto>>(results);
        }

        public async Task<LogisticCenterDto> GetLogisticCenterByID(int logisticCenterId)
        {
            var result = await _context.LogisticCenters.FindAsync(logisticCenterId);
            return _mapper.Map<LogisticCenter, LogisticCenterDto>(result);
        }

        public async Task InsertLogisticCenter(LogisticCenterDto logisticCenter)
        {
            var logisticCenterEntity = _mapper.Map<LogisticCenterDto, LogisticCenter>(logisticCenter);
            await _context.LogisticCenters.AddAsync(logisticCenterEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLogisticCenter(int logisticCenterId)
        {
            var logisticCenter = await _context.LogisticCenters.FindAsync(logisticCenterId);
            _context.LogisticCenters.Remove(logisticCenter);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLogisticCenter(LogisticCenterDto logisticCenter)
        {
            var logisticCenterEntity = _mapper.Map<LogisticCenterDto, LogisticCenter>(logisticCenter);
             _context.Entry(logisticCenterEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
