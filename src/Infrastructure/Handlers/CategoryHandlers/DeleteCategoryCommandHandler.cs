using Application.Commands;
using Application.Contracts;
using MediatR;


namespace Infrastructure.Handlers.CategoryHandlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var res = unitOfWork.CategoryRepository.DeleteCategory(request.categoryId);
            unitOfWork.CompleteAsync();
            return res;
        }
    }
}
