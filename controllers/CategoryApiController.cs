using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.models;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryApiController : ControllerBase
    {

        private static List<Category> categories = new List<Category>();


        [HttpPost]
        public IActionResult CreateCategory([FromBody] Category categoryData)
        {
            if (string.IsNullOrWhiteSpace(categoryData.Name.Trim()))
            {
                return BadRequest("Category name can not be empty");
            }
            if (string.IsNullOrWhiteSpace(categoryData.Description.Trim()))
            {
                return BadRequest("Category description can not be empty");
            }
            if (categoryData.Name.Length < 3)
            {
                return BadRequest("Category Name can not bellow 3 characters");
            }

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);
            return Created($"/api/v1/categories/{newCategory.CategoryId}", newCategory);
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue="")  
        {
            System.Console.WriteLine(searchValue);

            var searchCategory = categories.Where(c => !string.IsNullOrEmpty(c.Name) && 
            c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

            return Ok(searchCategory);
        }

        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, [FromBody] Category categoryData)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exits");
            }

            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return NoContent();
        }

        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound("Category with this id does not exits");
            }

            categories.Remove(foundCategory);

            return NoContent();
        }

    }
}



/*
    CRUD

    Create => Create a category => POST: /api/v1/categories
    Read   => Read a category => GET: /api/v1/categories
    Update => Update a category => POST:/api/v1/categories
    Delete => Delete a category => Delete: /api/v1/categories
*/