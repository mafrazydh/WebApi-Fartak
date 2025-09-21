using Application.Models.Users;
using Domin.Entities;


namespace Application.Contracts
{
    public interface IRoleRepository 
    {
        Task<string> CreateAsync(RoleDto userDto);
        Task<List<Role>> AllRoles();
        Task<Role> GetRoleById(string id);
        Task<List<Role>> DeleteRole(string id);
        Task<string> AddRoleToUser(string userId, string roleId);
        Task<string> DeleteRoleFromUser(string userId, string roleId);
        
    }
}
