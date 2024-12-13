using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductCategoryExternal>> GetProductsByCategoryAsync(string category);
    }
}