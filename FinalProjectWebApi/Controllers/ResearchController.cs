using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchController : ControllerBase
    {

        private readonly IResearchService _researchService;

        public ResearchController(IResearchService researchService)
        {
            _researchService = researchService;

        }
        // GET: api/<ResearchController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Research>>> GetResearches()
        {
            var result = await _researchService.GetResearchesAsync();
            return Ok(result);
        }


        // GET api/<ResearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResearchController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
