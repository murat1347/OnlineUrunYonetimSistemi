using BrandsA.Application.Abstractions.Tokens;
using BrandsA.Application.Abstractions;
using BrandsA.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using BrandsA.Application.IRepositories;
using BrandsA.Application.IRepositories.Product;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Application.IRepositories.User;
using BrandsA.Persistence.Repositories;
using BrandsA.Persistence.Repositories.Product;
using BrandsA.Persistence.Repositories.Token;
using BrandsA.Persistence.Repositories.User;

namespace BrandsA.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection)
        {
           // serviceCollection.AddScoped<IJwtGenerator, JwtGenerator>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            serviceCollection.AddScoped<IProductReadRepository, ProductReadRepository>();
            serviceCollection.AddScoped<ITokenWriteRepository, TokenWriteRepository>();
            serviceCollection.AddScoped<ITokenReadRepository, TokenReadRepository>();
            serviceCollection.AddScoped<IUserReadRepository, UserReadRepository>();
            serviceCollection.AddScoped<IUserWriteRepository, UserWriteRepository>();
            serviceCollection.AddScoped<IAuthService, AuthService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
        }
    }
}
