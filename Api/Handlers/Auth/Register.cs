using Api.Errors;
using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Auth
{
    public class Register
    {
        public class Request : IRequest<AuthData>
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string ConfirmPassword { get; set; }
        }

        public class Handler : IRequestHandler<Request, AuthData>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<AuthData> Handle(Request request, CancellationToken cancellationToken)
            {
                var userByEmail = await _userManager.FindByEmailAsync(request.Email);
                if (userByEmail != null)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Error = "User already exists"});
                }

                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new AuthData { Token = _jwtGenerator.CreateToken(user, new List<string>()) };
                }

                return null;
            }
        }
    }
}
