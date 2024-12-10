using System.Linq;
using System.Net;
using Common;
using EmployeeManagement.Data;
using static Common.Helpers;

namespace EmployeeManagement.Service
{
    public class DbPurchaseService : IDbPurchaseService
    {
        public IEnumerable<Purchase> GetAllPurchase()
        {
            return DBData.Purchases;
        }

        public IEnumerable<Purchase> GetPurchaseByCustomerID(string customerId)
        {
            return DBData.Purchases.Where(pur => pur.CustomerId == customerId);
        }

        public IEnumerable<object> GetCustomerExpenditureByYear(int year)
        {
            return DBData.Purchases
                .Where(pur => pur.PurchaseDate.Year == year)
                .GroupBy(group => group.CustomerId)
                .Select(customergroup => new
                {
                    customer = customergroup.Key,
                    TotalAmount = customergroup.Sum(pur => pur.Amount)
                });
        }

        public IEnumerable<object> GetTopCustomerWithHighExpenditureByMonthOfYear(int year)
        {
            var result = DBData.Purchases
                            .Where(pur => pur.PurchaseDate.Year == year)
                            .GroupBy(group => new { group.PurchaseDate.Month, group.CustomerId })
                            .Select(customergroup => new
                            {
                                Month = customergroup.Key.Month,
                                Customer = customergroup.Key.CustomerId,
                                TotalAmnt = customergroup.Sum(group => group.Amount)
                            })
                            .GroupBy(month => month.Month)
                            .Select(g => g.OrderByDescending(t => t.TotalAmnt).First())
                            .OrderBy(ord => ord.Month);

            return result;
        }

        public IEnumerable<object> GetCustomerPurchasHistory(string customerId)
        {
            return DBData.Purchases
                               .Where(cust => cust.CustomerId == customerId)
                               .OrderBy(cus => cus.PurchaseDate);
        }

        public IEnumerable<object> GetCustomerMinMaxAveragePurchaseAmt()
        {
            return DBData.Purchases
                             .GroupBy(group => group.CustomerId)
                             .Select(cus => new
                             {
                                 Customer = cus.Key,
                                 Min = cus.Min(cus => cus.Amount),
                                 Max = cus.Max(cus => cus.Amount),
                                 Average = cus.Average(cus => cus.Amount)
                             });
        }

        public IEnumerable<object> GetCustomerAveragePurchaseAmtByMonthOfYear(int year)
        {
            return DBData.Purchases
                         .GroupBy(group => new
                         {
                             Year = group.PurchaseDate.Year,
                             Month = group.PurchaseDate.Month
                         })
                         .Select(cus => new
                         {
                             cus.Key.Year,
                             cus.Key.Month,
                             Average = cus.Average(cus => cus.Amount)
                         });
        }

        public int GetCustomerMedianPurchaseAmtInLast3Month()
        {
            var fromDatePurchase = DateTime.Now.AddMonths(-3);

            return Helpers.GetMedianAmt(DBData.Purchases
                                               .Where(pur => pur.PurchaseDate > fromDatePurchase)
                                               .Select(pur1 => pur1.Amount));
        }

        public IEnumerable<string> GetCustomerMadePurchasesInLast6MonthOfYear(int year)
        {
            var fromDatePurchase = new DateTime(year, 12, 31).AddMonths(-6);

            return DBData.Purchases
                         .Where(pur => pur.PurchaseDate > fromDatePurchase)
                         .DistinctBy(pur1 => pur1.CustomerId)
                         .Select(cus => cus.CustomerId);
        }

        public IEnumerable<object> GetCustomerMadePurchasesInYear(int year)
        {
            return DBData.Purchases
                         .Where(pur => pur.PurchaseDate.Year == year)
                         .DistinctBy(pur => pur.CustomerId)
                         .Select(cus => cus.CustomerId);
        }

        public IEnumerable<object> GetTotalPurchasesMadeOnEachDaysOfYear(int year)
        {
            return DBData.Purchases
                           .Where(pur => pur.PurchaseDate.Year == year)
                           .GroupBy(group => group.PurchaseDate.DayOfWeek)
                           .Select(record => new
                           {
                               Day = record.Key,
                               Count = record.Count()
                           });
        }

        public IEnumerable<object> GetTotalPurchasesMadeOnEachDaysInLast3Months()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.Purchases
                         .Where(pur => pur.PurchaseDate > fromDate)
                         .GroupBy(group => group.PurchaseDate.DayOfWeek)
                         .Select(record => new
                         {
                             Day = record.Key,
                             Count = record.Count()
                         });

        }

        public double GetAveragePurchasesMadeOnEachDaysOfYear(int year)
        {
            return DBData.Purchases
                         .Where(pur => pur.PurchaseDate.Year == year)
                         .GroupBy(group => group.PurchaseDate.DayOfWeek)
                         .Select(record => new
                         {
                             Day = record.Key,
                             DayPurchaseCount = record.Count()
                         })
                         .Average(avg => avg.DayPurchaseCount);
        }

        public int GetTotalPurchasesMadeOnWeekDaysOfYear(int year)
        {
            return DBData.Purchases
                        .Count(pur => pur.PurchaseDate.Year == year
                        && !(pur.PurchaseDate.DayOfWeek == DayOfWeek.Saturday || pur.PurchaseDate.DayOfWeek == DayOfWeek.Sunday));
        }

        public int GetTotalPurchasesMadeOnWeekendfYear(int year)
        {
            return DBData.Purchases
                        .Count(pur => pur.PurchaseDate.Year == year
                        && (pur.PurchaseDate.DayOfWeek == DayOfWeek.Saturday || pur.PurchaseDate.DayOfWeek == DayOfWeek.Sunday));
        }

        public IEnumerable<object> GetHighestPurchasesMadeInDayOfWeekOfYear(int year)
        {
            var rawResult = DBData.Purchases
                                  .Where(ord => ord.PurchaseDate.Year == year)
                                  .GroupBy(group => group.PurchaseDate.DayOfWeek)
                                  .Select(result => new
                                  {
                                      Day = result.Key,
                                      PurchaseCount = result.Count()
                                  });
            var finalResult = rawResult.Where(result => result.PurchaseCount == rawResult.Max(purcount => purcount.PurchaseCount));

            return finalResult;
        }


        public IEnumerable<object> GetLowestPurchasesMadeInDayOfWeekOfYear(int year)
        {

            var rawResult = DBData.Purchases
                            .Where(ord => ord.PurchaseDate.Year == year)
                            .GroupBy(group => group.PurchaseDate.DayOfWeek)
                            .Select(result => new
                            {
                                Day = result.Key,
                                PurchaseCount = result.Count()
                            });

            var finalResult = rawResult.Where(result => result.PurchaseCount == rawResult.Min(purcount => purcount.PurchaseCount));

            return finalResult;
        }

        public int GetTotalPurchasesMadeInMonthOfYear(int month, int year, PartOfDay partOfDay)
        {

            return DBData.Purchases
                             .Count(pur => pur.PurchaseDate.Year == year
                              && pur.PurchaseDate.Month == month
                              && GetTimeOftheDay(pur.PurchaseDate.TimeOfDay) == partOfDay);
        }
    }
}
