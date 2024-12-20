﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EmployeeManagement.Data;

namespace EmployeeManagement.Service
{
    public class ExternalApiService(HttpClient httpClient) : IDisposable
    {
        private bool disposedValue;

        public async Task<IEnumerable<ProductExternal>> GetProductsAsync(string limit)
        {
            var response = await httpClient.GetAsync($"?limit={limit}");

            response.EnsureSuccessStatusCode();

            var responseResult = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonSerializer.Deserialize<IEnumerable<ProductExternal>>(responseResult);

            return jsonObject;

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
                httpClient?.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ExternalApiService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
