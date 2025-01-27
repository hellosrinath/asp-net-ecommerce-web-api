using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.models;
using AutoMapper;

namespace asp_net_ecommerce_web_api.services
{
    public class CategoryServices : ICategoryService
    {
        private static readonly List<Category> categories = new List<Category>();

        private readonly IMapper _mapper;

        public CategoryServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public List<CategoryReadDto> GetAllCategories()
        {
            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public CategoryReadDto CreateCategory(CategoryCreateDto categoryData)
        {
            // CategoryCreateDTO => Category
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            categories.Add(newCategory);

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public CategoryReadDto? GetCategoryById(Guid categoryId)
        {

            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);
            
            return foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public bool UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            // CategoryUpdateDto => Category
            _mapper.Map(categoryData, foundCategory);
            // foundCategory.Name = categoryData.Name;
            // foundCategory.Description = categoryData.Description;

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