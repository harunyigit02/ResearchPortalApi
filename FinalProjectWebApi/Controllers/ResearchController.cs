using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
using FinalProjectWebApi.Entities.Abstract;
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
    public class ResearchController : ControllerBase
    {

        private readonly IResearchService _researchService;

        public ResearchController(IResearchService researchService)
        {
            _researchService = researchService;

        }
        // GET: api/<ResearchController>
        [HttpGet]
        public async Task<ActionResult<PagingResult<Research>>> GetResearches(int pageNumber=1,int pageSize=3)
        {
            var result = await _researchService.GetPagedResearchesAsync(pageNumber,pageSize);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin,Researcher")]
        [HttpGet("UserResearches")]
        public async Task<IActionResult> GetUserResearches(
            [FromQuery] string? keyword,
            [FromQuery] int? categoryId,
            [FromQuery] DateTime? minDate,
            [FromQuery] DateTime? maxDate,

            int pageNumber = 1,
            int pageSize = 10
           )
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            if (role != "Admin"&& role != "Researcher")
            {
                return Forbid();
            }

            var researches = await _researchService.GetPagedResearhesByUserIdAsync(int.Parse(userId),pageNumber,pageSize,keyword,categoryId, minDate, maxDate);
            return Ok(researches);
        }

        [HttpGet("/api/Research/Published")]
        public async Task<ActionResult<PagingResult<Research>>> GetCompletedResearches(
                       
            
            [FromQuery] int? categoryId,
            [FromQuery] string? keyword,
            [FromQuery] DateTime? minDate,
            [FromQuery] DateTime? maxDate,

            int pageNumber = 1,
            int pageSize = 10
             

            )
        {
            var result = await _researchService.GetCompletedResearchesAsync(pageNumber,pageSize,categoryId,keyword,minDate,maxDate);
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Researcher")]
        [HttpPost]
        public async Task<ActionResult<Research>> AddResearch(Research research)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // JWT token'dan userId alıyoruz
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userId == null)
            {
                return Unauthorized("Geçersiz kullanıcı");
            }
            if (role != "Admin" && role != "Researcher")
            {
                return Forbid();
            }

            // Makale bilgilerine userId ekliyoruz
            research.PublishedBy = int.Parse(userId);  // veya uygun dönüşümü yapın

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
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _researchService.DeleteResearchAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
