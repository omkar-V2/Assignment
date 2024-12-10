
namespace EmployeeManagement.Service
{
    public interface IDbSupportTicketService
    {
        IEnumerable<object> GetDuplicateSupportActivity();
        object GetHighestNoSupportTicketsMonthOfYear(int year);
        IEnumerable<object> GetMajorSupportTicketCountAndStatus();
        double GetSupportTicketsAverageInLast3Month();
        double GetSupportTicketsAveragePerMonth();
        int GetSupportTicketsTotalInLast3Month();
        IEnumerable<object> GetSupportTicketsTotalNoPerCategoryInLast3Month();
    }
}