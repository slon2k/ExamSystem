using Api.Handlers.Categories;
using Api.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Validators
{
    public class CategoryDtoValidator<T> : AbstractValidator<T> where T : CategoryDto
    {
        public CategoryDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }

    public class CreateCategoryRequestValidator : CategoryDtoValidator<CreateCategory.Request>
    {

    }

    public class UpdateCategoryRequestValidator : CategoryDtoValidator<UpdateCategory.Request>
    {

    }

}
