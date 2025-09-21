using Application.Commands;
using Application.Contracts;
using Application.Exceptions;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CartHandlers
{
    public class UpdateCartQuantityCommandHandler : IRequestHandler<UpdateCartQuantityCommand, List<Cart>>
    {
        private readonly IUnitOfWork unitOfWork;

        public UpdateCartQuantityCommandHandler( IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<Cart>> Handle(UpdateCartQuantityCommand request, CancellationToken cancellationToken)
        {
            var newId = request.productId.ToString().Replace("{", "").Replace("}", "");

            var prod = unitOfWork.ProductRepository.GetByIdAsync(newId);
            if (prod.Result.Number > request.newQuantity)
            {
                await unitOfWork.CartRepository.UpdateCartQuantity(request.userId, request.productId, request.newQuantity);
                var result = await unitOfWork.CartRepository.GetCartAsync(request.userId);
                return result;
            }
            else
            {
                throw new CustomException("تعدادی که وارد کردید برای این محصول موجود نیست!");
            }
            
        }
    }
}
