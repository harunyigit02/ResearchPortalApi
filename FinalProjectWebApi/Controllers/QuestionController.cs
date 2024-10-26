using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;

        }
        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestionsAsync()
        {
            var question = await _questionService.GetQuestionsAsync();
            return Ok(question);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(question); // 200 OK HTTP response
        }

        // POST api/<CategoryController>
        // POST api/<ArticleController>
        [HttpPost]
        public async Task<ActionResult<Question>> AddQuestion(Question question)
        {
            var createdQuestion = await _questionService.AddQuestionAsync(question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = createdQuestion.Id }, createdQuestion); // 201 Created HTTP response
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            // Servis katmanındaki DeleteArticleAsync metodunu çağırıyoruz
            var deletedQuestion = await _questionService.DeleteQuestionAsync(id);

            // Eğer silinecek bir makale bulunamadıysa 404 Not Found dönüyoruz
            if (deletedQuestion == null)
            {
                return NotFound(new { Message = $"Question with ID {id} not found." });
            }

            // Silme işlemi başarılı ise 200 OK ve silinen makale bilgisi döndürüyoruz
            return Ok(new { Message = "Question deleted successfully.", Question = deletedQuestion });
        }
    }
}
