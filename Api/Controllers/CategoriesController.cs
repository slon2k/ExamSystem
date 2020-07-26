using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Handlers.Categories;
using Api.Models;
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
            var result = await _mediator.Send(new GetAllCategories.Request());
            return Ok(result);
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(Guid id)
        {
            var response = await _mediator.Send(new GetCategoryById.Request() { Id = id });
            
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCategory.Request request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction("Get", new { id = response.Id }, response);
        }

        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UpdateCategory.Request request)
        {
            request.Id = id;

            var response = await _mediator.Send(request);
     
            if (response == null)
            {
                return NotFound();
            }

            return NoContent();
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
