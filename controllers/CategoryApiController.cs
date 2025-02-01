using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.models;
using asp_net_ecommerce_web_api.services;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.controllers
{
    [ApiController]
    [Route("v1/api/categories")]
    public class CategoryApiController : ControllerBase
    {

        private ICategoryService _categoryService;

        public CategoryApiController(ICategoryService categoryServices)
        {
            _categoryService = categoryServices;
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            var categoryReadDto = await _categoryService.CreateCategory(categoryData);

            return Created(nameof(GetCategoryById),
            ApiResponse<CategoryReadDto>.SuccessResponse(
                categoryReadDto,
                201,
                "Category created successfully"
              ));
        }

        // GET: /api/categories?pageNumber=2 && pageSize=5
        // Read Categories
        [HttpGet]
        public async Task<IActionResult> GetCategories(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 6,
            [FromQuery] string? search = null
        )
        {

            var categoryList = await _categoryService
            .GetAllCategories(pageNumber, pageSize,search);

            return Ok(ApiResponse<PaginatedResult<CategoryReadDto>>.SuccessResponse(
                categoryList,
                200,
                "Categories returned successfully"
              ));
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategoryById(Guid categoryId)
        {

            var foundCategory = await _categoryService.GetCategoryById(categoryId);
            if (foundCategory == null)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this id does not exits" },
                 404, "")
                 );
            }

            return Ok(ApiResponse<CategoryReadDto>.SuccessResponse(
                foundCategory,
                200,
                "Category returned successfully"
              ));
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = await _categoryService.UpdateCategory(categoryId, categoryData);

            if (!foundCategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this id does not exits" },
                 404,
                 "")
                 );
            }

            return Ok(ApiResponse<object?>.SuccessResponse(
                null,
                200,
                "Category updated successfully"
              ));
        }

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            var foundCategory = await _categoryService.DeleteCategory(categoryId);

            if (!foundCategory)
            {
                return NotFound(ApiResponse<object>.ErrorResponse(new List<string>
                { "Category with this id does not exits" },
                 404,
                 "Validation Failed")
                 );
            }

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


    Best URL Naming practices for rest api

    1. Descriptive name
    2. plurals
    3. plurals/{singularNoun} => /categories/{categoryId}
    4. use hypens for multiple words for improving the redability => /producs-categories
    5. versioning
    6. avoid verbs in url path /createCategory => POST/ categories
*/