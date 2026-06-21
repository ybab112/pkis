using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using MyWebApp.Data;

namespace MyWebApp.Services
{
    public class CartItem
    {
        public Shoe Shoe { get; set; } = null!;
        public int Quantity { get; set; }
        public int Size { get; set; }
    }
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(string userId, List<CartItem> cartItems)
        {
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.New,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ShoeId = item.Shoe.Id,
                    Quantity = item.Quantity,
                    PriceAtOrder = item.Shoe.Price 
                };
                order.OrderItems.Add(orderItem);
            }

            _context.Set<Order>().Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetOrderTotalAsync(int orderId)
        {
            return await _context.Set<OrderItem>()
                .Where(oi => oi.OrderId == orderId)
                .SumAsync(oi => oi.Quantity * oi.PriceAtOrder);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Set<Order>()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Shoe)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}