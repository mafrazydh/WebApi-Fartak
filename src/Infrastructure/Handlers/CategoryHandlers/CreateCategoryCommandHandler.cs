using Application.Commands;
using Application.Contracts;
using Domin.Entities;
using MediatR;


namespace Infrastructure.Handlers.CategoryHandlers
{
    public class CreateCategotyAsyncCommandHandler : IRequestHandler<CreateCategotyAsyncCommand, Category>
    {
        private readonly IUnitOfWork unitOfWork;

        public CreateCategotyAsyncCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<Category> Handle(CreateCategotyAsyncCommand request, CancellationToken cancellationToken)
        {
            var value = await unitOfWork.CategoryRepository.CreateAsync(request.categoryDto);
       
            unitOfWork.CompleteAsync();
            return value;

        }
    }
}
