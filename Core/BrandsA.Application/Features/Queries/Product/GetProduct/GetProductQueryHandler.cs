using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using MediatR;

namespace BrandsA.Application.Features.Queries.Product.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQueryRequest,GetProductQueryResponse>
    {
        private readonly IProductService _productService;

        public GetProductQueryHandler(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<GetProductQueryResponse> Handle(GetProductQueryRequest request, CancellationToken cancellationToken)
        {
          var result  = await _productService.GetProductByIdAsync(request.Id);
            if (result != null)
            {
                return new GetProductQueryResponse
                {
                    Product = result
                };
            }
            else
            {
                throw new Exception("Ürün Bulunamadı!");
            }
        }
    }
}
