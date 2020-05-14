using GraphBfs.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.RepositoriesContracts
{
    public interface IPathRepository
    {
        Task<IEnumerable<PathDto>> GetPaths();

        Task<PathDto> GetPathByID(int pathId);

        Task<PathDto> InsertPath(PathDto path);

        Task DeletePath(int pathId);

        Task UpdatePath(PathDto path);
    }
}
