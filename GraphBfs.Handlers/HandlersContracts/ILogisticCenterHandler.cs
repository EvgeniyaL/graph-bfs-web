using GraphBfs.Dtos;
using System.Threading.Tasks;

namespace GraphBfs.HandlersContracts
{
     public interface ILogisticCenterHandler
    {
        Task<LogisticCenterDto> ProcessLogisticCenter();
    }
}
