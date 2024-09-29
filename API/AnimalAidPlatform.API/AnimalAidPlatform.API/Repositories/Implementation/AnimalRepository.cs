using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Animal>> GetAllAnimals()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<Animal> GetAnimalById(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Animal> CreateAnimal(Animal animal)
        {
            _context.Animals.Add(animal);
            await _context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> UpdateAnimal(int id, Animal animal)
        {
            var existingAnimal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (existingAnimal != null)
            {
                existingAnimal.Name = animal.Name;
                existingAnimal.Breed = animal.Breed;
                existingAnimal.Photo = animal.Photo;
                existingAnimal.Age = animal.Age;
                existingAnimal.Description = animal.Description;
                existingAnimal.AnimalShelterId = animal.AnimalShelterId;

                await _context.SaveChangesAsync();
            }
            return existingAnimal;
        }

        public async Task<bool> DeleteAnimal(int id)
        {
            var animalToDelete = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
            if (animalToDelete != null)
            {
                _context.Animals.Remove(animalToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
