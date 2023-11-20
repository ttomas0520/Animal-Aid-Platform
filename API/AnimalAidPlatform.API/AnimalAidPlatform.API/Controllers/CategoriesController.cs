using AnimalAidPlatform.API.Data;
using AnimalAidPlatform.API.Models;
using AnimalAidPlatform.API.Models.DTO;
using AnimalAidPlatform.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalAidPlatform.API.Controllers
{

    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var category = new Category
                {
                    Name = request.Name,
                    Urlhandle = request.Urlhandle,
                };

                var createdCategory = await categoryRepository.CreateCategory(category);

                var categoryDto = new CategoryDto
                {
                    Id = createdCategory.Id,
                    Name = createdCategory.Name,
                    Urlhandle = createdCategory.Urlhandle,
                };

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kategória létrehozása közben: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await categoryRepository.GetAllCategories();

                var categoryDtos = categories.Select(category => new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Urlhandle = category.Urlhandle,
                }).ToList();

                return Ok(categoryDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kategóriák lekérdezése közben: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await categoryRepository.GetCategoryById(id);

                if (category == null)
                {
                    return NotFound();
                }

                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    Urlhandle = category.Urlhandle,
                };

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kategória lekérdezése közben: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingCategory = await categoryRepository.GetCategoryById(id);

                if (existingCategory == null)
                {
                    return NotFound();
                }

                existingCategory.Name = request.Name;
                existingCategory.Urlhandle = request.Urlhandle;

                var updatedCategory = await categoryRepository.UpdateCategory(id, existingCategory);

                var categoryDto = new CategoryDto
                {
                    Id = updatedCategory.Id,
                    Name = updatedCategory.Name,
                    Urlhandle = updatedCategory.Urlhandle,
                };

                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kategória frissítése közben: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string name, string url)
        {
            CategoryRequestDto category = new CategoryRequestDto();
            category.Name = name;
            category.Urlhandle = url;

            try
            {
                var categoryToDelete = await categoryRepository.GetCategoryByNameAndUrl(category);

                if (categoryToDelete == null)
                {
                    return NotFound();
                }

                var isDeleted = await categoryRepository.DeleteCategory(categoryToDelete.Id);

                if (isDeleted)
                {
                    return NoContent();
                }

                return StatusCode(500, "A kategória törlése közben hiba történt.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hiba történt a kategória törlése közben: {ex.Message}");
            }
        }
    }
}
