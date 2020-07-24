using Api.Models;
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
    public class GetCategoryById
    {
        public class Query : IRequest<Response>
        {
            public Guid Id { get; set; }
        }
        
        public class Response 
        {
            public CategoryDto Category { get; set; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IAsyncRepository<Category> _repository;

            public Handler(IAsyncRepository<Category> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await _repository.GetByIdAsync(request.Id);
                var categoryDto = category == null ? null : new CategoryDto 
                    { 
                        Id = category.Id, 
                        Description = category.Description, 
                        Title = category.Title 
                    };
                return new Response { Category = categoryDto };
            }
        }
    }
}
