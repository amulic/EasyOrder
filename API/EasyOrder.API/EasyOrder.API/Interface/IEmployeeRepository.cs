using EasyOrder.API.Models.Domain;

namespace EasyOrder.API.Interface
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int employeeId);
        ICollection<Employee> GetEmployees();
        bool CreateEmployee(Employee employee);
        bool EmployeeExists(int employeeId);
        bool Save();
    }
}
