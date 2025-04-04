using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.IRepositories.Token
{
    public interface ITokenWriteRepository : IWriteRepository<Domain.Entities.Token>
    {
        public Task<bool> SaveToken(Domain.Entities.Token token, string username);
        public Task<Domain.Entities.Token> UpdateRefreshToken(string accesstoken, string username);
        public bool DeleteTokenById(string token);
    }
}
