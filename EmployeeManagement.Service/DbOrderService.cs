using System.Linq;
using EmployeeManagement.Data;
using static Common.Helpers;

namespace EmployeeManagement.Service
{
    public class DbOrderService : IDbOrderService
    {

        public IEnumerable<Order> GetAllOrder()
        {
            return DBData.Orders;
        }

        public IEnumerable<Order> GetAllOrder(Func<Order, bool> selector)
        {
            return DBData.Orders.Where(selector);
        }

        public IEnumerable<Order> GetOrderByOrderNo(string orderNo)
        {
            return DBData.Orders.Where(ord => ord.OrderNo == orderNo);
        }

        public IEnumerable<string> GetCustomerMadePurchasesInPartsOfDayOfFirstMonthOfYear(int month, int year, PartOfDay partOfDay)
        {
            return DBData.Orders.Where(ord => ord.OrderDateTime.Year == year
                               && ord.OrderDateTime.Month == month
                               && GetTimeOftheDay(ord.OrderDateTime.TimeOfDay) == partOfDay)
                                .Select(cus => cus.CustomerId)
                                .Distinct();
        }

        public IEnumerable<object> GetTotalNoOfOrderPlacedInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.Orders
                .Where(get => get.OrderDateTime > fromDate)
                .SelectMany(prod => prod.Products, (ord, prod) => new
                {
                    productId = prod.ProductID,
                    productName = prod.Title
                })
                .GroupBy(group => new { group.productId, group.productName })
                .Select(sel => new
                {
                    sel.Key.productId,
                    sel.Key.productName,
                    totalOrderCount = sel.Count()
                });
        }

        public IEnumerable<object> GetTotalNoOfUniqueCustomersPlacedOrderInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.Orders.Where(get => get.OrderDateTime > fromDate)
                                .Select(ord => ord.CustomerId)
                                .Distinct();
        }

        public IEnumerable<object> GetGroupOfCustomersPlacedOrderInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.Orders
                         .Where(get => get.OrderDateTime > fromDate)
                         .GroupBy(ord => new
                         {
                             ord.OrderDateTime.Year,
                             ord.OrderDateTime.Month
                         })
                         .Select(ord => new
                         {
                             orderdateyear = ord.Key.Year,
                             orderdatemonth = ord.Key.Month,
                             customers = string.Join(",", ord.Select(ord => ord.CustomerId))
                         })
                         .OrderBy(ord => ord.orderdatemonth);
        }

        public IEnumerable<object> GetOrderCountPerMonthByYear(int year)
        {
            return DBData.Orders
                         .Where(ord => ord.OrderDateTime.Year == year)
                         .SelectMany(product => product.Products, (ord, product) => new
                         {
                             ordermonth = ord.OrderDateTime.ToString("MMMM"),
                             productId = product.ProductID,
                             productName = product.Title,
                         })
                        .GroupBy(group => new { group.productId, group.productName })
                         .Select(sel => new
                         {
                             sel.Key.productId,
                             sel.Key.productName,
                             monthlyOrders = sel.GroupBy(x => new { x.ordermonth })
                                       .ToDictionary(
                                         x => $"{x.Key.ordermonth}",
                                         x => x.Count()
                                        )
                         });
        }

        public IEnumerable<object> GetTotalNoOfOrderPlacedPerMonthInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.Orders
                .Where(get => get.OrderDateTime > fromDate)
                .SelectMany(prod => prod.Products, (ord, prod) => new
                {
                    productId = prod.ProductID,
                    productName = prod.Title,
                    OrderMonth = ord.OrderDateTime.ToString("MMMM")
                })
                .GroupBy(group => new { group.productId, group.productName })
                .Select(sel => new
                {
                    sel.Key.productId,
                    sel.Key.productName,
                    monthlyOrders = sel.GroupBy(x => new { x.OrderMonth })
                                       .ToDictionary(
                                         x => $"{x.Key.OrderMonth}",
                                         x => x.Count()
                                        )
                });
        }

    }
}
