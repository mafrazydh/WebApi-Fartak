using Application.Behaviors;
using Application.Commands;
using Application.Contracts;
using Application.Models.Identity;
using Application.Validators.CategoryValidators;
using Application.Validators.ProductValidators;
using Application.Validators.RoleValidators;
using Application.Validators.UserValidators;
using Domin.Entities;
using FluentValidation;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Infrastructure;

public static class ConfigurationServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    
        services.AddMediatR(option => { option.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); }).AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient<IUnitOfWork,UnitOfWork>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlingBehavior<,>));
        services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddTransient<IValidator<CreateRoleCommand>, CreateRoleCommandValidator>();
        services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
        services.AddTransient<IValidator<CreateCategotyAsyncCommand>,CreateCategoryCommandValidator >();
     
        services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
 


        //JWT System
        services.AddIdentity<User, Role>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 5;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.SignIn.RequireConfirmedEmail = false;
        }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();


        services.AddTransient<JwtTokenService>();
        services.Configure<JwtOption>(options => configuration.GetSection("JwtOption").Bind(options));
        services.AddAuthentication(option => {
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtOption:Issuer"],
                        ValidAudience = configuration["JwtOption:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOption:Key"])),
                    };
                });

        return services;
    }
    
    
}