
namespace EmployeeManagement.Data
{
    public class Order
    {
        public Order(string orderNo, string customerId, List<Product> products, DateTime orderDateTime)
        {
            this.OrderNo = orderNo;
            this.CustomerId = customerId;
            this.Products = products;
            this.OrderDateTime = orderDateTime;
        }

        public string OrderNo { get; set; }
        public string CustomerId { get; set; }
        public List<Product> Products { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}