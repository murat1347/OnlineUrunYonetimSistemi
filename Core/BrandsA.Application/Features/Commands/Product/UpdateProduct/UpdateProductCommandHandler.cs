using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using MediatR;

namespace BrandsA.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest,UpdateProductCommandResponse>
    {
        private readonly IProductService _productService;

        public UpdateProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var result =await _productService.UpdateProductAsync(request.Product.Id, request.Product.Name,
                request.Product.Description, request.Product.Price, request.Product.Stock,request.Product.ImageUrl);

            if (result)
            {
                return new UpdateProductCommandResponse();
            }
            else
            {
                throw new Exception("Güncelleme Başarısız!");
            }
        }
    }
}
