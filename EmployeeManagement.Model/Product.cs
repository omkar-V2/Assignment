namespace EmployeeManagement.Data
{
    public class Product
    {
        private string productID;
        private string title;
        private double price;
        private string category;

        public Product(string productID, string title, double price, string category)
        {
            this.ProductID = productID;
            this.Title = title;
            this.Price = price;
            this.Category = category;
        }

        public string ProductID { get => productID; set => productID = value; }
        public string Title { get => title; set => title = value; }
        public double Price { get => price; set => price = value; }
        public string Category { get => category; set => category = value; }
    }
}