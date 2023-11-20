using AnimalAidPlatform.Models;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllAnimals();
        Task<Animal> GetAnimalById(Guid id);
        Task<Animal> CreateAnimal(Animal animal);
        Task<Animal> UpdateAnimal(Guid id, Animal animal);
        Task<bool> DeleteAnimal(Guid id);
    }
}
