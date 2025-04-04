using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using MediatR;
using Serilog;

namespace BrandsA.Application.Features.Commands.Product.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest,DeleteProductCommandResponse>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = _productService.DeleteProductAsync(request.Id);
                return new DeleteProductCommandResponse { Complate = result };
            }
            catch (Exception e)
            {
                Log.Error("Ürün Silinemedi");
                throw new Exception("Ürün Silinemedi!");
            }

           

            throw new NotImplementedException();
        }
    }
}
