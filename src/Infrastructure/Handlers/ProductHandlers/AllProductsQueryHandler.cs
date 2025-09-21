using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.ProductHandlers
{
    public class AllProductsQueryHandler : IRequestHandler<AllProductsQuery, IEnumerable<Product>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllProductsQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> Handle(AllProductsQuery request, CancellationToken cancellationToken)
        {
             var res = await unitOfWork.ProductRepository.AllProducts();
             return res;

        }
    }
}
