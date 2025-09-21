using Application.Models.Products;
using Domin.Entities;
using MediatR;


namespace Application.Commands
{
    public record CreateProductCommand(ProductDto dto) : IRequest<Product>;
    public record DeleteProductCommand(string id) : IRequest;
    public record UpdateProductCommand(string id, ProductDto productDto) : IRequest<Product>;

}
