using EmployeeManagement.Model;

namespace EmployeeManagement.Data
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);
        int Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        IEnumerable<object> GetAllLocationsOfDepartment(string departmentName);
        decimal GetAverageSalaryOfDepartment(string departmentName);
        IEnumerable<object> GetEmployeeCountOfAllDepartment();
        IEnumerable<object> GetRecent5Employees();
        IEnumerable<Employee> SearchEmployee(int id);
        IEnumerable<Employee> SearchEmployee(string firstName);

        int Update(Employee employee);
    }
}