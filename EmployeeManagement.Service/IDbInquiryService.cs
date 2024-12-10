
namespace EmployeeManagement.Service
{
    public interface IDbInquiryService
    {
        IEnumerable<object> GetAllCategoryTotalNoOfInquiryInLast3Month();
        object? GetDateWithHighestNoOfInquiryInYear(int year);
        object GetProductCategoryWithHighestNoOfInquiryInLast3Month();
    }
}