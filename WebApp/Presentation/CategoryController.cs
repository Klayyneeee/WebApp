using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Dto;
using WebApp.Application.Services;
using WebApp.Domain.Interface;
using WebApp.Domain.Models;

namespace ReviewApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            var categories = _categoryService.GetCategories();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult GetProducts(int categoryId)
        {
            if (!_categoryService.CategoryExists(categoryId))
                return NotFound();

            var category = _categoryService.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("product/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(400)]
        public IActionResult GetProductByCategoryId(int categoryId)
        {
            var goods = _categoryService.GetProductsByCategoryId(categoryId);
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(goods);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            if (!_categoryService.CreateCategory(categoryCreate))
            {
                ModelState.AddModelError(" ", "Category already exists");
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null || !_categoryService.UpdateCategory(categoryId, updatedCategory))
                return BadRequest(ModelState);

            return Ok("Chill");
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCategory(int categoryId)
        {
            if (!_categoryService.DeleteCategory(categoryId))
                return NotFound();

            return NoContent();
        }
    }
}
