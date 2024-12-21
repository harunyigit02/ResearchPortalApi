using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchRequirementController : ControllerBase
    {

        private readonly IResearchRequirementService _researchRequirementService;
        private readonly IParticipantInfoService _participantInfoService;

        public ResearchRequirementController(IResearchRequirementService researchRequirementService,IParticipantInfoService participantInfoService)
        {
            _researchRequirementService = researchRequirementService;
            _participantInfoService = participantInfoService;

        }
        // GET: api/<ResearchRequirementController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResearchRequirement>>> GetResearchRequirements()
        {
            var result = await _researchRequirementService.GetResearchRequirementsAsync();
            return Ok(result);
        }

        [HttpGet("ResearchsConditions/{researchId}")]
        public async Task<ActionResult<ResearchRequirement>> GetResearchRequirementsByResearchId(int researchId)
        {
            var result = await _researchRequirementService.GetResearchRequirementByResearchIdAsync(researchId);
            return Ok(result);
        }

        // GET api/<ResearchRequirementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<ResearchRequirementController>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin,Researcher")]
        [HttpPost]
        public async Task<ActionResult<ResearchRequirement>> AddResearchRequirement([FromBody] ResearchRequirement researchRequirement)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId == null) 
            {
                return Unauthorized();
            }
            if(role != "Admin" && role != "Researcher")
            {
                return Forbid();
            }

            return  await _researchRequirementService.AddResearchRequirementAsync(researchRequirement);
        }

        


        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("MatchedResearchRequirements")]
        public async Task<IActionResult> GetMatchedResearchRequirements(string? keyword,
    int? categoryId,DateTime? minDate,
    DateTime? maxDate
    , int pageNumber=1,
    int pageSize=5
    )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var participantInfo= await _participantInfoService.GetParticipantInfosByUserIdAsync(int.Parse(userId));
            var result = await _researchRequirementService.GetMatchedResearchRequirementsAsync(participantInfo, pageNumber, pageSize, keyword, categoryId,minDate,maxDate);
            return Ok(result);
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
