using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchRequirementController : ControllerBase
    {

        private readonly IResearchRequirementService _researchRequirementService;

        public ResearchRequirementController(IResearchRequirementService researchRequirementService)
        {
            _researchRequirementService = researchRequirementService;

        }
        // GET: api/<ResearchRequirementController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResearchRequirement>>> GetResearchRequirements()
        {
            var result = await _researchRequirementService.GetResearchRequirementsAsync();
            return Ok(result);
        }

        // GET api/<ResearchRequirementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ResearchRequirementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ResearchRequirementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ResearchRequirementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
