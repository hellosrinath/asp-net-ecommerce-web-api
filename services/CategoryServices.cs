using System.Threading.Tasks;
using asp_net_ecommerce_web_api.controllers;
using asp_net_ecommerce_web_api.data;
using asp_net_ecommerce_web_api.DTOs;
using asp_net_ecommerce_web_api.enums;
using asp_net_ecommerce_web_api.Helper;
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

        public async Task<PaginatedResult<CategoryReadDto>> GetAllCategories(
            QueryParameters parameters
        )
        {

            var search = parameters.Search;
            var sortOrder = parameters.SortOrder;
            var pageNumber = parameters.PageNumber;
            var pageSize = parameters.PageSize;

            IQueryable<Category> query = _appDbContext.Categories;

            if (!string.IsNullOrWhiteSpace(search))
            {

                // query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()) ||
                // c.Description.ToLower().Contains(search.ToLower()));
                var formattedSearch = $"%{search.Trim()}%";
                query = query.Where(c => EF.Functions.ILike(c.Name, formattedSearch) ||
                EF.Functions.ILike(c.Description, formattedSearch));
            }

            if (string.IsNullOrWhiteSpace(sortOrder))
            {
                query = query.OrderBy(c => c.Name);
            }
            else
            {
                var formattedSortOrder = sortOrder.Trim().ToLower();

                if (Enum.TryParse<SortOrder>(formattedSortOrder, true, out var parsedSortOrder))
                {
                    query = parsedSortOrder switch
                    {
                        SortOrder.NameAsc => query.OrderBy(c => c.Name),
                        SortOrder.NameDesc => query.OrderByDescending(c => c.Name),
                        SortOrder.CreatedAtAsc => query.OrderBy(c => c.CreatedAt),
                        SortOrder.CreatedAtDesc => query.OrderByDescending(c => c.CreatedAt),
                        _ => query.OrderBy(c => c.Name)
                    };
                }

                // query = formattedSortOrder switch
                // {
                //     "name_asc" => query.OrderBy(c => c.Name),
                //     "name_desc" => query.OrderByDescending(c => c.Name),
                //     "createdat_asc" => query.OrderBy(c => c.CreatedAt),
                //     "createdat_desc" => query.OrderByDescending(c => c.CreatedAt),
                //     _ => query.OrderBy(c => c.Name)
                // };

                // switch (formattedSortOrder.ToLower())
                // {
                //     case "name_asc":
                //         query = query.OrderBy(c => c.Name);
                //         break;
                //     case "name_desc":
                //         query = query.OrderByDescending(c => c.Name);
                //         break;
                //     case "createdat_asc":
                //         query = query.OrderBy(c => c.CreatedAt);
                //         break;
                //     case "createdat_desc":
                //         query = query.OrderByDescending(c => c.CreatedAt);
                //         break;
                //     default:
                //         query = query.OrderBy(c => c.Name);
                //         break;
                // }

            }

            var totalCount = await query.CountAsync();

            // pagination, pageNumber =1 , pageSize = 5
            // 20 categories
            // Skip((pageNumber-1)*pageSize).Take(pageSize)
            var items = await query.Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).ToListAsync();


            //  var categories = await _appDbContext.Categories.ToListAsync();

            var results = _mapper.Map<List<CategoryReadDto>>(items);

            return new PaginatedResult<CategoryReadDto>
            {
                Items = results,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

        }

        public async Task<CategoryReadDto> CreateCategory(CategoryCreateDto categoryData)
        {
            // CategoryCreateDTO => Category
            var newCategory = _mapper.Map<Category>(categoryData);
          

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