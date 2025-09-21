using Application.Commands;
using Application.Contracts;
using MediatR;


namespace Infrastructure.Handlers.ProductHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var res = unitOfWork.ProductRepository.DeleteProduct(request.id);
            unitOfWork.CompleteAsync();
            return res;
        }
    }
}
