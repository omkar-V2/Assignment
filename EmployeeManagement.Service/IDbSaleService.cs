using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public interface IDbSaleService
    {
        IEnumerable<object> GetAggregateOfEachProductSoldOfYear(int year);
        IEnumerable<object> GetAveargeQuanOfProductSoldMonthWiseOfYear(int year);
        object GetLeastPopularProductInLast6MonthOfYear(int year);
        object GetLeastSoldProductByYear(int year);
        IEnumerable<object> GetLeastSoldProductMonthWiseOfYear(int year);
        object GetMostPopularProductInLast6MonthOfYear(int year);
        object GetMostSoldProductByYear(int year);
        IEnumerable<object> GetMostSoldProductMonthWiseOfYear(int year);
        IEnumerable<object> GetProductGroupedBySeason(int top);
        IEnumerable<Sale> GetProductMonthlySales(string ProductName);
        IEnumerable<Sale> GetAllMonthlySales();

        IEnumerable<object> GetTop3ProductsWithHighSalesForFirst3MonthsOfYear(int year);
        IEnumerable<object> GetTotalProductQuantitySoldByYear(int year);
        int GetTotalSalesOfYearForMonth(int year, int month);
    }
}