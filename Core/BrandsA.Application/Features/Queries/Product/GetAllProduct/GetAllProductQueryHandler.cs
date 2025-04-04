using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions.Product;
using MediatR;

namespace BrandsA.Application.Features.Queries.Product.GetAllProduct
{
    internal class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest,GetAllProductQueryResponse>
    {
        private readonly IProductService _productService;

        public GetAllProductQueryHandler(IProductService productService)
        {
            _productService= productService;
        }

        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllProductsAsync();
            return new GetAllProductQueryResponse { Products = result };
        }
    }
}
