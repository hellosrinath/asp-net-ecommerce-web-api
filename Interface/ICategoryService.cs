using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Interface
{
    public interface ICategoryService
    {
        Task<List<CategoryReadDto>> GetAllCategories();
        Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData);
        Task<CategoryReadDto?> GetCategoryById(Guid categoryId);
        Task<bool> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData);
        Task<bool> DeleteCategory(Guid categoryId);
    }
}