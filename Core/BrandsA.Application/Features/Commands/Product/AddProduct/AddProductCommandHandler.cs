using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using MediatR;

namespace BrandsA.Application.Features.Commands.Product.AddProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest,AddProductCommandResponse>
    {
        private readonly IProductService _productService;

        public AddProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
           var result =  await _productService.AddProductAsync(request.Product.Name, request.Product.Description, request.Product.Price,
                request.Product.Stock,request.Product.ImageUrl);

           if (result)
           {
               return new AddProductCommandResponse();
           }
           else
           {
               throw new Exception("Ürün Eklenemedi!");
           }
        }
    }
}
