
namespace EmployeeManagement.Service
{
    public interface IDbCustomerService
    {
        IEnumerable<object> GetCustomerLoyaltyTiers();
        IEnumerable<object> GetLast3MonthsCustomerOrderInfoOfPurchases(string customerId);
        IEnumerable<object> GetLast3MonthsCustomerPurchaseInfo(string customerId);
        object GetMostActiveCustomerInLast3Month();
        IEnumerable<object> GetPurchaseAndOrderInfo(string customerId);
        IEnumerable<object> GetUniqueCustomerInteractedInLast3Month();
    }
}