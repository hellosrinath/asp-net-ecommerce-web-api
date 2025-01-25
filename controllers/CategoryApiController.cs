using asp_net_ecommerce_web_api.DTOs;
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
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {

            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);

            var categoryReadDto = new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt
            };

            return Created($"/api/v1/categories/{categoryReadDto.CategoryId}", categoryReadDto);
        }

        [HttpGet]
        public IActionResult GetCategories([FromQuery] string searchValue = "")
        {
            System.Console.WriteLine(searchValue);

            if (!string.IsNullOrWhiteSpace(searchValue))
            {
                var searchCategory = categories.Where(c => !string.IsNullOrEmpty(c.Name) &&
                   c.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();


                var categoryRead = searchCategory.Select(c => new CategoryReadDto
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name,
                    Description = c.Description,
                    CreatedAt = c.CreatedAt
                });
                return Ok(categoryRead);
            }


            var categoryList = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            });

            return Ok(categoryList);
        }

        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
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