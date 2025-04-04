using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.IRepositories.User
{
    public interface IUserReadRepository : IReadRepository<Domain.Entities.User>
    {
        public Task<Domain.Entities.User> GetUser(string username);
    }
}
