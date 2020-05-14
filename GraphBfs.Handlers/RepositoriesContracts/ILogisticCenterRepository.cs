using GraphBfs.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.RepositoriesContracts
{
    public interface ILogisticCenterRepository
    {
        Task<IEnumerable<LogisticCenterDto>> GetLogisticCenters();
        Task<LogisticCenterDto> GetLogisticCenterByID(int logisticCenterId);

        Task InsertLogisticCenter(LogisticCenterDto logisticCenter);

        Task DeleteLogisticCenter(int logisticCenterId);

        Task UpdateLogisticCenter(LogisticCenterDto logisticCenter);
    }
}
