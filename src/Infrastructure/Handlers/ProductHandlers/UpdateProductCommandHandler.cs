using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,Product>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.ProductRepository.UpdateAsync(request.id,request.productDto);
            unitOfWork.CompleteAsync();
            return value;

        }
    }
}
