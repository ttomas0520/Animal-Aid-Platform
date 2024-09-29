using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO.Category;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> GetCategoryByNameAndUrl(CategoryRequestDto category)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Name == category.Name && c.Urlhandle == category.Urlhandle);
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(int id, Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                existingCategory.Urlhandle = category.Urlhandle;

                await _context.SaveChangesAsync();
            }
            return existingCategory;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
