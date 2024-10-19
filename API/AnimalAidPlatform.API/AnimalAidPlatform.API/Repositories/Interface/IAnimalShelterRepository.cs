using AnimalAidPlatform.Models;
using NetTopologySuite.Geometries;

namespace AnimalAidPlatform.API.Repositories.Interface
{
    public interface IAnimalShelterRepository
    {
        Task<AnimalShelter?> GetByIdAsync(int id);
        Task<IEnumerable<AnimalShelter>> GetAllAsync();
        Task AddAsync(AnimalShelter shelter);
        Task<AnimalShelter> UpdateAsync(AnimalShelter shelter);
        Task DeleteAsync(int id);
        Task<IEnumerable<AnimalShelter>> GetByLocationAsync(Point location, double radius);
    }
}
