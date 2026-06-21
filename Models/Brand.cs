namespace MyWebApp.Models;
public class Brand {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Country { get; set; }
    public List<Shoe> Shoes { get; set; } = new();
}