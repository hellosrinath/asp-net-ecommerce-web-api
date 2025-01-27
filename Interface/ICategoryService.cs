using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;

namespace asp_net_ecommerce_web_api.Interface
{
    public interface ICategoryService
    {
        List<CategoryReadDto> GetAllCategories();
        CategoryReadDto CreateCategory(CategoryCreateDto categoryData);
        CategoryReadDto? GetCategoryById(Guid categoryId);
        bool UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData);
        bool DeleteCategory(Guid categoryId);
    }
}