using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.Abstractions.Product
{
    public interface IProductService
    {
        public Task<bool> AddProductAsync(string name, string description, decimal price, int stock,string imageUrl);
        public Task<bool> UpdateProductAsync(Guid id, string name, string description, decimal price, int stock,string imageUrl);
        public bool DeleteProductAsync(Guid id);
        public Task<Domain.Entities.Product> GetProductByIdAsync(Guid id);
        public Task<List<Domain.Entities.Product>> GetAllProductsAsync();

    }
}
