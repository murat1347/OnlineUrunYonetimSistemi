using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories.Product;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BrandsA.Persistence.Repositories.Product
{
    public class ProductReadRepository : ReadRepository<Domain.Entities.Product>, IProductReadRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductReadRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Product>> GetAllProduct()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Domain.Entities.Product> GetProduct(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(data => data.Id == id);
        }
    }
}
