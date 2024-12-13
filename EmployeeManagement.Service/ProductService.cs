using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductCategoryExternal>> GetProductsByCategoryAsync(string jewelery)
        {
            var response = await _httpClient.GetAsync($"{jewelery}");

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonSerializer.Deserialize<IEnumerable<ProductCategoryExternal>>(responseResult);

            return jsonObject;
        }

    }
}
