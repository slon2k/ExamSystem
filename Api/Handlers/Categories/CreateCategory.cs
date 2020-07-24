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
    public class CreateCategory
    {    
        public class Response
        {
            public CategoryDto Category { get; set; }
            public bool Success { get; set; } = false;
            public string ErrorMessage { get; set; } = "";
        }

        public class Request : IRequest<Response>
        {
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAsyncRepository<Category> _repository;

            public Handler(IAsyncRepository<Category> repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var category = new Category
                {
                    Title = request.Title,
                    Description = request.Description
                };
                try
                {
                    var result = await _repository.CreateAsync(category);
                    var categoryDto = new CategoryDto
                    {
                        Id = category.Id,
                        Title = category.Title,
                        Description = category.Description
                    };
                    return new Response
                    {
                        Category = categoryDto,
                        Success = true,
                    };
                }
                catch (Exception)
                {
                    return new Response
                    {
                        Category = null,
                        Success = false,
                        ErrorMessage = "Unable to create category " + request.Title
                    };
                }             
            }
        }

    }


}
