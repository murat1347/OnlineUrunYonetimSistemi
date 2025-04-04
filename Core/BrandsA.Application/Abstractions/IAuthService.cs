using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Domain.Entities;

namespace BrandsA.Application.Abstractions
{
    public interface IAuthService
    {
        Task<Token> LoginAsync(string username, string password, int accessTokenLifeTime);
        public Task<Token> RefreshTokenAsync(string refreshToken, string username);

        public Task<bool> Register(string username, string password, string email, string adress, string namesurname,
            string phoneNumber);

    }
}
