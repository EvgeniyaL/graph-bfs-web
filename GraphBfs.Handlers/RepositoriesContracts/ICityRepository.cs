using GraphBfs.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphBfs.RepositoriesContracts
{
    public interface ICityRepository
    {
        Task<IEnumerable<CityDto>> GetCities();

        Task<CityDto> GetCityByID(int cityId);

        Task<CityDto> InsertCity(CityDto city);

        Task DeleteCity(int cityId);

        Task UpdateCity(CityDto city);
    }
}
