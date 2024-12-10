using Common;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public interface IDbPurchaseService
    {
        IEnumerable<Purchase> GetAllPurchase();
        double GetAveragePurchasesMadeOnEachDaysOfYear(int year);
        IEnumerable<object> GetCustomerAveragePurchaseAmtByMonthOfYear(int year);
        IEnumerable<object> GetCustomerExpenditureByYear(int year);
        IEnumerable<string> GetCustomerMadePurchasesInLast6MonthOfYear(int year);
        IEnumerable<object> GetCustomerMadePurchasesInYear(int year);
        int GetCustomerMedianPurchaseAmtInLast3Month();
        IEnumerable<object> GetCustomerMinMaxAveragePurchaseAmt();
        IEnumerable<object> GetCustomerPurchasHistory(string customerId);
        IEnumerable<object> GetHighestPurchasesMadeInDayOfWeekOfYear(int year);
        IEnumerable<object> GetLowestPurchasesMadeInDayOfWeekOfYear(int year);
        IEnumerable<Purchase> GetPurchaseByCustomerID(string customerId);
        IEnumerable<object> GetTopCustomerWithHighExpenditureByMonthOfYear(int year);
        int GetTotalPurchasesMadeInMonthOfYear(int month, int year, Helpers.PartOfDay partOfDay);
        IEnumerable<object> GetTotalPurchasesMadeOnEachDaysInLast3Months();
        IEnumerable<object> GetTotalPurchasesMadeOnEachDaysOfYear(int year);
        int GetTotalPurchasesMadeOnWeekDaysOfYear(int year);
        int GetTotalPurchasesMadeOnWeekendfYear(int year);
    }
}