using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Data
{
    public static class DBData
    {
        public static readonly List<Sale> MonthlySales =
        [
        // Sales data from January to September 2023
        // January 2023
        new Sale { ProductName = "Laptop", QuantitySold = 65, SaleDate = new DateTime(2023, 01, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 55, SaleDate = new DateTime(2023, 01, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 30, SaleDate = new DateTime(2023, 01, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 20, SaleDate = new DateTime(2023, 01, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 15, SaleDate = new DateTime(2023, 01, 01) },

        // February 2023
        new Sale { ProductName = "Laptop", QuantitySold = 70, SaleDate = new DateTime(2023, 02, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 45, SaleDate = new DateTime(2023, 02, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 25, SaleDate = new DateTime(2023, 02, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 25, SaleDate = new DateTime(2023, 02, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 14, SaleDate = new DateTime(2023, 02, 01) },

        // March 2023
        new Sale { ProductName = "Laptop", QuantitySold = 55, SaleDate = new DateTime(2023, 03, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 60, SaleDate = new DateTime(2023, 03, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 40, SaleDate = new DateTime(2023, 03, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 30, SaleDate = new DateTime(2023, 03, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 12, SaleDate = new DateTime(2023, 03, 01) },

        // April 2023
        new Sale { ProductName = "Laptop", QuantitySold = 75, SaleDate = new DateTime(2023, 04, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 50, SaleDate = new DateTime(2023, 04, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 35, SaleDate = new DateTime(2023, 04, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 35, SaleDate = new DateTime(2023, 04, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 10, SaleDate = new DateTime(2023, 04, 01) },

        // May 2023
        new Sale { ProductName = "Laptop", QuantitySold = 80, SaleDate = new DateTime(2023, 05, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 70, SaleDate = new DateTime(2023, 05, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 45, SaleDate = new DateTime(2023, 05, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 40, SaleDate = new DateTime(2023, 05, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 9, SaleDate = new DateTime(2023, 05, 01) },

        // June 2023
        new Sale { ProductName = "Laptop", QuantitySold = 85, SaleDate = new DateTime(2023, 06, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 65, SaleDate = new DateTime(2023, 06, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 50, SaleDate = new DateTime(2023, 06, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 45, SaleDate = new DateTime(2023, 06, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 8, SaleDate = new DateTime(2023, 06, 01) },

        // July 2023
        new Sale { ProductName = "Laptop", QuantitySold = 90, SaleDate = new DateTime(2023, 07, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 75, SaleDate = new DateTime(2023, 07, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 55, SaleDate = new DateTime(2023, 07, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 50, SaleDate = new DateTime(2023, 07, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 7, SaleDate = new DateTime(2023, 07, 01) },

        // August 2023
        new Sale { ProductName = "Laptop", QuantitySold = 95, SaleDate = new DateTime(2023, 08, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 80, SaleDate = new DateTime(2023, 08, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 60, SaleDate = new DateTime(2023, 08, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 55, SaleDate = new DateTime(2023, 08, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 6, SaleDate = new DateTime(2023, 08, 01) },

        // September 2023
        new Sale { ProductName = "Laptop", QuantitySold = 100, SaleDate = new DateTime(2023, 09, 01) },
        new Sale { ProductName = "Smartphone", QuantitySold = 85, SaleDate = new DateTime(2023, 09, 01) },
        new Sale { ProductName = "Tablet", QuantitySold = 65, SaleDate = new DateTime(2023, 09, 01) },
        new Sale { ProductName = "Headphones", QuantitySold = 60, SaleDate = new DateTime(2023, 09, 01) },
        new Sale { ProductName = "Watch", QuantitySold = 5, SaleDate = new DateTime(2023, 09, 01) }
        ];

        public static readonly List<Purchase> Purchases =
        [
        new Purchase { CustomerId = "C001", Amount = 1500, PurchaseDate = new DateTime(2023, 01, 15) },
        new Purchase { CustomerId = "C002", Amount = 1000, PurchaseDate = new DateTime(2023, 01, 15) },
        new Purchase { CustomerId = "C001", Amount = 2400, PurchaseDate = new DateTime(2023, 02, 20) },
        new Purchase { CustomerId = "C002", Amount = 2500, PurchaseDate = new DateTime(2023, 02, 20) },
        new Purchase { CustomerId = "C002", Amount = 2500, PurchaseDate = new DateTime(2023, 02, 18) },
        new Purchase { CustomerId = "C001", Amount = 3000, PurchaseDate = new DateTime(2023, 03, 05) },
        new Purchase { CustomerId = "C003", Amount = 700, PurchaseDate = new DateTime(2023, 04, 25) },
        new Purchase { CustomerId = "C002", Amount = 5500, PurchaseDate = new DateTime(2023, 05, 15) },
        new Purchase { CustomerId = "C001", Amount = 3200, PurchaseDate = new DateTime(2023, 06, 10) },
        new Purchase { CustomerId = "C003", Amount = 1000, PurchaseDate = new DateTime(2023, 07, 22) },
        new Purchase { CustomerId = "C004", Amount = 400, PurchaseDate = new DateTime(2023, 08, 30) }
        ];

        public static readonly List<Order> Orders =
        [
            new Order("O001", "C001",
        [
            new Product("P001", "Laptop", 1200.00, "Electronics"),
            new Product("P002", "Smartphone", 700.00, "Electronics")
        ], new DateTime(2023, 06, 01, 20, 30, 00)), // Evening, 2023
    
        new Order("O002", "C002",
        [
            new Product("P003", "Blouse", 80.00, "Apparel"),
            new Product("P004", "Jeans", 60.00, "Apparel"),
            new Product("P002", "Smartphone", 700.00, "Electronics")
        ], new DateTime(2023, 06, 01, 10, 15, 00)), // Morning, 2023
    
        new Order("O003", "C003",
        [
            new Product("P005", "Coffee Maker", 200.00, "Home Goods"),
            new Product("P006", "Toaster", 90.00, "Home Goods")
        ], new DateTime(2023, 11, 21, 15, 45, 00)), // Afternoon, 2023
    
        new Order("O004", "C004",
        [
            new Product("P007", "Gaming Chair", 350.00, "Electronics"),
            new Product("P008", "Gaming Monitor", 700.00, "Electronics"),
            new Product("P009", "Table Lamp", 45.00, "Home Goods")
        ], new DateTime(2024, 01, 05, 21, 00, 00)), // Evening, 2024
    
        new Order("O005", "C005",
        [
            new Product("P010", "Bookshelf", 150.00, "Home Goods"),
            new Product("P011", "Desk", 300.00, "Home Goods")
        ], new DateTime(2024, 03, 15, 11, 30, 00)), // Morning, 2024
    
        new Order("O006", "C006",
        [
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P013", "Socks", 25.00, "Apparel")
        ], new DateTime(2024, 05, 20, 19, 00, 00)), // Evening, 2024
    
        new Order("O007", "C007",
        [
            new Product("P002", "Smartphone", 700.00, "Electronics"),
            new Product("P001", "Laptop", 1200.00, "Electronics"),
            new Product("P014", "Bluetooth Speaker", 90.00, "Electronics"),
            new Product("P015", "Wireless Earbuds", 130.00, "Electronics")
        ], new DateTime(2024, 08, 11, 14, 30, 00)), // Afternoon, 2024
    
        new Order("O008", "C008",
        [
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P016", "Fitness Tracker", 99.00, "Electronics"),
            new Product("P017", "Smart Scale", 130.00, "Electronics")
        ], new DateTime(2023, 09, 30, 12, 15, 00)), // Afternoon, 2023

         new Order("O0014", "C008",
        [
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P016", "Fitness Tracker", 99.00, "Electronics"),
            new Product("P017", "Smart Scale", 130.00, "Electronics")
        ], new DateTime(2023, 09, 30, 12, 15, 00)), // Afternoon, 2023

           new Order("O0015", "C001",
        [
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P016", "Fitness Tracker", 99.00, "Electronics"),
            new Product("P017", "Smart Scale", 130.00, "Electronics")
        ], new DateTime(2023, 09, 30, 12, 15, 00)), // Afternoon, 2023


        new Order("O009", "C009",
        [
            new Product("P007", "Gaming Chair", 350.00, "Electronics"),
            new Product("P008", "Gaming Monitor", 700.00, "Electronics"),
            new Product("P018", "Espresso Machine", 300.00, "Home Goods"),
            new Product("P019", "Coffee Grinder", 150.00, "Home Goods")
        ], new DateTime(2023, 07, 04, 09, 00, 00)), // Morning, 2023
    
        new Order("O010", "C010",
        [
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P020", "Yoga Mat", 50.00, "Fitness"),
            new Product("P021", "Kettlebell", 45.00, "Fitness")
        ], new DateTime(2024, 02, 14, 18, 45, 00)), // Evening, 2024
            new Order("O011", "C001",
        [
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P020", "Yoga Mat", 50.00, "Fitness"),
            new Product("P021", "Kettlebell", 45.00, "Fitness")
        ], new DateTime(2024, 02, 13, 18, 45, 00)), // Evening, 2024
          new Order("O012", "C002",
        [
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P020", "Yoga Mat", 50.00, "Fitness"),
            new Product("P021", "Kettlebell", 45.00, "Fitness")
        ], new DateTime(2024, 02, 24, 18, 45, 00)), // Evening, 2024
          new Order("O013", "C003",
        [
            new Product("P012", "Running Shoes", 120.00, "Apparel"),
            new Product("P013", "Socks", 25.00, "Apparel"),
            new Product("P020", "Yoga Mat", 50.00, "Fitness"),
            new Product("P021", "Kettlebell", 45.00, "Fitness")
        ], new DateTime(2024, 02, 25, 18, 45, 00)) // Evening, 2024
        ];

        public static readonly List<Inquiry> MonthlyInquiries =
        [
         // Inquiries data from January to September 2023
        // January 2023
        new Inquiry { CategoryName = "Electronics", Quantity = 120, InquiryDate = new DateTime(2023, 01, 01) },
        new Inquiry { CategoryName = "Clothing", Quantity = 80, InquiryDate = new DateTime(2023, 01, 01) },
        new Inquiry { CategoryName = "Home Appliances", Quantity = 105, InquiryDate = new DateTime(2023, 01, 01) },
        new Inquiry { CategoryName = "Toys", Quantity = 70, InquiryDate = new DateTime(2023, 01, 01) },
        new Inquiry { CategoryName = "Books", Quantity = 50, InquiryDate = new DateTime(2023, 01, 01) },

        // February 2023
        new Inquiry { CategoryName = "Electronics", Quantity = 115, InquiryDate = new DateTime(2023, 02, 01) },
        new Inquiry { CategoryName = "Clothing", Quantity = 90, InquiryDate = new DateTime(2023, 02, 01) },
        new Inquiry { CategoryName = "Home Appliances", Quantity = 110, InquiryDate = new DateTime(2023, 02, 01) },
        new Inquiry { CategoryName = "Toys", Quantity = 75, InquiryDate = new DateTime(2023, 02, 01) },
        new Inquiry { CategoryName = "Books", Quantity = 55, InquiryDate = new DateTime(2023, 02, 01) },

        // March 2023
        new Inquiry { CategoryName = "Electronics", Quantity = 130, InquiryDate = new DateTime(2023, 03, 01) },
        new Inquiry { CategoryName = "Clothing", Quantity = 85, InquiryDate = new DateTime(2023, 03, 01) },
        new Inquiry { CategoryName = "Home Appliances", Quantity = 120, InquiryDate = new DateTime(2023, 03, 01) },
        new Inquiry { CategoryName = "Toys", Quantity = 60, InquiryDate = new DateTime(2023, 03, 01) },
        new Inquiry { CategoryName = "Books", Quantity = 45, InquiryDate = new DateTime(2023, 03, 01) },

        // April 2023
        new Inquiry { CategoryName = "Electronics", Quantity = 125, InquiryDate = new DateTime(2023, 04, 01) },
        new Inquiry { CategoryName = "Clothing", Quantity = 95, InquiryDate = new DateTime(2023, 04, 01) },
        new Inquiry { CategoryName = "Home Appliances", Quantity = 115, InquiryDate = new DateTime(2023, 04, 01) },
        new Inquiry { CategoryName = "Toys", Quantity = 65, InquiryDate = new DateTime(2023, 04, 01) },
        new Inquiry { CategoryName = "Books", Quantity = 40, InquiryDate = new DateTime(2023, 04, 01) },

        // May 2023
        new Inquiry { CategoryName = "Electronics", Quantity = 125, InquiryDate = new DateTime(2023, 05, 01) },
        new Inquiry { CategoryName = "Clothing", Quantity = 95, InquiryDate = new DateTime(2023, 05, 01) },
        new Inquiry { CategoryName = "Home Appliances", Quantity = 115, InquiryDate = new DateTime(2023, 05, 01) },
        new Inquiry { CategoryName = "Toys", Quantity = 65, InquiryDate = new DateTime(2023, 05, 01) },
        new Inquiry { CategoryName = "Books", Quantity = 40, InquiryDate = new DateTime(2023, 05, 01) }
        ];

        public static readonly List<SupportTicket> MonthlySupportTickets =
        [
            // Flattened support ticket data from January to March 2023
            // January 2023
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 01, 01)),

        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 01, 01)),

        new SupportTicket("Payment Issues", new DateTime(2023, 01, 01)),
        new SupportTicket("Payment Issues", new DateTime(2023, 01, 01)),
        new SupportTicket("Payment Issues", new DateTime(2023, 01, 01)),
        new SupportTicket("Customer Service", new DateTime(2023, 01, 01)),
        new SupportTicket("Customer Service", new DateTime(2023, 01, 01)),

        // February 2023
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 02, 01)),
        new SupportTicket("Payment Issues", new DateTime(2023, 02, 01)),
        new SupportTicket("Customer Service", new DateTime(2023, 02, 01)),

        // March 2023
        new SupportTicket("Delivery Delays", new DateTime(2023, 03, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 03, 01)),
        new SupportTicket("Delivery Delays", new DateTime(2023, 03, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 03, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 03, 01)),
        new SupportTicket("Payment Issues", new DateTime(2023, 03, 01)),
        new SupportTicket("Customer Service", new DateTime(2023, 03, 01)),

        // April 2023 
        new SupportTicket("Delivery Delays", new DateTime(2023, 04, 01)),
        new SupportTicket("Customer Delays", new DateTime(2023, 04, 01)),
        new SupportTicket("Customer Delays", new DateTime(2023, 04, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 04, 01)),
        new SupportTicket("Product Quality", new DateTime(2023, 04, 01)),
        new SupportTicket("Payment Issues", new DateTime(2023, 04, 01)),
        new SupportTicket("Customer Service", new DateTime(2023, 04, 01))
        ];

        public static readonly List<CustomerActivity> CustomerActivities = new()
    {
        new CustomerActivity("C001", new DateTime(2023, 01, 05)),
        new CustomerActivity("C001", new DateTime(2023, 02, 10)),
        new CustomerActivity("C001", new DateTime(2023, 03, 15)),
        new CustomerActivity("C001", new DateTime(2023, 05, 20)),
        new CustomerActivity("C001", new DateTime(2023, 07, 25)),
        new CustomerActivity("C001", new DateTime(2023, 09, 15)),
        new CustomerActivity("C001", new DateTime(2023, 11, 05)),
        new CustomerActivity("C002", new DateTime(2023, 01, 05)),
        new CustomerActivity("C002", new DateTime(2023, 02, 10)),
        new CustomerActivity("C002", new DateTime(2023, 03, 15)),
        new CustomerActivity("C002", new DateTime(2023, 04, 20)),
        new CustomerActivity("C002", new DateTime(2023, 05, 25)),
        new CustomerActivity("C002", new DateTime(2023, 06, 15)),
        new CustomerActivity("C002", new DateTime(2023, 07, 05)),
        new CustomerActivity("C002", new DateTime(2023, 08, 10)),
        new CustomerActivity("C002", new DateTime(2023, 09, 20)),
        new CustomerActivity("C002", new DateTime(2023, 10, 15)),
        new CustomerActivity("C002", new DateTime(2023, 11, 25)),
        new CustomerActivity("C002", new DateTime(2023, 12, 05)),
        new CustomerActivity("C003", new DateTime(2023, 02, 05)),
        new CustomerActivity("C003", new DateTime(2023, 05, 10)),
        new CustomerActivity("C003", new DateTime(2023, 07, 20)),
        new CustomerActivity("C003", new DateTime(2023, 11, 15)),
        new CustomerActivity("C004", new DateTime(2023, 04, 15)),
        new CustomerActivity("C004", new DateTime(2023, 08, 05)),

        new CustomerActivity("C001", new DateTime(2024, 01, 05)),
        new CustomerActivity("C001", new DateTime(2024, 02, 10)),
        new CustomerActivity("C001", new DateTime(2024, 03, 15)),
        new CustomerActivity("C001", new DateTime(2024, 05, 20)),
        new CustomerActivity("C001", new DateTime(2024, 07, 25)),
        new CustomerActivity("C001", new DateTime(2024, 09, 15)),
        new CustomerActivity("C001", new DateTime(2024, 11, 05)),
        new CustomerActivity("C002", new DateTime(2024, 01, 05)),
        new CustomerActivity("C002", new DateTime(2024, 02, 10)),
        new CustomerActivity("C002", new DateTime(2024, 03, 15)),
        new CustomerActivity("C002", new DateTime(2024, 04, 20)),
        new CustomerActivity("C002", new DateTime(2024, 05, 25)),
        new CustomerActivity("C002", new DateTime(2024, 06, 15)),
        new CustomerActivity("C002", new DateTime(2024, 07, 05)),
        new CustomerActivity("C002", new DateTime(2024, 08, 10)),
        new CustomerActivity("C002", new DateTime(2024, 09, 20)),
        new CustomerActivity("C002", new DateTime(2024, 10, 15)),
        new CustomerActivity("C002", new DateTime(2024, 11, 25)),
        new CustomerActivity("C002", new DateTime(2024, 12, 05)),
        new CustomerActivity("C003", new DateTime(2024, 02, 05)),
        new CustomerActivity("C003", new DateTime(2024, 05, 10)),
        new CustomerActivity("C003", new DateTime(2024, 07, 20)),
        new CustomerActivity("C003", new DateTime(2024, 11, 15)),
        new CustomerActivity("C004", new DateTime(2024, 04, 15)),
        new CustomerActivity("C004", new DateTime(2024, 08, 05))

    };
    }
}
