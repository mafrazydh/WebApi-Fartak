using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.OrderHandlers
{
    public class AllUserOrderQueryHandler : IRequestHandler<AllUserOrderQuery, List<Order>>
    {
        private readonly IUnitOfWork unitOfWork;

        public AllUserOrderQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Order>> Handle(AllUserOrderQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.OrderRepository.AllUserOrder(request.userId);
        }
    }
}
