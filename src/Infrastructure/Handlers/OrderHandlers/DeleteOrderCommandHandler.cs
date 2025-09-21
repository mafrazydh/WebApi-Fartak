using Application.Commands;
using Application.Contracts;
using MediatR;


namespace Infrastructure.Handlers.OrderHandlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteOrderCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.OrderRepository.DeleteOrder(request.orderId);
           
        }
    }
}
