using Application.Contracts;
using Application.Exceptions;
using Application.Models.Users;
using AutoMapper;
using Domin.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IValidator<RoleDto> validator;
        private readonly IMapper mapper;

        public RoleRepository(ApplicationDbContext dbContext, IValidator<RoleDto> validator, IMapper mapper) /*: base(context)*/
        {
            this.validator = validator;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }


        public async Task<List<Role>> AllRoles()
        {
            List<Role> roles = await dbContext.Roles.ToListAsync();
            return roles;
        }

        public async Task<string> CreateAsync(RoleDto roleDto)
        {
            var role = dbContext.Roles.Where(d => d.Name == roleDto.Name)?.SingleOrDefault();
            if (role is not null)
            {
                throw new Exception("Please Use From Other Email");
            }
            var newRole = mapper.Map<Role>(roleDto);
            var add = await dbContext.Roles.AddAsync(newRole);
            var save = dbContext.SaveChanges();
            return newRole.Id;


        }

        public async Task<List<Role>> DeleteRole(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var role = await dbContext.Roles.FirstOrDefaultAsync(d => d.Id == id);
                if (role is not null)
                {
                    dbContext.Remove(role);
                    await dbContext.SaveChangesAsync();
                    List<Role> roles = dbContext.Roles.ToList();
                    return roles;

                }
                throw new CustomNotFoundException("Role", id);

            }
            throw new CustomNotFoundException("Role", id);

        }

        public async Task<Role> GetRoleById(string id)
        {
            return await dbContext.Roles.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<string> AddRoleToUser(string userId, string roleId)
        {
            var user = dbContext.Users.FirstOrDefault(d => d.Id == userId);
            var role = dbContext.Roles.FirstOrDefault(d => d.Id == roleId);
            if (user == null || role == null)
            {
                throw new CustomNotFoundException("Role", roleId);
                throw new CustomNotFoundException("User", userId);
            }
            var result = await dbContext.UserRoles.AddAsync(new IdentityUserRole<string> { RoleId = role.Id, UserId = user.Id });
            await dbContext.SaveChangesAsync();
            return "مقام با موفقیت به کاربر داده شده است";
        }

        public async Task<string> DeleteRoleFromUser(string userId, string roleId)
        {
            var userRole = dbContext.UserRoles.FirstOrDefault(d => d.RoleId == roleId && d.UserId == userId);
            if (userRole == null)
            {
                throw new CustomNotFoundException("User", "برای این یوزر هیچ مقامی نیست");
            }

            var result = dbContext.UserRoles.Remove(userRole);
            await dbContext.SaveChangesAsync();
            return "مقام با موفقیت برای کاربر حذف شده است";
        }

    }
}
