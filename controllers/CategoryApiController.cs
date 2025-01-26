using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.models;
using asp_net_ecommerce_web_api.services;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_ecommerce_web_api.controllers
{
    [ApiController]
    [Route("v1/api/categories")]
    public class CategoryApiController : ControllerBase
    {

        private CategoryServices _categoryService;

        public CategoryApiController(CategoryServices categoryServices)
        {
            _categoryService = categoryServices;
        }


        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryCreateDto categoryData)
        {
            var categoryReadDto = _categoryService.CreateCategory(categoryData);

            return Created(nameof(GetCategoryById),
            ApiResponse<CategoryReadDto>.SuccessResponse(
                categoryReadDto,
                201,
                "Category created successfully"
              ));
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var categoryList = _categoryService.GetAllCategories();

            return Ok(ApiResponse<List<CategoryReadDto>>.SuccessResponse(
                categoryList,
                200,
                "Categories returned successfully"
              ));
        }

        [HttpGet("{categoryId:guid}")]
        public IActionResult GetCategoryById(Guid categoryId)
        {

            var foundCategory = _categoryService.GetCategoryById(categoryId);
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
        public IActionResult UpdateCategory(Guid categoryId, [FromBody] CategoryUpdateDto categoryData)
        {
            var foundCategory = _categoryService.UpdateCategory(categoryId,categoryData);

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
        public IActionResult DeleteCategory(Guid categoryId)
        {
            var foundCategory = _categoryService.DeleteCategory(categoryId);

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