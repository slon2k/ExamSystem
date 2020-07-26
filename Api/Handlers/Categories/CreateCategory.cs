using Api.Errors;
using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Categories
{
    public class CreateCategory
    {    
        public class Request : CategoryDto, IRequest<CategoryDto>
        {
        }

        public class Handler : IRequestHandler<Request, CategoryDto>
        {
            private readonly IAsyncRepository<Category> _repository;
            private readonly IMapper _mapper;

            public Handler(IAsyncRepository<Category> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<CategoryDto> Handle(Request request, CancellationToken cancellationToken)
            {
                var category = new Category();
                _mapper.Map(request as CategoryDto, category);

                try
                {
                    var result = await _repository.CreateAsync(category);
                    return _mapper.Map<CategoryDto>(category);
                }
                catch (Exception)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Error = "Unable to create Category"});
                }             
            }
        }

    }


}
