using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newId = request.dto.CategoryId.ToString().Replace("{", "").Replace("}", "");
            var cat = unitOfWork.CategoryRepository.GetCategoryInfo(newId);
            var value = await unitOfWork.ProductRepository.CreateAsync(request.dto,cat.Result);
            unitOfWork.CompleteAsync();
            return value;

        }
    }
}
