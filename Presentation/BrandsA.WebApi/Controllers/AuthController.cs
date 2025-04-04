using BrandsA.Application.Features.Commands.Auth.CreateUser;
using BrandsA.Application.Features.Commands.Auth.LoginUser;
using BrandsA.Application.Features.Commands.Auth.RefreshToken;
using BrandsA.Application.Features.Commands.Product.UpdateProduct;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrandsA.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost] // Kullanıcı Girişi
        public async Task<IActionResult> LoginbyUsername(LoginUserCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                LoginUserCommandResponse response = await _mediator.Send(request);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Register")] // Kullanıcı Kayıt
        public async Task<IActionResult> Signup(CreateUserCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                CreateUserCommandResponse response = await _mediator.Send(request);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpPost("RefreshToken")] // Access Token Yenileme
        public async Task<IActionResult> RefreshToken(RefreshTokenCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                RefreshTokenCommandResponse response = await _mediator.Send(request);

                return Ok(response);
            }
            else
            {
                return BadRequest();
            }
        }


    }
}
