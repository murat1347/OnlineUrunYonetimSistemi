using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities;

namespace BrandsA.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandResponse
    {
        public Token Token { get; set; }
    }
}
