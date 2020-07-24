using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Handlers.Categories;
using Api.Models;
using Api.Queries.Categories;
using ApplicationCore.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
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
        public async Task<ActionResult> Post([FromBody] CreateCategory.Request request)
        {
            var response = await _mediator.Send(request);
            if (response.Success)
            {
                return CreatedAtAction(nameof(Get), new { id = response.Category.Id }, response.Category);
            }
            return BadRequest(response.ErrorMessage);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CategoryDto categoryDto)
        {
            var request = new UpdateCategory.Request()
            {
                Id = id,
                Description = categoryDto.Description,
                Title = categoryDto.Title
            };

            var response = await _mediator.Send(request);
            
            if (response.Success)
            {
                return NoContent();
            };

            if (response.Category == null)
            {
                return NotFound();
            }

            return BadRequest(response.ErrorMessage);
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var response = await _mediator.Send(new DeleteCategory.Request() { Id = id });
            
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound();
        
        }
    }
}
