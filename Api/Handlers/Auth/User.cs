using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Auth
{
    public class User
    {
        public class Response
        {
            public string UserName { get; set; }
        }
        
        public class Request : IRequest<Response>
        {
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IUserAccessor _userAccessor;
            private readonly UserManager<AppUser> _userManager;

            public Handler(IUserAccessor userAccessor, UserManager<AppUser> userManager)
            {
                _userAccessor = userAccessor;
                _userManager = userManager;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var userName = _userAccessor.GetCurrentUserName();
                var user = await _userManager.FindByNameAsync(userName);
                return new Response() { UserName = user.UserName };
            }
        }
    }
}
