using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetArticlesAsync()
        {
            var category = await _categoryService.GetCategoriesAsync();
            return Ok(category);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(category); // 200 OK HTTP response
        }

        // POST api/<CategoryController>
        // POST api/<ArticleController>
        [HttpPost]
        public async Task<ActionResult<Category>> AddACategory(Category category)
        {
            var createdCategory = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory); // 201 Created HTTP response
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
