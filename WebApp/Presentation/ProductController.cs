using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Application.Dto;
using WebApp.Application.Services;
using WebApp.Domain.Models;

namespace ReviewApp.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Products>))]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        [HttpGet("{ProductId}")]
        [ProducesResponseType(200, Type = typeof(Products))]
        [ProducesResponseType(400)]
        public IActionResult GetGood(int ProductId)
        {
            if (!_productService.ProductExists(ProductId))
                return NotFound();

            var product = _productService.GetProduct(ProductId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }

        [HttpPut("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGood(int productId, [FromQuery] int categoryId, [FromBody] ProductDto updatedGood)
        {
            if (updatedGood == null || !_productService.UpdateProduct(productId, categoryId, updatedGood))
                return BadRequest(ModelState);

            return Ok("Chill");
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromQuery] int categoryId, [FromBody] ProductDto productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            if (!_productService.CreateProduct(categoryId, productCreate))
            {
                ModelState.AddModelError("", "Product already exists or something went wrong while saving");
                return StatusCode(422, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!_productService.ProductExists(productId))
                return NotFound();

            if (!_productService.DeleteProduct(productId))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
