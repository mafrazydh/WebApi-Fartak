using Application.Contracts;
using Application.Exceptions;
using Application.Models.Products;
using AutoMapper;
using Domin.Entities;
using Infrastructure.Persistence;


namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IMapper mapper;
       

        public ProductRepository(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Product>> AllProducts()
        {

            var products =  await GetAllAsync();
            return products;
        }

        public async Task<Product> CreateAsync(ProductDto productDto, Category category)
        {
            var product = mapper.Map<Product>(productDto);
            product.ProductPictures = productDto.ProductPicturesStr;
            if (product != null )
            {
                if (product.CategoryId != null) {
                    product.Category = category;
                    return AddAsync(product).Result;
                }
                throw new CustomNotFoundException("Category", category.Id.ToString());

            }
            throw new CustomNotFoundException("Product", "ورودی");


        }

        public async Task<Product> UpdateAsync(string id,ProductDto productDto)
        {

            var oldProduct = GetByIdAsync(id);
            if (oldProduct == null)
            {
                throw new CustomNotFoundException("Product", id);
            }
            if (productDto.Number != null) { oldProduct.Result.Number = productDto.Number; }
            if (!string.IsNullOrEmpty(productDto.Price)) { oldProduct.Result.Price = productDto.Price; }
            if (!string.IsNullOrEmpty(productDto.Name)) { oldProduct.Result.Name = productDto.Name; }
            if (!string.IsNullOrEmpty(productDto.MainDescription)) { oldProduct.Result.MainDescription = productDto.MainDescription; }
            if (!string.IsNullOrEmpty(productDto.MadeIn)) { oldProduct.Result.MadeIn = productDto.MadeIn; }
            if (!string.IsNullOrEmpty(productDto.AllDescription)) { oldProduct.Result.AllDescription = productDto.AllDescription; }
            if (!string.IsNullOrEmpty(productDto.Color)) { oldProduct.Result.Color = productDto.Color; }
            if (!string.IsNullOrEmpty(productDto.ReleaseDate)) { oldProduct.Result.ReleaseDate = productDto.ReleaseDate; }
            if (productDto.ProductPictures is not null && productDto.ProductPictures.Count > 0)
            {
                oldProduct.Result.ProductPictures = productDto.ProductPicturesStr;
            }
            return await UpdateAsync(oldProduct.Result);
        }

        public Task DeleteProduct(string id)
        {
           var result = Delete(id);
            if (result.IsCompletedSuccessfully) {
                return Task.CompletedTask;
            }
            throw new CustomNotFoundException("Product", id);

        }

        public Task<Product> GetProductById(string id)
        {
            var result = GetByIdAsync(id);
            if (result.IsCompletedSuccessfully)
            {
                return result;
            }
            throw new CustomNotFoundException("Product", id);
        }

       

     

    
    }
}
