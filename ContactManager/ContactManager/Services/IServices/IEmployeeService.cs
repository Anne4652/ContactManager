using ContactManager.Models;

namespace ContactManager.Services.IServices
{
    public interface IEmployeeService
    {
        public void ReadEmployeesFromCsv(IFormFile file);
        public void EditEmployee(Employee employee);
        public void DeleteEmployee(int id);
        public bool ValidateEmployee(Employee employee);
        public List<Employee> GetEmployees();
    }
}
