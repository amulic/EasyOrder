using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface ICountryRepository
    {
        Country GetCountry(int countryId);
        ICollection<Country> GetCountries();
        bool CreateCountry(Country country);
        bool CountryExists(int userId);
        bool Save();
    }
}
