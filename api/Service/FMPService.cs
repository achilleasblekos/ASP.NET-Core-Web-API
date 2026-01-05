using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private HttpClient _httpClient;
        private IConfiguration _config;
        public FMPService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }
        public async Task<Stock?> FindStockBySymbolAsync(string symbol)
        {
            var apiKey = _config["FMPKey"];
            if (string.IsNullOrWhiteSpace(apiKey))
                return null;

            var url =
                $"https://financialmodelingprep.com/stable/profile?symbol={Uri.EscapeDataString(symbol)}&apikey={apiKey}";

            using var response = await _httpClient.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"FMP call failed: {(int)response.StatusCode} {response.StatusCode}");
                Console.WriteLine($"Body: {body}");
                return null;
            }

            // stable/profile typically returns a list/array as well
            var fmpStocks = JsonConvert.DeserializeObject<List<FMPStock>>(body);
            return fmpStocks?.FirstOrDefault()?.ToStockFromFMP();
        }


    }
}