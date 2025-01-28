using System.Threading.Tasks;
using asp_net_ecommerce_web_api.data;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.Interface;
using asp_net_ecommerce_web_api.models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace asp_net_ecommerce_web_api.services
{
    public class CategoryServices : ICategoryService
    {

        private readonly AppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public CategoryServices(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<CategoryReadDto>> GetAllCategories()
        {
            var categories = await _appDbContext.Categories.ToListAsync();

            return _mapper.Map<List<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            // CategoryCreateDTO => Category
            var newCategory = _mapper.Map<Category>(categoryData);
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.CreatedAt = DateTime.UtcNow;

            await _appDbContext.Categories.AddAsync(newCategory);
            await _appDbContext.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(newCategory);
        }

        public async Task<CategoryReadDto?> GetCategoryById(Guid categoryId)
        {

            var foundCategory = await _appDbContext.Categories.FindAsync(categoryId);

            return foundCategory == null ? null : _mapper.Map<CategoryReadDto>(foundCategory);
        }

        public async Task<bool> UpdateCategory(Guid categoryId, CategoryUpdateDto categoryData)
        {
            var foundCategory = await _appDbContext.Categories.FindAsync(categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            // CategoryUpdateDto => Category
            _mapper.Map(categoryData, foundCategory);

             _appDbContext.Categories.Update(foundCategory);
            await _appDbContext.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteCategory(Guid categoryId)
        {
            var foundCategory = await _appDbContext.Categories.FindAsync(categoryId);

            if (foundCategory == null)
            {
                return false;
            }

            _appDbContext.Categories.Remove(foundCategory);
            await _appDbContext.SaveChangesAsync();

            return true;
        }
    }
}