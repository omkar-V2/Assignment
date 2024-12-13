using EmployeeManagement.Model;

namespace EmployeeManagement.Service
{
    public interface IEmployeeService
    {
        Employee Add(Employee employee);
        int Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        IEnumerable<object> GetAllLocationsOfDepartment(string departmentName);
        decimal GetAverageSalaryOfDepartment(string departmentName);
        IEnumerable<object> GetEmployeeCountOfAllDepartment();
        IEnumerable<object> GetRecent5Employees();
        IEnumerable<Employee> SearchEmployee(string firstName);
        IEnumerable<Employee> SearchEmployee(int id);

        int Update(Employee employee);
    }
}