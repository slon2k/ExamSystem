using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Handlers.Categories
{
    public class GetAllCategories
    {
        public class Request : IRequest<IEnumerable<CategoryDto>>
        {
        }

        public class Handler : IRequestHandler<Request, IEnumerable<CategoryDto>>
        {
            private readonly IAsyncRepository<Category> _repository;
            private readonly IMapper _mapper;

            public Handler(IAsyncRepository<Category> repository, IMapper mapper)
            {
                _repository = repository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CategoryDto>> Handle(Request request, CancellationToken cancellationToken)
            {
                var categories = await _repository.ListAllAsync();
                return _mapper.Map<IList<CategoryDto>>(categories);
            }
        }

    }
}
