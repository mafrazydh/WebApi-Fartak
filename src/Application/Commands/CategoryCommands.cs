using Application.Models.Category;
using Domin.Entities;
using MediatR;


namespace Application.Commands
{
   
   public record CreateCategotyAsyncCommand(CategoryDto categoryDto) : IRequest<Category>;
   public record DeleteCategoryCommand(string categoryId) : IRequest;

}
