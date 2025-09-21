using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CartHandlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, List<Cart>>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetCartQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Cart>> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            return await unitOfWork.CartRepository.GetCartAsync(request.userId);
        }
    }
}
