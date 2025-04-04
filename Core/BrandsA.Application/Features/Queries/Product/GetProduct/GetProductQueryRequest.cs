using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BrandsA.Application.Features.Queries.Product.GetProduct
{
    public class GetProductQueryRequest : IRequest<GetProductQueryResponse>
    {
       public Guid Id { get; set; }
    }
}
