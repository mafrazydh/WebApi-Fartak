using Application.Contracts;
using Application.Exceptions;
using Application.Models.Category;
using AutoMapper;
using Domin.Entities;
using Infrastructure.Persistence;


namespace Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;



        public CategoryRepository(ApplicationDbContext context, IMapper mapper, IProductRepository productRepository) : base(context)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        public async Task<Category> CreateAsync(CategoryDto categoryDto)
        {
            var category = mapper.Map<Category>(categoryDto);
            return await AddAsync(category);
        }

        public async Task<IEnumerable<Category>> AllCategories()
        {
            var categories = await GetAllAsync();
            return categories;
        }

        public async Task DeleteCategory(string categoryId)
        {

            var cate = await  GetByIdAsync(categoryId);
            if (cate == null)
            {
                throw new CustomException("دسته ای با این آیدی وجود ندارد");
            }
            await Delete(categoryId);
        }

        public async Task<Category> GetCategoryInfo(string id)
        {
            var category = await GetByIdAsync(id);
            if (category == null)
            {
                throw new CustomException("دسته ای با این آیدی وجود ندارد");
            }
            var products = productRepository.AllProducts().Result.Where(p => p.CategoryId == new Guid(id)).ToList();            
            category.Products = products;
            return category;


        }


    }
}
