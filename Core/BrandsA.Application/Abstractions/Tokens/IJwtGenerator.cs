using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities;

namespace BrandsA.Application.Abstractions.Tokens
{
    public interface IJwtGenerator
    {
        public Task<Token> CreateAccessTokenAsync(int minute, string username);
        Token CreateRefreshToken(int minute);
    }
}
