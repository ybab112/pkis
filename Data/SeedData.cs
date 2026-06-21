using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;

namespace MyWebApp.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

        if (context.Shoes.Any()) return;

        var nike = new Brand { Name = "Nike" };
        var ecco = new Brand { Name = "Ecco" };
        var zara = new Brand { Name = "Zara" };
        var timberland = new Brand { Name = "Timberland" };
        context.Brands.AddRange(nike, ecco, zara, timberland);
        context.SaveChanges();

        var sneakers = new Category { Name = "Кроссовки" };
        var boots = new Category { Name = "Ботинки" };
        var heels = new Category { Name = "Туфли на каблуке" };
        context.Categories.AddRange(sneakers, boots, heels);
        context.SaveChanges();

        context.Shoes.AddRange(
            new Shoe 
            { 
                Name = "Nike Air Max 90", 
                Price = 12500, 
                BrandId = nike.Id, 
                CategoryId = sneakers.Id, 
                Gender = "Мужской", 
                Season = "Демисезон", 
                Material = "Текстиль", 
                ImagePath = "/img/shoes/nike_am.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "41", Quantity = 5 },
                    new ShoeSize { Size = "42", Quantity = 12 },
                    new ShoeSize { Size = "43", Quantity = 7 }
                }
            },
            new Shoe 
            { 
                Name = "Nike Revolution 6", 
                Price = 7500, 
                BrandId = nike.Id, 
                CategoryId = sneakers.Id, 
                Gender = "Женский", 
                Season = "Лето", 
                Material = "Сетка", 
                ImagePath = "/img/shoes/nike_rev.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "37", Quantity = 4 },
                    new ShoeSize { Size = "38", Quantity = 10 },
                    new ShoeSize { Size = "39", Quantity = 0 } 
                }
            },
            new Shoe 
            { 
                Name = "Ecco BIOM", 
                Price = 16000, 
                BrandId = ecco.Id, 
                CategoryId = sneakers.Id, 
                Gender = "Унисекс", 
                Season = "Демисезон", 
                Material = "Кожа", 
                ImagePath = "/img/shoes/ecco_biom.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "39", Quantity = 3 },
                    new ShoeSize { Size = "40", Quantity = 8 },
                    new ShoeSize { Size = "41", Quantity = 6 }
                }
            },

            new Shoe 
            { 
                Name = "Timberland Premium 6-Inch", 
                Price = 22000, 
                BrandId = timberland.Id, 
                CategoryId = boots.Id, 
                Gender = "Мужской", 
                Season = "Зима", 
                Material = "Нубук", 
                ImagePath = "/img/shoes/timb_m.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "42", Quantity = 2 },
                    new ShoeSize { Size = "43", Quantity = 15 },
                    new ShoeSize { Size = "44", Quantity = 4 }
                }
            },
            new Shoe 
            { 
                Name = "Timberland Courmayeur", 
                Price = 19500, 
                BrandId = timberland.Id, 
                CategoryId = boots.Id, 
                Gender = "Женский", 
                Season = "Зима", 
                Material = "Замша", 
                ImagePath = "/img/shoes/timb_w.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "36", Quantity = 3 },
                    new ShoeSize { Size = "37", Quantity = 9 },
                    new ShoeSize { Size = "38", Quantity = 5 }
                }
            },
            new Shoe 
            { 
                Name = "Ecco Tredtray", 
                Price = 18000, 
                BrandId = ecco.Id, 
                CategoryId = boots.Id, 
                Gender = "Унисекс", 
                Season = "Демисезон", 
                Material = "Кожа", 
                ImagePath = "/img/shoes/ecco_tred.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "40", Quantity = 5 },
                    new ShoeSize { Size = "41", Quantity = 11 },
                    new ShoeSize { Size = "42", Quantity = 7 }
                }
            },

            new Shoe 
            { 
                Name = "Zara Classic Pumps", 
                Price = 4999, 
                BrandId = zara.Id, 
                CategoryId = heels.Id, 
                Gender = "Женский", 
                Season = "Лето", 
                Material = "Искусственная кожа", 
                ImagePath = "/img/shoes/zara_pumps.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "38", Quantity = 6 },
                    new ShoeSize { Size = "39", Quantity = 14 },
                    new ShoeSize { Size = "40", Quantity = 2 }
                }
            },
            new Shoe 
            { 
                Name = "Zara Slingback", 
                Price = 5500, 
                BrandId = zara.Id, 
                CategoryId = heels.Id, 
                Gender = "Женский", 
                Season = "Лето", 
                Material = "Текстиль", 
                ImagePath = "/img/shoes/zara_sling.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "35", Quantity = 2 },
                    new ShoeSize { Size = "36", Quantity = 8 }
                }
            },
            new Shoe 
            { 
                Name = "Ecco Shape 45", 
                Price = 12000, 
                BrandId = ecco.Id, 
                CategoryId = heels.Id, 
                Gender = "Женский", 
                Season = "Демисезон", 
                Material = "Кожа", 
                ImagePath = "/img/shoes/ecco_shape.jpg",
                ShoeSizes = new List<ShoeSize>
                {
                    new ShoeSize { Size = "37", Quantity = 4 },
                    new ShoeSize { Size = "38", Quantity = 10 },
                    new ShoeSize { Size = "39", Quantity = 5 }
                }
            }
        );
        context.SaveChanges();
    }
}