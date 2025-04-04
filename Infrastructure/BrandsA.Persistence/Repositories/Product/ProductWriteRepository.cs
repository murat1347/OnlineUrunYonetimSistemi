using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories.Product;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BrandsA.Persistence.Repositories.Product
{
    public class ProductWriteRepository : WriteRepository<Domain.Entities.Product>, IProductWriteRepository
    {

        private readonly ApplicationDbContext _context;

        public ProductWriteRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Domain.Entities.Product product)
        {
            return await AddAsync(product);
        }

        public async Task<bool> DeleteProduct(Guid id)
        {
            var entity = _context.Products.FirstOrDefault(x => x.Id == id);

            if (entity != null)
            {
                _context.Products.Remove(entity);
                SaveChanges();
                return true;
            }
            else
            {
                throw new Exception("Ürün Bulunamadı");
            }

               
        }

        public async Task<bool> UpdateProduct(Domain.Entities.Product product)
        {
            try
            {
                var result = _context.Products.FirstOrDefault(x => x.Id == product.Id);

                _context.Entry(result).CurrentValues.SetValues(product);

                // Change the state to Modified
                _context.Entry(result).State = EntityState.Modified;
                SaveChanges();



                return true;
            }
            catch (Exception e)
            {
                Log.Error("Güncelleme Başarısız! {e.Message}");
                throw new Exception($"Güncelleme Başarısız! {e.Message}");
            }
          
        }

    }
}
