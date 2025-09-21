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
    public class UserRepository :  IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UserRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<List<User>> AllUsers()
        {
            List<User> users =  await dbContext.Users.ToListAsync();
            return users;
        }

        public async Task<string> CreateAsync(UserDto userDto)
        {
            var user = dbContext.Users.Where(d => d.Email == userDto.Email)?.SingleOrDefault();
            if (user is not null)
            {
                throw new CustomException("لطفا از یک ایمیل دیگر استفاده کنید!");
            }
            var newUser = mapper.Map<User>(userDto);
            newUser.CreatedDate = DateTime.Now;
            newUser.Profile = userDto.ProfileStr;
            var passwordHasher = new PasswordHasher<User>();
            newUser.UserName = userDto.Email;
            newUser.PasswordHash = passwordHasher.HashPassword(newUser,userDto.Password);
            var add = await dbContext.Users.AddAsync(newUser);
            var save = dbContext.SaveChanges();
            return newUser.Id;
        }

        public async Task<List<User>> DeleteUser(string id)
        {
            if (!string.IsNullOrEmpty(id)) 
            {
                var user = await dbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
                if (user is not null) 
                {
                    dbContext.Remove(user);
                    await dbContext.SaveChangesAsync();
                    List<User> users = dbContext.Users.ToList();
                    return users;

                }
                
                    throw new CustomNotFoundException("User", id);
               

            }
            
            throw new CustomException($"فیلد آیدی '{id}'  نباید خالی باشد .", 404, "کاربر یافت نشد");

        }

        public async Task<User> GetUserById(string id)
        {
            return await dbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<User> UpdateAsync(string id,UserDto userDto)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(d => d.Id == id );
            if (user == null)
            {
                throw new CustomNotFoundException("User", id);
            }
            else{ 
                if(userDto.FullName is not null) { user.FullName = userDto.FullName; }
                if(userDto.PhoneNumber is not null) { user.PhoneNumber = userDto.PhoneNumber; }
                if(userDto.ProfileStr is not null) { user.Profile = userDto.ProfileStr; }
                if(userDto.Email is not null) { user.Email = userDto.Email; }
            }


            var entity = dbContext.Entry(user);
            entity.State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return user;
        }
        
        public async Task<List<Role>> UserRoles(string id)
        {

            var user = await dbContext.Users.FirstOrDefaultAsync(d => d.Id == id );

            if (user == null)
            {
                throw new CustomNotFoundException("User", id);
            }
            var userRoles = dbContext.UserRoles.Where(d => d.UserId == id).ToList();
            var roles = new List<Role>();
            foreach (var i in userRoles) 
            {
                foreach (var r in dbContext.Roles.ToList()) {
                    if (i.RoleId == r.Id) {
                        roles.Add(r);
                    }
                }
            }

            return roles;
        }

       
    }
}
