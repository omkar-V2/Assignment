using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class DbInquiryService : IDbInquiryService
    {
        public object GetProductCategoryWithHighestNoOfInquiryInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlyInquiries
                          .Where(get => get.InquiryDate > fromDate)
                          .GroupBy(group => new
                          {
                              product = group.CategoryName
                          })
                          .Select(inq1 => new
                          {
                              inq1.Key.product,
                              Quantity = inq1.Sum(quant => quant.Quantity)
                          })
                          .MaxBy(inq => inq.Quantity)!;
        }

        public IEnumerable<object> GetAllCategoryTotalNoOfInquiryInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlyInquiries
                        .Where(get => get.InquiryDate > fromDate)
                        .GroupBy(group => new
                        {
                            product = group.CategoryName
                        })
                        .Select(inq1 => new
                        {
                            category = inq1.Key.product,
                            Quantity = inq1.Sum(quant => quant.Quantity)
                        });
        }

        public object? GetDateWithHighestNoOfInquiryInYear(int year)
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlyInquiries
                            .Where(get => get.InquiryDate.Year == year)
                            .GroupBy(group => new
                            {
                                InqDate = group.InquiryDate.Date
                            })
                            .Select(inq1 => new
                            {
                                Date = inq1.Key.InqDate,
                                QuantityNoOfinquiry = inq1.Sum(high => high.Quantity)
                            })
                            .MaxBy(inq1 => inq1.QuantityNoOfinquiry);

        }
    }
}
