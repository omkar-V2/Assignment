using EmployeeManagement.Model;

namespace EmployeeManagement.Data
{
    public interface IEmployeeRepository
    {
        Employee Add(Employee employee);
        int Delete(int employeeId);
        IEnumerable<Employee> GetAll();
        IEnumerable<Employee> SearchEmployee(int id);
        IEnumerable<Employee> SearchEmployee(string firstName);

        int Update(Employee employee);
    } 
}