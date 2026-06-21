using Microsoft.EntityFrameworkCore;
using MyWebApp.Models;
using Microsoft.AspNetCore.Components.Forms; 
using MyWebApp.Data;


namespace MyWebApp.Services
{
    public class ShoeService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ShoeService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<Shoe>> GetShoesAsync()
        {
            return await _context.Set<Shoe>()
                .Include(s => s.Brand)
                .Include(s => s.Category)
                .ToListAsync();
        }

        public async Task<List<Shoe>> GetFilteredShoesAsync(string? category, string? gender, string? material, string? season, string? sort)
        {
            var query = _context.Shoes
                .Include(s => s.Brand)
                .Include(s => s.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category)) query = query.Where(s => s.Category.Name == category);
            if (!string.IsNullOrEmpty(gender)) query = query.Where(s => s.Gender == gender);
            if (!string.IsNullOrEmpty(material)) query = query.Where(s => s.Material == material);
            if (!string.IsNullOrEmpty(season)) query = query.Where(s => s.Season == season);

            query = sort switch
            {
                "price_asc" => query.OrderBy(s => s.Price),
                "price_desc" => query.OrderByDescending(s => s.Price),
                _ => query.OrderBy(s => s.Name)
            };

            return await query.ToListAsync();
        }

        public async Task<List<object>> GetShoeShortListAsync()
        {
        return await _context.Set<Shoe>()
        .Select(s => new { s.Id, s.Name }) 
        .ToListAsync<object>();
        }

        public async Task<Shoe?> GetShoeByIdAsync(int id)
        {
            return await _context.Set<Shoe>()
                .Include(s => s.Brand)
                .Include(s => s.Category)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<string> UploadImageAsync(IBrowserFile file)
        {
            var uploadPath = Path.Combine(_env.WebRootPath, "img", "shoes");
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.Name)}";
            var fullPath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(fullPath, FileMode.Create);
            await file.OpenReadStream(maxAllowedSize: 1024 * 1024 * 10).CopyToAsync(stream); 
            return $"/img/shoes/{fileName}";
        }

        public async Task AddShoeAsync(Shoe shoe)
        {
            _context.Set<Shoe>().Add(shoe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateShoeAsync(Shoe shoe)
        {
            _context.Set<Shoe>().Update(shoe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteShoeAsync(int id)
        {
            var shoe = await _context.Set<Shoe>().FindAsync(id);
            if (shoe != null)
            {
                if (!string.IsNullOrEmpty(shoe.ImagePath))
                {
                    var fileToDelete = Path.Combine(_env.WebRootPath, shoe.ImagePath.TrimStart('/'));
                    if (File.Exists(fileToDelete)) File.Delete(fileToDelete);
                }

                _context.Set<Shoe>().Remove(shoe);
                await _context.SaveChangesAsync();
            }
        }
    }
}