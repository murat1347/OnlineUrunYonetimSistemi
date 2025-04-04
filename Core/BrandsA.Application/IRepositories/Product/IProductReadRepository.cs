using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities;

namespace BrandsA.Application.IRepositories.Product
{
    public interface IProductReadRepository : IReadRepository<Domain.Entities.Product>
    {
        public Task<Domain.Entities.Product> GetProduct(Guid id);
        public Task<List<Domain.Entities.Product>> GetAllProduct();

    }
}
