using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Model;

namespace EmployeeManagement.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private Dictionary<int, Employee> AllEmployees = new()
            {
                { 1,new Employee {Id=1,FirstName="Sachin",LastName="Tendulkar",Gender="Male",MaritalStatus="Married",Email="sachin.t@v2solutions.com",Department="DigitalEngineering",Designation="Lead",Address="Worli",MobileNo="12345",PhoneNo="",PostalCode="",State="Maharashtra",City="Mumbai",Country="India" }
                },
                { 2,new Employee {Id=2,FirstName="Rakesh",LastName="Roshan",Gender="Male",MaritalStatus="Married",Email="rakesh.r@v2Solutions.com",Department="DigitalMarketing",Designation="ResearchExpert",Address="Indore",MobileNo="54321",PhoneNo="",PostalCode="",State="MadhyaPradesh",City="Indore",Country="India" }
                },
                { 3,new Employee {Id=3,FirstName="Priyanka",LastName="Chopra",Gender="Female",MaritalStatus="Married",Email="priyanka.c@v2solutions.com",Department="MovieMaking",Designation="Director",Address="America",MobileNo="007",PhoneNo="",PostalCode="",State="California",City="LosAngeles",Country="Usa" }
                },
                { 4,new Employee {Id=4,FirstName="Pratik",LastName="Mhatre",Gender="Male",MaritalStatus="Single",Email="pratik.m@v2solutions.com",Department="DigitalSupport",Designation="Consultant",Address="Banglore",MobileNo="456789",PhoneNo="",PostalCode="",State="Karnataka",City="Banglore",Country="India" }
                }
            };

        public Employee Add(Employee employee)
        {
            int keyValue = AllEmployees.Count + 1;
            employee.Id = keyValue;

            AllEmployees.Add(keyValue, employee);

            return employee;
        }

        public int Update(Employee employee)
        {
            if (!AllEmployees.Any(emp => emp.Key == employee.Id))
                return 0;

            AllEmployees[employee.Id] = employee;
            return 1;
        }

        public int Delete(int employeeId)
        {
            if (!AllEmployees.Any(emp => emp.Key == employeeId))
                return 0;

            AllEmployees.Remove(employeeId);
            return 1;
        }

        public IEnumerable<Employee> GetAll()
        {
            return AllEmployees.Select(emp => emp.Value);
        }

        public IEnumerable<Employee> SearchEmployee(int id)
        {
            return AllEmployees.Where(emp => emp.Key == id).Select(emp => emp.Value);
        }

        public IEnumerable<Employee> SearchEmployee(string firstName)
        {

            return AllEmployees.Where(emp => emp.Value.FirstName == firstName).Select(emp => emp.Value);
        }

    }
}

