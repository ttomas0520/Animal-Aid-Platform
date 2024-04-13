using AnimalAidPlatform.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllAnimals();
        Task<Animal> GetAnimalById(int id);
        Task<Animal> CreateAnimal(Animal animal);
        Task<Animal> UpdateAnimal(int id, Animal animal);
        Task<bool> DeleteAnimal(int id);
    }
}
