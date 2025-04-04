using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using BrandsA.Application.IRepositories.Product;
using BrandsA.Domain.Entities;
using Serilog;

namespace BrandsA.Persistence.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;

        public ProductService(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }
        public async Task<bool> AddProductAsync(string name, string description, decimal price, int stock,string imageUrl)
        {
            try
            {
                Product product = new Product
                {
                    Name = name,
                    Description = description,
                    Price = price,
                    Stock = stock,
                    ImageUrl = imageUrl
                };


                await _productWriteRepository.AddProduct(product);
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Ürün Eklenemedi! Hata: ${e.message}");
                throw new Exception("Ürün Eklenemedi!");
            }
        }

        public async Task<bool> UpdateProductAsync(Guid id, string name, string description, decimal price, int stock,string imageUrl)
        {
            try
            {
                Product product = new Product
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Price = price,
                    Stock = stock,
                    ImageUrl = imageUrl
                };
                await _productWriteRepository.UpdateProduct(product);
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Ürün Güncellenemedi! Hata: ${e.message}");
                throw new Exception("Ürün Güncellenemedi!");
            }
        }

        public bool DeleteProductAsync(Guid id)
        {
            try
            {
                 _productWriteRepository.DeleteProduct(id);
                 return true;
            }
            catch (Exception e)
            {
                Log.Error("Ürün Silinemedi! Hata: ${e.message}");
                throw new Exception("Ürün Silinemedi!");
            }
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            try
            {
                return await _productReadRepository.GetProduct(id);
            }
            catch (Exception e)
            {
                Log.Error("Ürün Bulunamadı! Hata: ${e.message}");
                throw new Exception("Ürün Bulunamadı!");
            }
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            try
            {
               return await _productReadRepository.GetAllProduct();
            }
            catch (Exception e)
            {
                Log.Error("Ürünler Bulunamadı! Hata: ${e.message}");
                throw new Exception("Ürün Bulunamadı!");
            }
        }
    }
}
