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
        public static List<EmployeeNew> AllEmployeesNew()
        {
            var loc1 = new List<Location> { new Location { City = "Charlotte", State = "NC", ZipCode = "28277" }, new Location { City = "Charlotte", State = "NC", ZipCode = "28012" } };
            var loc2 = new List<Location> { new Location { City = "Boston", State = "MA", ZipCode = "02333" }, new Location { City = "Waldham", State = "MA", ZipCode = "02323" } };
            var loc3 = new List<Location> { new Location { City = "Greenville", State = "NC", ZipCode = "29011" } };
            var loc4 = new List<Location> { new Location { City = "Greenville", State = "SC", ZipCode = "26000" }, new Location { City = "Charleston", State = "SC", ZipCode = "26011" } };
            var loc5 = new List<Location> { new Location { City = "Greenville", State = "SC", ZipCode = "26000" }, new Location { City = "Charleston", State = "SC", ZipCode = "26011" } };

            return new List<EmployeeNew>()
            {
                new EmployeeNew {Id = 1, FirstName ="John", LastName = "Doe", Salary = 100000, Department = new Department{Id = 1, Name = "Technology" },  HireDate = DateTime.Now.AddMonths(-5), Locations = loc1 },
                new EmployeeNew {Id = 2, FirstName ="Dean", LastName = "Smith", Salary = 120000, Department = new Department{Id = 1, Name = "Technology" } ,  HireDate = DateTime.Now.AddDays(-5), Locations = loc2},
                new EmployeeNew {Id = 3, FirstName ="Jeff", LastName = "Stricklin", Salary = 130000, Department = new Department{Id = 1, Name = "Technology" },  HireDate = DateTime.Now.AddMonths(-3), Locations = loc3 },
                new EmployeeNew {Id = 4, FirstName ="Brad", LastName = "Johnson", Salary = 50000, Department = new Department{Id = 2, Name = "HR" },  HireDate = DateTime.Now.AddMonths(-1), Locations = loc4 },
                new EmployeeNew {Id = 5, FirstName ="Manoj", LastName = "Tiwari", Salary = 200000, Department = new Department{Id = 2, Name = "HR" },  HireDate = DateTime.Now.AddDays(-12), Locations = loc5 },
                new EmployeeNew {Id = 6, FirstName ="Doug", LastName = "Johns", Salary = 125000, Department = new Department{Id = 3, Name = "Sales" },  HireDate = DateTime.Now.AddMonths(-7), Locations = loc3 },
                new EmployeeNew {Id = 7, FirstName ="Scott", LastName = "Clark", Salary = 100000, Department = new Department{Id = 3, Name = "Sales" },  HireDate = DateTime.Now.AddDays(-5), Locations = loc2},
                new EmployeeNew {Id = 9, FirstName ="Michael", LastName = "Clark", Salary = 100000, Department = new Department{Id = 4, Name = "Marketing" },  HireDate = DateTime.Now.AddMonths(-9), Locations = loc2 },
                new EmployeeNew {Id = 9, FirstName ="Johnson", LastName = "Doe", Salary = 100000, Department = new Department{Id = 4, Name = "Marketing" },  HireDate = DateTime.Now.AddDays(-15), Locations = loc1 },
            };
        }

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

        public decimal GetAverageSalaryOfDepartment(string departmentName)
        {
            return AllEmployeesNew()
                .Where(grpDepart => grpDepart.Department.Name == departmentName)
                .Average(emp => emp.Salary);
        }

        public IEnumerable<object> GetEmployeeCountOfAllDepartment()
        {
            return AllEmployeesNew()
                .GroupBy(grpDepart => grpDepart.Department.Name)
                .Select(emp => new { DepartmentName = emp.Key, Count = emp.Count() });
        }

        public IEnumerable<object> GetRecent5Employees()
        {
            return AllEmployeesNew()
                .OrderByDescending(hireddate => hireddate.HireDate)
                .Take(5)
                .Select(emp => new { emp.FirstName, emp.LastName, emp.HireDate });
        }

        public IEnumerable<object> GetAllLocationsOfDepartment(string departmentName)
        {
            return AllEmployeesNew()
                   .Where(grpDepart => grpDepart.Department.Name == departmentName)
                   .SelectMany(loc => loc.Locations, (emp, loc) => new { loc.City, loc.State, loc.ZipCode });

        }


    }
}

