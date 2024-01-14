using EasyOrder.API.Data;
using EasyOrder.API.Interface;
using EasyOrder.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace EasyOrder.API.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return Save();
        }

        public bool EmployeeExists(int employeeId)
        {
            return _context.Employees.Any(c => c.Id == employeeId);
        }

        public Employee GetEmployee(int employeeId)
        {
            return _context.Employees.Where(x => x.Id == employeeId).FirstOrDefault();
        }

        public ICollection<Employee> GetEmployees()
        {
            return _context.Employees.ToList(); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
