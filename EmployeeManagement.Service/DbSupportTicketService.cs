using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class DbSupportTicketService : IDbSupportTicketService
    {
        public IEnumerable<object> GetDuplicateSupportActivity()
        {
            return DBData.MonthlySupportTickets
                 .GroupBy(supp => new { supp.Category, supp.SupportDateTime.Month })
                 .Select(supp => new
                 {
                     ticketname = supp.Key.Category,
                     month = supp.Key.Month,
                     duplicatecount = supp.Count()
                 });
        }

        public double GetSupportTicketsAverageInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlySupportTickets
                 .Where(supp => supp.SupportDateTime > fromDate)
                 .GroupBy(group => group.SupportDateTime.Month)
                 .Select(sup => new
                 {
                     month = sup.Key,
                     ticket = sup.Count()
                 })
                 .Average(tic => tic.ticket);
        }

        public IEnumerable<object> GetSupportTicketsTotalNoPerCategoryInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlySupportTickets
                   .Where(supp => supp.SupportDateTime > fromDate)
                   .GroupBy(group => group.Category)
                    .Select(sup1 => new
                    {
                        category = sup1.Key,
                        ticketCount = sup1.Count()
                    });
        }

        public int GetSupportTicketsTotalInLast3Month()
        {
            var fromDate = DateTime.Now.AddMonths(-3);

            return DBData.MonthlySupportTickets
                   .Count(supp => supp.SupportDateTime > fromDate);
        }

        public double GetSupportTicketsAveragePerMonth()
        {
            return DBData.MonthlySupportTickets
                    .GroupBy(group => group.SupportDateTime.Month)
                    .Select(sup => new
                    {
                        month = sup.Key,
                        ticket = sup.Count()
                    }).Average(tic => tic.ticket);
        }

        public object GetHighestNoSupportTicketsMonthOfYear(int year)
        {
            return DBData.MonthlySupportTickets
                      .Where(tick => tick.SupportDateTime.Year == year)
                      .GroupBy(supp => supp.SupportDateTime.Month)
                      .Select(supp => new
                      {
                          ticketmonth = supp.Key,
                          ticketcount = supp.Count()
                      })
                      .MaxBy(high => high.ticketcount)!;
        }

        public IEnumerable<object> GetMajorSupportTicketCountAndStatus()
        {
            return DBData.MonthlySupportTickets
                    .GroupBy(supp => new { supp.Category }) //, supp.SupportDateTime.Month 
                      .Select(supp => new
                      {
                          categoryname = supp.Key.Category,
                          //ticketmonth = supp.Key.Month,
                          occurrence = supp.Count(),
                          critical = GetStatus(supp)
                      });

        }

        private static bool GetStatus(IGrouping<object, SupportTicket> supp)
        {
            int itemCount = supp.Count();


            if (itemCount > 70)
            {
                return true;
            }
            else
            {
                return false;
            }

            //if (itemCount <= 10)
            //{
            //    return itemCount == 10 ? "Major" : "Minor";
            //}
            //else
            //{
            //    return "Critical";
            //}
        }


    }

}
