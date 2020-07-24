using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Categories
{
    public class DeleteCategory
    {
        public class Response
        {
            public bool Success { get; set; } = false;
        }
        
        public class Request: IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Handler: IRequestHandler<Request, Response>
        {
            private readonly IAsyncRepository<Category> _repository;

            public Handler(IAsyncRepository<Category> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(request.Id);
                if (category == null)
                {
                    return new Response();
                }
                try
                {
                    await _repository.DeleteAsync(category);
                    return new Response { Success = true };
                }
                catch (Exception)
                {
                    return new Response();
                }
                
            }
        }
    }
}
