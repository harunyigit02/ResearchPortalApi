using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
