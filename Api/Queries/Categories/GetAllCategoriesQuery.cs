using Api.Models;
using ApplicationCore.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Queries.Categories
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<Category>>
    {

    }
}
