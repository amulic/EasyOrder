using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IAdministratorRepository
    {
        Administrator GetAdministrator(int id);
        ICollection<Administrator> GetAdministrators();
        bool CreateAdministrator(Administrator administrator);
        bool AdministratorExists(int adminId);
        bool Save();
    }
}
