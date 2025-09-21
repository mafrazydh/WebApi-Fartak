using Application.Commands;
using Application.Contracts;
using Application.Exceptions;
using MediatR;


namespace Infrastructure.Handlers.CartHandlers
{
    public class AddToCartCommandHandler : IRequestHandler<AddToCartCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public AddToCartCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            var newId = request.productId.ToString().Replace("{", "").Replace("}", "");

            var prod = unitOfWork.ProductRepository.GetByIdAsync(newId);
            if (prod.Result.Number > request.quantity)
            {
                var res = unitOfWork.CartRepository.AddToCart(request.userId, request.productId, request.quantity);
                unitOfWork.CompleteAsync();
                return res;
            }
            else {
                throw new CustomException("تعدادی که وارد کردید برای این محصول موجود نیست!");
            }
        }
    }
}
