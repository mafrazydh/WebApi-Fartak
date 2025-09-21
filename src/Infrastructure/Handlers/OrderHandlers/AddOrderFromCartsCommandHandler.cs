using Application.Commands;
using Application.Contracts;
using Application.Models.Orders;
using MediatR;


namespace Infrastructure.Handlers.OrderHandlers
{
    public class AddOrderFromCartsCommandHandler : IRequestHandler<AddOrderFromCartsCommand, OrderDto>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddOrderFromCartsCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task<OrderDto> Handle(AddOrderFromCartsCommand request, CancellationToken cancellationToken)
        {
            var res =  unitOfWork.OrderRepository.AddOrderFromCarts(request.userId);
            unitOfWork.CompleteAsync();
            return res;
        }
    }
}
