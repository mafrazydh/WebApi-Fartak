using Domin.Entities;
using MediatR;

namespace Application.Queries
{
    public record AllProductsQuery() : IRequest<IEnumerable<Product>>;
    public record GetProductByIdQuery(string id) : IRequest<Product>;
}
