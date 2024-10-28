using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
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
        [HttpGet("/api/Research/Published")]
        public async Task<ActionResult<IEnumerable<Research>>> GetCompletedResearches()
        {
            var result = await _researchService.GetCompletedResearchesAsync();
            return Ok(result);
        }


        // GET api/<ResearchController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Research>> GetResearchById(int id)
        {
            var article = await _researchService.GetResearchByIdAsync(id);
            if (article == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(article); // 200 OK HTTP response
        }

        // POST api/<ResearchController>
        [HttpPost]
        public async Task<ActionResult<Research>> AddResearch(Research research)
        {
            var createdResearch = await _researchService.AddResearchAsync(research);
            return CreatedAtAction(nameof(GetResearchById), new { id = createdResearch.Id }, createdResearch); // 201 Created HTTP response
        }

        // PUT api/<ResearchController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResearch(int id, [FromBody] Research research)
        {
            if (id != research.Id)
            {
                return BadRequest("Research ID mismatch");
            }

            try
            {
                var updatedResearch = await _researchService.UpdateResearchAsync(id, research);
                return Ok(updatedResearch);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE api/<ResearchController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
