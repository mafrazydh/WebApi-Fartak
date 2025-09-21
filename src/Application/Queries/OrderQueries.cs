using Domin.Entities;
using MediatR;

namespace Application.Queries
{
    public record AllUserOrderQuery(string userId) : IRequest<List<Order>>;
    public record AllOrdersQuery() : IRequest<List<Order>>;
}
