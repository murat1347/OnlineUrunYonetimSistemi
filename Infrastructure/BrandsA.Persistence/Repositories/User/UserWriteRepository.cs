using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.IRepositories.User;
using BrandsA.Domain.Entities;
using BrandsA.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BrandsA.Persistence.Repositories.User
{
    public class UserWriteRepository : WriteRepository<Domain.Entities.User>, IUserWriteRepository
    {
        private readonly ApplicationDbContext _context;

        public UserWriteRepository(ApplicationDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
        }


        public async Task<bool> SignUpUser(Domain.Entities.User user)
        {
            try
            {
               
                if (string.IsNullOrEmpty(user.Password))
                {
                    throw new ArgumentException("Password zorunludur");
                }

              
                if (await _context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email))
                {
                    return false; 
                }
                user.Id = Guid.NewGuid();
                user.Roles.Add(new UserRole{RoleName = "User",RoleNumber = 2,User = user});
                await _context.Users.AddAsync(user);
                int affectedRows = await _context.SaveChangesAsync();

                return affectedRows > 0;
            }
            catch (DbUpdateException ex)
            {
             
                Log.Error($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                
                Log.Error($"Unexpected error: {ex.Message}");
                return false;
            }
        }

    }
}
