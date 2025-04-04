using BrandsA.Application.Abstractions;
using BrandsA.Application.IRepositories.Product;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Application.IRepositories.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Tokens;

namespace BrandsA.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IJwtGenerator, JwtGenerator>();
            
        }
    }
}
