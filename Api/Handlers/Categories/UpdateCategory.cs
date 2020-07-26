using Api.Errors;
using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Categories
{
    public class UpdateCategory
    {
        public class Request: CategoryDto, IRequest<CategoryDto>
        {
        }

        public class Handler: IRequestHandler<Request, CategoryDto>
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
                var category = await _repository.GetByIdAsync((Guid)request.Id);
                
                if (category == null)
                {
                    return null;
                }

                _mapper.Map(request as CategoryDto, category);

                try
                {
                    await _repository.UpdateAsync(category);
                    return _mapper.Map<CategoryDto>(category);
                }
                catch (Exception)
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Error = "Unable to update Category" }); ;
                }
                
            }
        }

    }
}
