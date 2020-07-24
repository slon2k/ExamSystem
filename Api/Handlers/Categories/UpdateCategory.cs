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
    public class UpdateCategory
    {
        public class Response
        {
            public CategoryDto Category { get; set; }
            public bool Success { get; set; } = false;
            public string ErrorMessage { get; set; }
        }

        public class Request: IRequest<Response>
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
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
                    return new Response { Category = null };
                }

                category.Title = request.Title;
                category.Description = request.Description;

                try
                {
                    await _repository.UpdateAsync(category);
                    
                    return new Response
                    {
                        Success = true,
                        Category = new CategoryDto 
                        { 
                            Id = category.Id,
                            Description = category.Description,
                            Title = category.Title
                        } 
                    };
                }
                catch (Exception)
                {
                    return new Response { ErrorMessage = "Unable to update category " + category.Title };
                }
                
            }
        }

    }
}
