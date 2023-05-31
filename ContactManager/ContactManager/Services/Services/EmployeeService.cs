using ContactManager.Models;
using ContactManager.Services.IServices;
using System.Globalization;
using System;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ContactManager.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ManagerContext _dbContext;
        public EmployeeService(ManagerContext managerContext)
        {
            _dbContext = managerContext;
        }

        public void ReadEmployeesFromCsv(IFormFile file)
        {
            var employees = new List<EmployeeWithoutId>();

            if (file != null && file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        employees = csv.GetRecords<EmployeeWithoutId>().ToList();
                    }

                }
            }

            _dbContext.Employees.AddRange(MapEmployeesWithId(employees));
            _dbContext.SaveChanges();
        }

        private List<Employee> MapEmployeesWithId(List<EmployeeWithoutId> employees)
        {
            var employeesWithId = new List<Employee>();

            foreach (var employee in employees)
            {
                var employeeWithId = new Employee
                {
                    Name = employee.Name,
                    DateOfBirth = employee.DateOfBirth,
                    Married = employee.Married,
                    Phone = employee.Phone,
                    Salary = employee.Salary
                };

                employeesWithId.Add(employeeWithId);
            }

            return employeesWithId;
        }



        public void EditEmployee(Employee employee)
        {
            var existingEmployee = _dbContext.Employees.Find(employee.Id);

            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.DateOfBirth = employee.DateOfBirth;
                existingEmployee.Married = employee.Married;
                existingEmployee.Phone = employee.Phone;
                existingEmployee.Salary = employee.Salary;

                _dbContext.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
        }

        public bool ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name))
            {
                return false;
            }

            if (employee.DateOfBirth == default(DateTime))
            {
                return false;
            }

            if (employee.Salary <= 0)
            {
                return false;
            }

            if (!IsValidPhone(employee.Phone))
            {
                return false;
            }


            return true;
        }

        private bool IsValidPhone(string phone)
        {
            return !string.IsNullOrEmpty(phone) && phone.All(char.IsDigit);
        }

        public List<Employee> GetEmployees()
        {
            return _dbContext.Employees.ToList();
        }
    }
}
