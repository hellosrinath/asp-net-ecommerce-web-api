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

            return Created($"/api/v1/categories/{categoryReadDto.CategoryId}",
            ApiResponse<CategoryReadDto>.SuccessResponse(
                categoryReadDto,
                201,
                "Category created successfully"
              ));
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categoryList = categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(
                categoryList,
                200,
                "Categories returned successfully"
              ));
        }

        [HttpPut("{categoryId:guid}")]
        public IActionResult UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this id does not exits" },
                 404,
                 "Validation Failed")
                 );
            }

            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return Ok(ApiResponse<object?>.SuccessResponse(
                null,
                200,
                "Category updated successfully"
              ));
        }

        [HttpDelete("{categoryId:guid}")]
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this id does not exits" },
                 404,
                 "Validation Failed")
                 );
            }

            categories.Remove(foundCategory);

            return Ok(ApiResponse<object?>.SuccessResponse(
                null,
                200,
                "Category deteled successfully"
              ));
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