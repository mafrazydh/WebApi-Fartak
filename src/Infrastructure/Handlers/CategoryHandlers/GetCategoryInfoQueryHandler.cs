using Application.Contracts;
using Application.Queries;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CategoryHandlers
{
    public class GetCategoryInfoQueryHandler : IRequestHandler<GetCategoryInfoQuery, Category>
    {
        private readonly IUnitOfWork unitOfWork;

        public GetCategoryInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Category> Handle(GetCategoryInfoQuery request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.CategoryRepository.GetCategoryInfo(request.id);
            unitOfWork.CompleteAsync();
            return value;

        }
    }
}
