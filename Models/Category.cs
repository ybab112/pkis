namespace MyWebApp.Models;
public class Category {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<Shoe> Shoes { get; set; } = new();
}