using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.OrderHandlers
{
    public class AllOrdersQueryHandler : IRequestHandler<AllOrdersQuery, List<Order>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllOrdersQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Order>> Handle(AllOrdersQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.OrderRepository.AllOrders();
        }
    }
}
