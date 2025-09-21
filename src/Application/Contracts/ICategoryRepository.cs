using Application.Models.Category;
using Domin.Entities;


namespace Application.Contracts
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> CreateAsync(CategoryDto categoryDto);
        Task<IEnumerable<Category>> AllCategories();
        Task DeleteCategory(string categoryId);
        Task<Category> GetCategoryInfo(string id);
    }
}
