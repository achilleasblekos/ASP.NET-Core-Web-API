using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock?> AddStockAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStockAsync(int id)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public async Task<Stock?> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }


        public async Task<List<Stock>> GetStocksAsync(StockQueryParameters queryParameters)
        {
            var stocks = _context.Stocks
                .Include(c => c.Comments)
                .ThenInclude(c => c.AppUser)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryParameters.CompanyName))
            {
                stocks = stocks.Where(s => s.CompanyName!.Contains(queryParameters.CompanyName));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.Symbol))
            {
                stocks = stocks.Where(s => s.Symbol!.Contains(queryParameters.Symbol));
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.SortBy) &&
                queryParameters.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
            {
                stocks = queryParameters.IsDecsending
                    ? stocks.OrderByDescending(s => s.Symbol)
                    : stocks.OrderBy(s => s.Symbol);
            }
            else
            {
                stocks = stocks.OrderBy(s => s.Id);
            }

            var skipNumber = (queryParameters.PageNumber - 1) * queryParameters.PageSize;

            return await stocks
                .Skip(skipNumber)
                .Take(queryParameters.PageSize)
                .ToListAsync();
        }


        public Task<bool> StockExistsAsync(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStockAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stocks.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }

            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.PurchasePrice = stockDto.PurchasePrice;
            stockModel.LastDiv = stockDto.LastDiv;
            stockModel.Industry = stockDto.Industry;
            stockModel.MarketCap = stockDto.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }

    }
}