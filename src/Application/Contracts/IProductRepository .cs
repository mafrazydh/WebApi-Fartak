using Application.Models.Products;
using Domin.Entities;


namespace Application.Contracts
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> CreateAsync(ProductDto productDto,Category category);
        Task<IEnumerable<Product>> AllProducts();
        Task<Product> GetProductById(string id);
        Task<Product> UpdateAsync(string id, ProductDto productDto);
        Task DeleteProduct(string id);
        
    }
}
