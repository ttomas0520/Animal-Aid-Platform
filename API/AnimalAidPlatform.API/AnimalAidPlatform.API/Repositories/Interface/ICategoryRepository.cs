using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> GetCategoryById(Guid id);
        Task<Category> GetCategoryByNameAndUrl(CategoryRequestDto category);
        Task<Category> CreateCategory(Category category);
        Task<Category> UpdateCategory(Guid id, Category category);
        Task<bool> DeleteCategory(Guid id);
    }
}
