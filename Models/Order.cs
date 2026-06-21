using Microsoft.AspNetCore.Identity;

namespace MyWebApp.Models
{
    public enum OrderStatus
    {
        New, Processed, Shipped, Completed, Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }

        public string UserId { get; set; } = string.Empty;
        public IdentityUser? User { get; set; } 

        public string DeliveryType { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}