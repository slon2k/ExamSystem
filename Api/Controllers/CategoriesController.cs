using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Handlers.Categories;
using Api.Models;
using Api.Queries.Categories;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IAsyncRepository<Category> _repository;
        private readonly IMediator _mediator;

        public CategoriesController(IAsyncRepository<Category> repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var query = new GetAllCategoriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(Guid id)
        {
            var query = new GetCategoryById.Query() { Id = id };
            var response = await _mediator.Send(query);
            if (response.Category == null)
            {
                return NotFound();
            }
            return Ok(response.Category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<Category> Post([FromBody] CategoryDto categoryDto)
        {
            var category = new Category
            {
                Title = categoryDto.Title,
                Description = categoryDto.Description
            };
            return await _repository.CreateAsync(category);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            if (categoryDto.Id != id)
            {
                return BadRequest();
            }
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            category.Title = categoryDto.Title;
            category.Description = categoryDto.Description;
            await _repository.UpdateAsync(category);
            return NoContent();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            await _repository.DeleteAsync(category);
            return NoContent();
        }
    }
}
