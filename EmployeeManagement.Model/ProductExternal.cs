using System.Text.Json.Serialization;

namespace EmployeeManagement.Data
{
    public class ProductExternal
    {
        //[JsonPropertyName("id")]
        //public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        // Add other properties as needed
    }

    public class ProductCategoryExternal
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }

    }
}