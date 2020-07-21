using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IAsyncRepository<Category> _repository;

        public CategoriesController(IAsyncRepository<Category> repository)
        {
            _repository = repository;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetAll()
        {
            return await _repository.ListAllAsync();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
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
