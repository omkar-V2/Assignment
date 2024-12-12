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
        IEnumerable<object> GetTotalNoOfOrderPlacedInLast3Month();
        IEnumerable<object> GetTotalNoOfOrderPlacedPerMonthInLast3Month();
        IEnumerable<object> GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month();
    }
}