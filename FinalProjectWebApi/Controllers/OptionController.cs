using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {

        private readonly IOptionService _optionService;
        private readonly IQuestionService _questionService;
        

        public OptionController(IOptionService optionService, IQuestionService questionService)
        {
            _optionService = optionService;
            _questionService = questionService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Option>>> GetOptionsAsync()
        {
            var option = await _optionService.GetOptionsAsync();
            return Ok(option);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Option>> GetOptionById(int id)
        {
            var option = await _optionService.GetOptionByIdAsync(id);
            if (option == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(option); // 200 OK HTTP response
        }

        // POST api/<CategoryController>
        // POST api/<ArticleController>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Researcher")]
        [HttpPost]
        public async Task<ActionResult<Option>> AddOption(Option option)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            var question= await _questionService.GetQuestionByIdAsync(option.QuestionId);
            var isAuthorized= await _questionService.IsUserAuthorizedForResearchAsync(int.Parse(userId),question);
            if (!isAuthorized) 
            {
                return Forbid();
            }

            var createdOption = await _optionService.AddOptionAsync(option);
            return CreatedAtAction(nameof(GetOptionById), new { id = createdOption.Id }, createdOption); // 201 Created HTTP response
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            // Servis katmanındaki DeleteArticleAsync metodunu çağırıyoruz
            var deletedOption = await _optionService.DeleteOptionAsync(id);

            // Eğer silinecek bir makale bulunamadıysa 404 Not Found dönüyoruz
            if (deletedOption == null)
            {
                return NotFound(new { Message = $"Option with ID {id} not found." });
            }

            // Silme işlemi başarılı ise 200 OK ve silinen makale bilgisi döndürüyoruz
            return Ok(new { Message = "Option deleted successfully.", Option = deletedOption });
        }
    }
}
