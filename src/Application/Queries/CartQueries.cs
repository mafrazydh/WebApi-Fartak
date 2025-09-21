using Domin.Entities;
using MediatR;

namespace Application.Queries
{
    public record GetCartQuery(string userId) : IRequest<List<Cart>>;
}
