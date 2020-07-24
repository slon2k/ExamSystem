using Api.Models;
using Api.Queries.Categories;
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
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<Category>>
    {
        private readonly IAsyncRepository<Category> _repository;

        public GetAllCategoriesHandler(IAsyncRepository<Category> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Category>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.ListAllAsync();
        }
    }
}
