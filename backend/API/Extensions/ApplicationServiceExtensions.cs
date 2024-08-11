using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITokenService, TokenService>();

            //Auto mapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            //Redis
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = ConfigurationOptions.Parse(
                    config.GetConnectionString("Redis") ?? "localhost",
                    true
                );
                return ConnectionMultiplexer.Connect(configuration);
            });
            services.AddScoped<IBasketRepository, BasketRepository>();

            //DbContext
            services.AddDbContext<StoreContext>(opts =>
                opts.UseSqlite(config.GetConnectionString("DefaultConnection"))
            );

            services.AddDbContext<AppIdentityDbContext>(opt =>
                opt.UseSqlite(config.GetConnectionString("IdentityConnection"))
            );

            //Api Validation Error
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext
                        .ModelState.Where(e => e.Value?.Errors.Count > 0)
                        .SelectMany(x => x.Value?.Errors ?? Enumerable.Empty<ModelError>())
                        .Select(x => x.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ApiValidationErrorResponse(errors);
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .WithOrigins("http://localhost:4200");
                    }
                );
            });

            return services;
        }
    }
}
