using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BrandsA.Application.Features.Commands.Product.AddProduct
{
    public class AddProductCommandRequest : IRequest<AddProductCommandResponse>
    {
        public Domain.Entities.Product Product { get; set; }
    }
}
