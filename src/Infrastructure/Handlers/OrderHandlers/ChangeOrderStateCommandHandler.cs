using Application.Commands;
using Application.Contracts;
using Application.Models.Orders;
using MediatR;


namespace Infrastructure.Handlers.OrderHandlers
{
    public class ChangeOrderStateCommandHandler : IRequestHandler<ChangeOrderStateCommand, OrderDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public ChangeOrderStateCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<OrderDto> Handle(ChangeOrderStateCommand request, CancellationToken cancellationToken)
        {
            return unitOfWork.OrderRepository.ChangeOrderState(request.orderId);            
        }
    }
}
