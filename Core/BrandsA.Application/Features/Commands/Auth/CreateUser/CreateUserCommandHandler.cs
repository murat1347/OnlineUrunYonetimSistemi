using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrandsA.Application.Abstractions;
using MediatR;

namespace BrandsA.Application.Features.Commands.Auth.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest,CreateUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public CreateUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var result = await _authService.Register(request.User.Username, request.User.Password, request.User.Email,
                request.User.Address, request.User.NameSurname, request.User.PhoneNumber);

            if (result)
            {
                return new CreateUserCommandResponse();
            }
            else
            {
                throw new Exception("User Oluşturulamadı!");
            }
               
        }
    }
}
