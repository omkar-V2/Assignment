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
                             orderyear = ord.OrderDateTime.Year,
                             ordermonth = ord.OrderDateTime.Month,
                             orderno = ord.OrderNo,
                             product = product.Title
                         })
                         .GroupBy(group1 => new
                         {
                             group1.orderyear,
                             group1.ordermonth
                         })
                         .Select(result => new
                         {
                             year = result.Key.orderyear,
                             month = result.Key.ordermonth,
                             product = result.GroupBy(prd => prd.product)
                                      .Select(ord => new { Title = ord.Key, ProdCount = ord.Count() }),
                             totalproductcount = result.Count()
                         })
                        .OrderBy(ord => ord.month);
        }

    }
}
