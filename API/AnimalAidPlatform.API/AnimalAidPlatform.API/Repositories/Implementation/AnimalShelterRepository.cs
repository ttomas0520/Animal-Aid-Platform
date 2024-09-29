using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class AnimalShelterRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AnimalShelterRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task<Category> CreateAsync(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
