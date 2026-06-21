namespace MyWebApp.Models;

public class Shoe 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;    
    public int BrandId { get; set; }
    public Brand? Brand { get; set; }    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public string? ImagePath { get; set; }
    public string Gender { get; set; } = "Мужской";
    public string Material { get; set; } = "Кожа";   
    public string Season { get; set; } = "Лето";     
    public List<ShoeSize> ShoeSizes { get; set; } = new();
}

public class ShoeSize
{
    public int Id { get; set; }
    public string Size { get; set; } = string.Empty; 
    public int Quantity { get; set; } 
    public int ShoeId { get; set; }
    public Shoe? Shoe { get; set; }
}