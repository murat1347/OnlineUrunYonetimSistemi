using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.IRepositories.User
{
    public interface IUserWriteRepository  : IWriteRepository<Domain.Entities.User>
    {
        public Task<bool> SignUpUser(Domain.Entities.User user);
    }
}
