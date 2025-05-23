﻿using Education.Application.DTO_s;
using Education.Application.DTO_s.CategoryDto_s;
using Education.Application.Services.CategoryServices;
using Education.Domain.Entities;
using Education.Domain.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices ?? throw new ArgumentNullException(nameof(categoryServices));
        }
        private readonly ICategoryServices _categoryServices;
        // get All Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> GetAllCategories()
        {
            var categories = await _categoryServices.GetCategories();
            if (categories == null || !categories.Any())
            {
                return NotFound("There are no categories yet");
            }
            return Ok(categories);
        }
        //get All Category By It's Id

        // Add new Category
        [HttpPost]
        //[Authorize(Roles = MyRoles.Admin)]
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
               var res = await _categoryServices.Add(category);
                if (!res)
                {
                    return BadRequest("Failed to create category");
                }
                return StatusCode(201,category);
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
                return NotFound($"Category with id {Id} was not found ");
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
                return NotFound($"Category with id {id} was not found ");
            }

            try
            {
              var res = await _categoryServices.Delete(id);
                if (!res)
                {
                    return BadRequest("Failed to delete category");
                }
                return Ok("caregory deleted succesfully");
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
                return NotFound($"ther is no category with Id {id}");
            }

            existingCategory.Name = categoryDto.Name;
            existingCategory.LastUpdateOn = DateTime.Now;

            try
            {
                var categoryEntity = new Categories { Name = existingCategory.Name, LastUpdateOn = existingCategory.LastUpdateOn, CreateOn = existingCategory.CreateOn, CategorieId = existingCategory.CategorieId, IsDeleted = false };

               var res = await _categoryServices.Update(id,categoryEntity);
                if (!res)
                {
                    return BadRequest("Failed to update category");
                }
                return Ok("category updated succesfully");
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
