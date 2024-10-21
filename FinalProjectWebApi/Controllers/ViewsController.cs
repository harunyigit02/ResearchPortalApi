using FinalProjectWebApi.Business.Abstract;
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
        public async Task<ActionResult<IEnumerable<Research>>> GetResearches()
        {
            var result = await _viewsService.GetViewsAsync();
            return Ok(result);
        }

        // GET api/<ViewsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ViewsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
