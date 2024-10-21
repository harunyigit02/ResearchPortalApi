using FinalProjectWebApi.Business.Abstract;
using FinalProjectWebApi.Business.Concrete;
using FinalProjectWebApi.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinalProjectWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService) 
        {
            _articleService = articleService;

        }
        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesAsync()
        {
            var result = await _articleService.GetArticlesAsync();
            return Ok(result);
        }

        // GET api/<ArticleController>/5
        // GET: api/Article/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);
            if (article == null)
            {
                return NotFound(); // 404 Not Found HTTP response
            }
            return Ok(article); // 200 OK HTTP response
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task<ActionResult<Article>> AddArticle(Article article)
        {
            var createdArticle = await _articleService.AddArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle); // 201 Created HTTP response
        }


        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            // Servis katmanındaki DeleteArticleAsync metodunu çağırıyoruz
            var deletedArticle = await _articleService.DeleteArticleAsync(id);

            // Eğer silinecek bir makale bulunamadıysa 404 Not Found dönüyoruz
            if (deletedArticle == null)
            {
                return NotFound(new { Message = $"Article with ID {id} not found." });
            }

            // Silme işlemi başarılı ise 200 OK ve silinen makale bilgisi döndürüyoruz
            return Ok(new { Message = "Article deleted successfully.", Article = deletedArticle });
        }
    }
}
