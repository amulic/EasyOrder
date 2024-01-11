using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface ICityRepository
    {
        City GetCity(int id);
        ICollection<City> GetCities();
        bool CreateCity(City city);
        bool CityExists(int userId);
        bool Save();
    }
}
