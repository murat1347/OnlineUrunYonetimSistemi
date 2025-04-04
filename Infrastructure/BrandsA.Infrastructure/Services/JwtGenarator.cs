using BrandsA.Application.Abstractions.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Dtos;
using BrandsA.Application.IRepositories.Token;
using BrandsA.Application.IRepositories.User;
using BrandsA.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BrandsA.Infrastructure
{
    public class JwtGenerator : IJwtGenerator
    {

        private readonly IUserWriteRepository _userWriteRepository;
        private readonly IUserReadRepository _userReadRepository;
        private readonly ITokenReadRepository _tokenReadRepository;
        private readonly TokenKeys _tokenkeys;
        private readonly TokenTime _tokenTime;

        public JwtGenerator(IConfiguration config, IOptions<TokenTime> tokentime,
            IUserWriteRepository userWriteRepository, IUserReadRepository userReadRepository,
            ITokenReadRepository tokenReadRepository, IOptions<TokenKeys> tokenkeys)
        {
            _userWriteRepository = userWriteRepository;
            _userReadRepository = userReadRepository;
            _tokenReadRepository = tokenReadRepository;
            _tokenkeys = tokenkeys.Value;
            _tokenTime = tokentime.Value;
        }

        public async Task<Token> CreateAccessTokenAsync(int minute, string username)
        {
            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenkeys.TokenKey));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.Now.AddMinutes(minute),
                SigningCredentials = creds,
                Audience = _tokenkeys.Audience,
                Issuer = _tokenkeys.Issuer,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshtokens = CreateRefreshToken(_tokenTime.RefreshTokenTimeOut);
            var res = tokenHandler.WriteToken(token);
            Token acccessToken = new Token();

            var user = await _userReadRepository.GetUser(username);

            acccessToken.UserId = user.Id;
            acccessToken.AccessToken = res;
            acccessToken.RefreshToken = refreshtokens.RefreshToken;
            acccessToken.RefreshTokenExpiration = refreshtokens.RefreshTokenExpiration;

            return acccessToken;
        }

        public Token CreateRefreshToken(int minute)
        {
            var _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenkeys.TokenKey));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.Now.AddMinutes(minute),
                SigningCredentials = creds,
                Issuer = _tokenkeys.Issuer,
                Audience = _tokenkeys.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var res = tokenHandler.WriteToken(token);
            var acctoken = tokenHandler.WriteToken(token);
            Token acccessToken = new Token();
            acccessToken.RefreshToken = res;
            acccessToken.AccessToken = acctoken;
            acccessToken.RefreshTokenExpiration = DateTime.Now.AddMinutes(_tokenTime.RefreshTokenTimeOut);
            return acccessToken;



        }
    }
}
