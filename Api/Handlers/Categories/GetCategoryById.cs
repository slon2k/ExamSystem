﻿using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
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
        public class Request : IRequest<CategoryDto>
        {
            public Guid Id { get; set; }
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
                var category = await _repository.GetByIdAsync(request.Id);
                
                if (category == null)
                {
                    return null;
                }
                
                return _mapper.Map<CategoryDto>(category);
            }
        }
    }
}
