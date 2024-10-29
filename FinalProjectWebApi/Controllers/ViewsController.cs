using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewsController : ControllerBase
    {

        private readonly IViewsService _viewsService;

        public ViewsController(IViewsService viewsService)
        {
            _viewsService = viewsService;

        }
        // GET: api/<ViewsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Views>>> GetViewsAsync()
        {
            var result = await _viewsService.GetViewsAsync();
            return Ok(result);
        }

        // GET api/<ViewsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Views>> GetViewsById(int id)
        {
            var views = await _viewsService.GetViewsByIdAsync(id);
            if (views == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(views); // 200 OK HTTP response
        }
        [HttpGet("{articleId}/views")]
        public async Task<IActionResult> GetViewsByArticleId(int articleId)
        {
            var views = await _viewsService.GetViewsByArticleId(articleId);
            return Ok(views);
        }
        [HttpGet("{articleId}/count")]
        public async Task<IActionResult> GetViewsCountByArticleId(int articleId)
        {
            int viewsCount = await _viewsService.GetViewsCountByArticleIdAsync(articleId);
            return Ok(new { ArticleId = articleId, ViewsCount = viewsCount });
        }

        // POST api/<ViewsController>
        [HttpPost]
        public async Task<ActionResult<Views>> AddViews(Views views)
        {
            var createdViews = await _viewsService.AddViewsAsync(views);
            return CreatedAtAction(nameof(GetViewsById), new { id = createdViews.Id }, createdViews); // 201 Created HTTP response
        }

        // PUT api/<ViewsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ViewsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
