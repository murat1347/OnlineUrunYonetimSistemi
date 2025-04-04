using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Persistence.Context;
using Microsoft.Extensions.Configuration;

namespace BrandsA.Persistence.Repositories.Token
{
    public class TokenReadRepository : ReadRepository<Domain.Entities.Token> ,ITokenReadRepository
    {
        private readonly ApplicationDbContext _context;

        public TokenReadRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }

       // public async Task<Token> 


    }
}
