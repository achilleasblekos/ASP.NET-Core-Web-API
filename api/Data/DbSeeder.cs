using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace api.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services, IHostEnvironment env, ILogger logger)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

            await db.Database.MigrateAsync();

            if (await db.Stocks.AnyAsync())
            {
                logger.LogInformation("Seed skipped: Stocks already exist.");
                return;
            }

            db.Stocks.AddRange(
                new Stock
                {
                    Symbol = "AAPL",
                    CompanyName = "Apple Inc.",
                    PurchasePrice = 150.00m,
                    LastDiv = 0.24m,
                    Industry = "Technology",
                    MarketCap = 3000000000000
                },
                new Stock
                {
                    Symbol = "MSFT",
                    CompanyName = "Microsoft Corporation",
                    PurchasePrice = 320.00m,
                    LastDiv = 0.75m,
                    Industry = "Technology",
                    MarketCap = 2800000000000
                },
                new Stock
                {
                    Symbol = "GOOGL",
                    CompanyName = "Alphabet Inc.",
                    PurchasePrice = 130.00m,
                    LastDiv = 0.00m,
                    Industry = "Communication Services",
                    MarketCap = 1700000000000
                },
                new Stock
                {
                    Symbol = "AMZN",
                    CompanyName = "Amazon.com, Inc.",
                    PurchasePrice = 140.00m,
                    LastDiv = 0.00m,
                    Industry = "Consumer Discretionary",
                    MarketCap = 1600000000000
                },
                new Stock
                {
                    Symbol = "TSLA",
                    CompanyName = "Tesla, Inc.",
                    PurchasePrice = 200.00m,
                    LastDiv = 0.00m,
                    Industry = "Consumer Discretionary",
                    MarketCap = 800000000000
                }
            );

            await db.SaveChangesAsync();
            logger.LogInformation("Seed completed: inserted initial Stocks.");
        }
    }
}
