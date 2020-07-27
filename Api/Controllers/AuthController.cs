using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Handlers.Auth;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthData>> Login(Login.Request request)
        {
            var result = await _mediator.Send(request);

            if (result != null)
            {
                return result;
            }

            return Unauthorized("Unable to authorize user");
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthData>> Register(Register.Request request)
        {
            var result = await _mediator.Send(request);

            if (result != null)
            {
                return result;
            }

            return BadRequest("Unable to create user");
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<User.Response>> GetCurrentUser()
        {
            var result = await _mediator.Send(new User.Request());
            return result;
        }
    }
}
