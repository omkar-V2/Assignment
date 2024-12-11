using Common;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class DbSaleService : IDbSaleService
    {

        public IEnumerable<Sale> GetAllMonthlySales()
        {
            return DBData.MonthlySales;
        }

        public IEnumerable<Sale> GetProductMonthlySales(string ProductName)
        {
            return DBData.MonthlySales.Where(sale => sale.ProductName == ProductName);
        }

        public IEnumerable<object> GetTop3ProductsWithHighSalesForFirst3MonthsOfYear(int year)
        {
            return DBData.MonthlySales
                 .Where(sale => sale.SaleDate.Year == year && sale.SaleDate.Month <= 3)
                 .GroupBy(group => new { group.ProductName })
                 .Select(customergroup => new
                 {
                     Product = customergroup.Key.ProductName,
                     TotalAmnt = customergroup.Sum(group => group.QuantitySold)
                 }).Take(3);
        }

        public int GetTotalSalesOfYearForMonth(int year, int month)
        {
            return DBData.MonthlySales
                    .Where(sale => sale.SaleDate.Year == year
                           && sale.SaleDate.Month == month)
                    .Sum(quantity => quantity.QuantitySold);
        }

        public object? GetMostPopularProductInLast6MonthOfYear(int year)
        {
            var fromDate = new DateTime(year, 12, 31).AddMonths(-6);

            return DBData.MonthlySales
                    .Where(sale => sale.SaleDate.Year == year && sale.SaleDate > fromDate)
                    .GroupBy(gr => gr.ProductName)
                    .Select(sales => new
                    {
                        Product = sales.Key,
                        TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                    })
                    .OrderByDescending(pop => pop.TotalAmnt)
                    .FirstOrDefault();
        }

        public object? GetLeastPopularProductInLast6MonthOfYear(int year)
        {
            var fromDate = new DateTime(year, 12, 31).AddMonths(-6);

            return DBData.MonthlySales
                               .Where(sale => sale.SaleDate.Year == year
                                      && sale.SaleDate > fromDate)
                               .GroupBy(gr => gr.ProductName)
                               .Select(sales => new
                               {
                                   Product = sales.Key,
                                   TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                               })
                               .OrderBy(pop => pop.TotalAmnt)
                               .FirstOrDefault();
        }

        public object GetMostSoldProductByYear(int year)
        {
            return DBData.MonthlySales
                   .Where(sale => sale.SaleDate.Year == year)
                   .GroupBy(gr => gr.ProductName)
                   .Select(sales => new
                   {
                       Product = sales.Key,
                       TotalAmnt = sales.Sum(grp => grp.QuantitySold)
                   })
                   .OrderByDescending(pop => pop.TotalAmnt).First();
        }

        public object GetLeastSoldProductByYear(int year)
        {
            return DBData.MonthlySales
                   .Where(yr => yr.SaleDate.Year == year)
                    .GroupBy(group => new
                    {
                        Year = group.SaleDate.Year,
                        Product = group.ProductName
                    })
                    .Select(sold => new
                    {
                        Year = sold.Key.Year,
                        Product = sold.Key.Product,
                        Sold = sold.Sum(sold => sold.QuantitySold)
                    })
                    .OrderBy(sold => sold.Sold).First();
        }

        public IEnumerable<object> GetTotalProductQuantitySoldByYear(int year)
        {
            return DBData.MonthlySales
                   .Where(yr => yr.SaleDate.Year == year)
                      .GroupBy(group => new
                      {
                          Year = group.SaleDate.Year
                      })
                      .Select(sold => new
                      {
                          Year = sold.Key.Year,
                          TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                      });

        }

        public IEnumerable<object> GetMostSoldProductMonthWiseOfYear(int year)
        {
            return DBData.MonthlySales
                         .Where(yr => yr.SaleDate.Year == year)
                              .GroupBy(group => new
                              {
                                  Year = group.SaleDate.Year,
                                  Month = group.SaleDate.Month,
                                  Product = group.ProductName
                              })
                              .Select(sold => new
                              {
                                  Year = sold.Key.Year,
                                  Month = sold.Key.Month,
                                  Product = sold.Key.Product,
                                  TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                              })
                              .GroupBy(group2 => new
                              {
                                  group2.Year,
                                  group2.Month
                              })
                              .Select(result => result
                                               .OrderByDescending(ord2 => ord2.TotalQuantity).First());
        }

        public Dictionary<string, IEnumerable<object>> GetProductGroupedBySeason(int top)
        {
            if (top <= 0)
            {
                return DBData.MonthlySales
                             .GroupBy(group => Helpers.GetSeason(group.SaleDate))
                             .Select(result => new
                             {
                                 Season = result.Key,
                                 ProductSales = result.GroupBy(group1 => group1.ProductName)
                                 .Select(newresult => new
                                 {
                                     product = newresult.Key,
                                     sales = newresult.Sum(quan => quan.QuantitySold)
                                 })
                                 .Cast<object>()
                             })
                             .ToDictionary(x => x.Season, x => x.ProductSales);
            }
            else
            {
                return DBData.MonthlySales
                             .GroupBy(group => Helpers.GetSeason(group.SaleDate))
                             .Select(result => new
                             {
                                 Season = result.Key,
                                 ProductSales = result.GroupBy(group1 => group1.ProductName)
                                 .Select(newresult => new
                                 {
                                     product = newresult.Key,
                                     sales = newresult.Sum(quan => quan.QuantitySold)
                                 })
                                 .OrderByDescending(result => result.sales)
                                 .Take(top)
                                 .Cast<object>()
                             })
                             .ToDictionary(x => x.Season, x => x.ProductSales);
            }
        }

        public IEnumerable<object> GetAveargeQuanOfProductSoldMonthWiseOfYear(int year)
        {
            return DBData.MonthlySales
                    .Where(yr => yr.SaleDate.Year == year)
                    .GroupBy(group => new
                    {
                        Year = group.SaleDate.Year,
                        Month = group.SaleDate.Month
                    })
                    .Select(sold => new
                    {
                        Year = sold.Key.Year,
                        Month = sold.Key.Month,
                        TotalAverage = sold.Average(sold => sold.QuantitySold)
                    });

        }

        public IEnumerable<object> GetAggregateOfEachProductSoldOfYear(int year)
        {
            return DBData.MonthlySales
                  .Where(yr => yr.SaleDate.Year == year)
                   .GroupBy(group => new
                   {
                       Year = group.SaleDate.Year,
                       Product = group.ProductName
                   })
                   .Select(sold => new
                   {
                       Year = sold.Key.Year,
                       Product = sold.Key.Product,
                       TotalSum = sold.Sum(sold => sold.QuantitySold),
                       TotalAverage = sold.Average(sold => sold.QuantitySold),
                       Min = sold.Min(sold => sold.QuantitySold),
                       Max = sold.Max(sold => sold.QuantitySold),
                       Count = sold.Count()
                   });
        }

        public IEnumerable<object> GetLeastSoldProductMonthWiseOfYear(int year)
        {
            return DBData.MonthlySales
                   .Where(yr => yr.SaleDate.Year == year)
                   .GroupBy(group => new
                   {
                       Year = group.SaleDate.Year,
                       Month = group.SaleDate.Month,
                       Product = group.ProductName
                   })
                   .Select(sold => new
                   {
                       Year = sold.Key.Year,
                       Month = sold.Key.Month,
                       Product = sold.Key.Product,
                       TotalQuantity = sold.Sum(sold => sold.QuantitySold)
                   })
                   .GroupBy(group2 => new
                   {
                       group2.Year,
                       group2.Month
                   })
                   .Select(result => result
                  .OrderBy(ord2 => ord2.TotalQuantity).First());
        }


    }
}
