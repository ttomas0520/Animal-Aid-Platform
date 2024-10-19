using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace AnimalAidPlatform.API.Repositories.Implementation
{
    public class AnimalShelterRepository : IAnimalShelterRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalShelterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AnimalShelter?> GetByIdAsync(int id)
        {
            return await _context.AnimalShelters.FindAsync(id);
        }

        public async Task<IEnumerable<AnimalShelter>> GetAllAsync()
        {
            return await _context.AnimalShelters.ToListAsync();
        }

        public async Task AddAsync(AnimalShelter shelter)
        {
            await _context.AnimalShelters.AddAsync(shelter);
            await _context.SaveChangesAsync();
        }

        public async Task<AnimalShelter?> UpdateAsync(AnimalShelter shelter)
        {
            _context.AnimalShelters.Update(shelter);
            await _context.SaveChangesAsync();
            return await _context.AnimalShelters.FindAsync(shelter.Id);
        }

        public async Task DeleteAsync(int id)
        {
            var shelter = await GetByIdAsync(id);
            if (shelter != null)
            {
                _context.AnimalShelters.Remove(shelter);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<AnimalShelter>> GetByLocationAsync(Point location, double radius)
        {
            // Spatial query to find shelters within a certain radius of the given location
            return await _context.AnimalShelters
                .Where(s => s.Location != null &&
                            s.Location.Distance(location) <= radius)
                .ToListAsync();
        }
    }
}
