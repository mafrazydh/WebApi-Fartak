using Domin.Entities;
using MediatR;

namespace Application.Queries
{
    public record AllCategoriesQuery() : IRequest<IEnumerable<Category>>;
    public record GetCategoryInfoQuery(string id) : IRequest<Category>;
    
}
