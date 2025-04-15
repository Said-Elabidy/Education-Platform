using Education.Application.DTO_s;
using Education.Application.DTO_s.CategoryDto_s;
using Education.Application.Services.CategoryServices;
using Education.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController: ControllerBase
    {
        
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices ?? throw new ArgumentNullException(nameof(categoryServices));
        }
        private readonly ICategoryServices _categoryServices ;
        // get All Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetCategories();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories);
        }
        //get All Category By It's Id
        
        // Add new Category
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCategory([FromBody] CreateCategoryDto categoryDto)
        {
            Categories category = new Categories
            {
                Name = categoryDto.Name,
                CreateOn = DateTime.Now,
                IsDeleted = false,



            };

           
            try
            {
                await _categoryServices.Add(category);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Categories>> GetCategoryById([FromRoute] int Id)
        {
            var category = await _categoryServices.GetCategoryById(Id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }
        // Delete Category
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryServices.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // Update Category
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            var existingCategory = await _categoryServices.GetCategoryById(id);
            if (existingCategory == null)
            {
                return NotFound();
            }

            existingCategory.Name = categoryDto.Name;
            existingCategory.LastUpdateOn= DateTime.Now;

            try
            {
                await _categoryServices.Update(existingCategory);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Categories>>> SearchCategoryByName([FromQuery] string name)
        {
            var categories = await _categoryServices.SearchCategoryByName(name);
            if (categories == null || !categories.Any())
            {
                return NotFound();
            }
            return Ok(categories);
        }
    }
}
