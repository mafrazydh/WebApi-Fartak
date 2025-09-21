using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CategoryHandlers
{
    public class AllCategoresQueryHandler : IRequestHandler<AllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllCategoresQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Category>> Handle(AllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var res = await unitOfWork.CategoryRepository.AllCategories();
            foreach (var i in res)
            {
                i.Products = unitOfWork.ProductRepository.AllProducts().Result.Where(s=>s.CategoryId == i.Id).ToList();
            }
             return res;

        }
    }
}
