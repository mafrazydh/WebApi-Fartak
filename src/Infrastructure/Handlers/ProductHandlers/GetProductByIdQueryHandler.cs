using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.ProductHandlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.ProductRepository.GetProductById(request.id);
        }
    }
}
