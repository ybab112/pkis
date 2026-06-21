using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using MyWebApp.Data;

namespace MyWebApp.Services
{
    public class AnalyticsService
    {
        private readonly AppDbContext _context;

        public AnalyticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.Today;
            
            return await _context.Set<Order>()
                .Where(o => o.OrderDate.Date == today && o.Status != OrderStatus.Cancelled)
                .SelectMany(o => o.OrderItems)
                .SumAsync(oi => oi.Quantity * oi.PriceAtOrder);
        }

        public async Task<Dictionary<string, int>> GetWeeklySalesDataAsync()
        {
            var weekAgo = DateTime.Today.AddDays(-7);

            var sales = await _context.Set<Order>()
                .Where(o => o.OrderDate >= weekAgo && o.Status != OrderStatus.Cancelled)
                .SelectMany(o => o.OrderItems)
                .GroupBy(oi => oi.Order.OrderDate.Date)
                .Select(g => new 
                { 
                    Date = g.Key, 
                    TotalQuantity = g.Sum(oi => oi.Quantity) 
                })
                .ToListAsync();

            return sales.ToDictionary(s => s.Date.ToString("dd.MM"), s => s.TotalQuantity);
        }
    }
}