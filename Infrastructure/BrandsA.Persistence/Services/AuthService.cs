using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions;
using BrandsA.Application.Abstractions.Tokens;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Application.IRepositories.User;
using BrandsA.Domain.Entities;
using Serilog;

namespace BrandsA.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly ITokenWriteRepository _tokenWriteRepository;
        public AuthService(IUserReadRepository userReadRepository,IUserWriteRepository userWriteRepository, IJwtGenerator jwtGenerator, ITokenWriteRepository tokenWriteRepository)
        {
            _userWriteRepository= userWriteRepository;
            _jwtGenerator = jwtGenerator;
            _tokenWriteRepository = tokenWriteRepository;
            _userReadRepository = userReadRepository;
        }
        public async Task<Token> LoginAsync(string username, string password, int accessTokenLifeTime)
        {
            try
            {
                var user = await _userReadRepository.GetUser(username);
                if (user == null)
                {
                    throw new Exception("Kullanıcı Bulunamadı");
                }

                string hashedPassword = ComputeSha256Hash(password);

                if (user.Password == hashedPassword)
                {
                    var token = await _jwtGenerator.CreateAccessTokenAsync(accessTokenLifeTime, username);
                    await _tokenWriteRepository.SaveToken(token, username);

                    return token;
                }
                else
                {
                    throw new Exception("Parolalar uyuşmuyor.");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
                Console.WriteLine(ex.ToString());
                throw;
            }
        }


        public async Task<bool> Register(string username,string password,string email,string adress,string namesurname,string phoneNumber)
        {
            try
            {
                User user = new User();
                user.Username = username;
                user.Password = ComputeSha256Hash(password);
                user.Email = email;
                user.Address = adress;
                user.NameSurname = namesurname;
                user.PhoneNumber = phoneNumber;
                user.Roles = new List<UserRole>();


                await _userWriteRepository.SignUpUser(user);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<Token> RefreshTokenAsync(string refreshToken,string username)
        {
            try
            {
                return await _tokenWriteRepository.UpdateRefreshToken(refreshToken, username);
               
            }
            catch (Exception e)
            {
                Log.Error("RefreshToken Üretilemedi!");
                throw;
            }

        }


        private static string ComputeSha256Hash(string rawData)
        {
            // SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - byte dizisini döndürür  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // byte dizisini bir string'e dönüştür
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
