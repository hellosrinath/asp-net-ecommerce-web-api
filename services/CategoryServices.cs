using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.models;

namespace asp_net_ecommerce_web_api.services
{
    public class CategoryServices
    {
        private static readonly List<Category> categories = new List<Category>();

        public List<CategoryReadDto> GetAllCategories()
        {
            return categories.Select(c => new CategoryReadDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Description = c.Description,
                CreatedAt = c.CreatedAt
            }).ToList();
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            var newCategory = new Category
            {
                CategoryId = Guid.NewGuid(),
                Name = categoryData.Name,
                Description = categoryData.Description,
                CreatedAt = DateTime.UtcNow,
            };
            categories.Add(newCategory);

            return new CategoryReadDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Description = newCategory.Description,
                CreatedAt = newCategory.CreatedAt
            };
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {

            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            if (foundCategory == null)
            {
                return null;
            }

            return new CategoryReadDto
            {
                CategoryId = foundCategory.CategoryId,
                Name = foundCategory.Name,
                Description = foundCategory.Description,
                CreatedAt = foundCategory.CreatedAt
            };
        }

        public bool UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            foundCategory.Name = categoryData.Name;
            foundCategory.Description = categoryData.Description;

            return true;
        }


        public bool DeleteCategory(Guid categoryId)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            categories.Remove(foundCategory);

            return true;
        }
    }
}