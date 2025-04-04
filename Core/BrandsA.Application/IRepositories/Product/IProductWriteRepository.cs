using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.IRepositories.Product
{
    public interface IProductWriteRepository : IWriteRepository<Domain.Entities.Product>
    {
        public Task<bool> AddProduct(Domain.Entities.Product product);
        public Task<bool> DeleteProduct(Guid id);
        public Task<bool> UpdateProduct(Domain.Entities.Product product);


    }
}
