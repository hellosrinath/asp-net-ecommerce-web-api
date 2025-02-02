using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.controllers;
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Interface
{
    public interface ICategoryService
    {
        Task<PaginatedResult<CategoryReadDto>> GetAllCategories(
            int pageNumber, int pageSize, string? search = null,
            string? sortOrder = null
            );
        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData);
        Task<CategoryReadDto?> GetCategoryById(Guid categoryId);
        Task<bool> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData);
        Task<bool> DeleteCategory(Guid categoryId);
    }
}