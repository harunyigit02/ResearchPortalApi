using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProjectWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {

        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> GetAnswersAsync()
        {
            var answer = await _answerService.GetAnswersAsync();
            return Ok(answer);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> GetAnswerById(int id)
        {
            var answer = await _answerService.GetAnswerByIdAsync(id);
            if (answer == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(answer); // 200 OK HTTP response
        }

        // POST api/<CategoryController>
        // POST api/<ArticleController>
        [HttpPost]
        public async Task<ActionResult<Answer>> AddAnswer(Answer answer)
        {
            var createdAnswer = await _answerService.AddAnswerAsync(answer);
            return CreatedAtAction(nameof(GetAnswerById), new { id = createdAnswer.Id }, createdAnswer); // 201 Created HTTP response
        }
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("submitAnswers")]
        public async Task<IActionResult> SubmitAnswers([FromBody] List<Answer> answers)
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Forbid();
            }

            if (answers == null || answers.Count == 0)
            {
                return BadRequest("Geçersiz veri.");
            }
            

            try
            {
                await _answerService.AddAnswersAsync(answers,int.Parse(userId));
                return Ok(answers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Data record failed: {ex.Message}");
            }
        }

        [HttpGet("{researchId}/Answers")]
        public async Task<IActionResult> GetAnswersGroupByUsers(int researchId)
        {
            var answers = await _answerService.GetAnswersGroupByUsersAsync(researchId);
            if (answers == null || !answers.Any())  // Eğer cevap yoksa
            {
                return NotFound("No Answers Found");
            }
            return Ok(answers);  // Başarılı yanıt olarak döndür
        }

        [HttpPost("AnalyzeTargetQuestion")]
        public async Task<IActionResult> GetTargetQuestionAnalyze([FromBody] FilterAnalyzeQuestionsDto dto)
        {
            var result = await _answerService.GetTargetQuestionResultsAsync(dto.OptionIds, dto.QuestionId);
            if(result == null)
            {
                return NotFound("No Count Found");
            }
            return Ok(result);
        }
        [HttpPost("AnalyzeAllQuestions/{researchId}")]
        public async Task<IActionResult> GetAllQuestionsAnalyze([FromBody] List<int> optionIds,int researchId)
        {
            var result = await _answerService.GetAllQuestionResultsAsync(optionIds,researchId);
            if (result == null)
            {
                return NotFound("No Count Found");
            }
            return Ok(result);
        }



        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<Answer> UpdateAnswer(int id,Answer answer) 
        {
            
           throw new NotImplementedException();

        }

        // DELETE api/<CategoryController>/5
        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            // Servis katmanındaki DeleteArticleAsync metodunu çağırıyoruz
            var deletedAnswer = await _answerService.DeleteAnswerAsync(id);

            // Eğer silinecek bir makale bulunamadıysa 404 Not Found dönüyoruz
            if (deletedAnswer == null)
            {
                return NotFound(new { Message = $"Answer with ID {id} not found." });
            }

            // Silme işlemi başarılı ise 200 OK ve silinen makale bilgisi döndürüyoruz
            return Ok(new { Message = "Answer deleted successfully.", Option = deletedAnswer });
        }
    }
}
