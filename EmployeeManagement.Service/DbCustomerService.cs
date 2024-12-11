using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class DbCustomerService : IDbCustomerService
    {

        public IEnumerable<object> GetPurchaseAndOrderInfo(string customerId)
        {
            return DBData.Purchases
                    .Join(DBData.Orders, purchase => purchase.CustomerId,
                    order => order.CustomerId, (purchase, order) => new
                    {
                        purchase.CustomerId,
                        purchase.Amount,
                        purchase.PurchaseDate,
                        order.OrderDateTime,
                        order.OrderNo,
                        Product = order.Products
                    });
        }

        public IEnumerable<object> GetLast3MonthsCustomerOrderInfoOfPurchases(string customerId)
        {
            var fromDatePurchase = DateTime.Now.AddMonths(-3);

            return DBData.Purchases
                    .Where(pur => pur.CustomerId == customerId
                          && pur.PurchaseDate > fromDatePurchase)
                    .Join(DBData.Orders.Where(ord => ord.CustomerId == customerId),
                          purchase => purchase.CustomerId, order => order.CustomerId,
                          (purchase, order) => new
                          {
                              order.OrderNo,
                              order.OrderDateTime
                          });
        }

        public IEnumerable<object> GetLast3MonthsCustomerPurchaseInfo(string customerId)
        {
            var fromDatePurchase = DateTime.Now.AddMonths(-3);

            return DBData.Purchases
                   .Where(pur => pur.CustomerId == customerId
                             && pur.PurchaseDate > fromDatePurchase)
                   .Select(purchase => new
                   {
                       purchase.Amount,
                       purchase.PurchaseDate
                   })
                   .OrderBy(result => result.PurchaseDate.Month);
        }

        public IEnumerable<object> GetCustomerLoyaltyTiers()
        {
            return DBData.CustomerActivities
                   .GroupBy(group1 => new
                   {
                       group1.ActivityDate.Year,
                       group1.CustomerId
                   })
                   .Select(result => new
                   {
                       year = result.Key.Year,
                       customer = result.Key.CustomerId,
                       loyaltytier = GetLoyaltyTier(result)
                   });
        }

        public IEnumerable<object> GetUniqueCustomerInteractedInLast3Month()
        {
            var fromDatePurchase = DateTime.Now.AddMonths(-3);

            return DBData.CustomerActivities
                    .Where(pur => pur.ActivityDate > fromDatePurchase)
                    .Select(purchase => new
                    {
                        purchase.CustomerId
                    })
                    .Distinct();
        }

        public object GetMostActiveCustomerInLast3Month()
        {
            var fromDatePurchase = DateTime.Now.AddMonths(-3);

            return DBData.CustomerActivities
                       .Where(pur => pur.ActivityDate > fromDatePurchase)
                       .GroupBy(purchase => new
                       {
                           purchase.CustomerId,
                       })
                       .Select(cus => new { cus.Key.CustomerId, activecount = cus.Count() })
                       .MaxBy(active => active.activecount)!
                       ;
        }

        public static string GetLoyaltyTier(IGrouping<object, CustomerActivity> loyalty)
        {
            //            -Platinum(12 or more purchases per year)
            //#           - Gold (6 to 11 purchases per year)
            //#           - Silver (1 to 5 purchases per year).

            int itemCount = loyalty.Count();
            if (itemCount < 12)
            {
                if (itemCount < 6)
                {
                    return "Silver";
                }
                else if (itemCount <= 0)
                {
                    return "None";
                }
                else
                {
                    return "Gold";
                }
            }
            else
            {
                return "Platinum";
            }

        }

    }
}
