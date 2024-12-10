using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class DbService
    {
        public IEnumerable<CustomerActivity> GetAllCustomerActivity()
        {
            return DBData.CustomerActivities;
        }

        public IEnumerable<Inquiry> GetAllInquiry()
        {
            return DBData.MonthlyInquiries;
        }

        public IEnumerable<Order> GetAllOrderIQ()
        {
            return DBData.Orders.AsQueryable();
        }
        public IEnumerable<Order> GetAllOrder()
        {
            return DBData.Orders;
        }

        public IEnumerable<Order> GetAllOrder(Func<Order, bool> selector)
        {
            return DBData.Orders.Where(selector);
        }

        public IEnumerable<Purchase> GetAllPurchase()
        {
            return DBData.Purchases;
        }

        public IEnumerable<Sale> GetAllSale()
        {
            return DBData.MonthlySales;
        }

        public IEnumerable<SupportTicket> GetAllSupportTicket()
        {
            return DBData.MonthlySupportTickets;
        }

    }

}
