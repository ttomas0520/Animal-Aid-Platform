using AnimalAidPlatform.API.Models.DTO;
using AnimalAidPlatform.API.Repositories.Interface;
using AnimalAidPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace AnimalAidPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalShelterController : ControllerBase
    {
        private readonly IAnimalShelterRepository _repository;

        public AnimalShelterController(IAnimalShelterRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnimalShelterDTO>>> GetAll()
        {
            var shelters = await _repository.GetAllAsync();

            // Mapping domain models to DTOs using AnimalShelterMapper
            var shelterDtos = shelters.Select(s => s.ToDto()).ToList();

            return Ok(shelterDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnimalShelterDTO>> GetById(int id)
        {
            var shelter = await _repository.GetByIdAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }

            // Mapping domain model to DTO using AnimalShelterMapper
            var shelterDto = shelter.ToDto();

            return Ok(shelterDto);
        }

        [HttpPost]
        public async Task<ActionResult> Add(AnimalShelterDTO shelterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Mapping DTO to domain model using AnimalShelterMapper
            var shelter = shelterDto.ToEntity();

            await _repository.AddAsync(shelter);
            return CreatedAtAction(nameof(GetById), new { id = shelter.Id }, shelterDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AnimalShelterDTO>> Update(int id, AnimalShelterDTO shelterDto)
        {
            if (id != shelterDto.Id)
            {
                return BadRequest("Azonosító eltérés.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingShelter = await _repository.GetByIdAsync(id);
            if (existingShelter == null)
            {
                return NotFound();
            }

            // Map DTO to existing entity (instead of creating a new one)
            shelterDto.UpdateEntity(existingShelter);

            // Update the entity in the repository
            var dbEntity = await _repository.UpdateAsync(existingShelter);
            return Ok(dbEntity.ToDto());
        }

        [HttpGet("location")]
        public async Task<ActionResult<IEnumerable<AnimalShelterDTO>>> GetByLocation([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] double radius)
        {
            var location = new Point(longitude, latitude) { SRID = 4326 };  // WGS 84 standard
            var shelters = await _repository.GetByLocationAsync(location, radius);

            // Mapping domain models to DTOs using AnimalShelterMapper
            var shelterDtos = shelters.Select(s => s.ToDto()).ToList();

            return Ok(shelterDtos);
        }

    }
}
