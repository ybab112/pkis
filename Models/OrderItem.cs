namespace MyWebApp.Models;
public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;
    
    public int ShoeId { get; set; }
    public Shoe Shoe { get; set; } = null!;
    public string Size { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal PriceAtOrder { get; set; }
}