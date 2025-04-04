using BrandsA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.Features.Commands.Auth.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
}
