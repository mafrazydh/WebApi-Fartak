using Application.Models.Users;
using Domin.Entities;


namespace Application.Contracts
{
    public interface IUserRepository
    {
        Task<string> CreateAsync(UserDto userDto);
        Task<List<User>> AllUsers();
        Task<User> GetUserById(string id);
        Task<List<User>> DeleteUser(string id);
        Task<User> UpdateAsync(string id, UserDto userDto);
        Task<List<Role>> UserRoles(string id);

    }

}
