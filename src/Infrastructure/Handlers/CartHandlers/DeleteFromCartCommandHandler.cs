using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CartHandlers
{
    public class DeleteFromCartCommandHandler : IRequestHandler<DeleteFromCartCommand, List<Cart>>
    {
        private readonly IUnitOfWork unitOfWork;
        public DeleteFromCartCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Cart>> Handle(DeleteFromCartCommand request, CancellationToken cancellationToken)
        {
            await unitOfWork.CartRepository.DeleteFromCart(request.userId,request.productId);
            var result = await unitOfWork.CartRepository.GetCartAsync(request.userId);
            return result;
        }
    }
}
