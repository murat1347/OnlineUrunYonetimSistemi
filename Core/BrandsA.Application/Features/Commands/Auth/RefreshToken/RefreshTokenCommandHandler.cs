using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions;
using BrandsA.Application.Features.Commands.Auth.LoginUser;
using MediatR;

namespace BrandsA.Application.Features.Commands.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommandRequest,RefreshTokenCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshTokenAsync(request.RefreshToken, request.UserName);

            if (result.AccessToken.Length > 0)
            {
                return new RefreshTokenCommandResponse
                { 
                    Token= result
                };
            }
            else
            {

                throw new Exception("Login Başarısız");
            }

        }
    }
}
