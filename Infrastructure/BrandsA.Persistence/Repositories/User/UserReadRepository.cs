using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories.User;
using BrandsA.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BrandsA.Persistence.Repositories.User
{
    public class UserReadRepository :ReadRepository<Domain.Entities.User>, IUserReadRepository
    {
        private readonly ApplicationDbContext _context;

        public UserReadRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }


        public async Task<Domain.Entities.User> GetUser(string username)
        {
            try
            {

                var user = _context.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    throw new Exception("Kullanıcı bulunamadı.");
                }

                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
