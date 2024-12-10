using Common;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public interface IDbOrderService
    {
        IEnumerable<Order> GetAllOrder();
        IEnumerable<Order> GetAllOrder(Func<Order, bool> selector);
        IEnumerable<string> GetCustomerMadePurchasesInPartsOfDayOfFirstMonthOfYear(int month, int year, Helpers.PartOfDay partOfDay);
        IEnumerable<object> GetGroupOfCustomersPlacedOrderInLast3Month();
        IEnumerable<Order> GetOrderByOrderNo(string orderNo);
        IEnumerable<object> GetOrderCountPerMonthByYear(int year);
        int GetTotalNoOfOrderPlacedInLast3Month();
        int GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month();
    }
}