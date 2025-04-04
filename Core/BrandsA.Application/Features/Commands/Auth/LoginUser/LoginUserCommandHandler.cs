using BrandsA.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrandsA.Application.Features.Commands.Auth.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            var result = await _authService.LoginAsync(request.UserName, request.Password,30);

            if (result.AccessToken.Length > 0)
            {
                return new LoginUserCommandResponse
                {
                    Token = result
                };
            }
            else
            {

                throw new Exception("Login Başarısız");
            }
        }
    }
}
