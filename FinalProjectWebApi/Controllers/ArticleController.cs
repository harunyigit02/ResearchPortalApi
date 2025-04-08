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
    public class ArticleController : ControllerBase
    {

        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;


        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger) 
        {
            _articleService = articleService;
            _logger = logger;
        }
        // GET: api/<ArticleController>
        [HttpGet]
        public async Task<ActionResult<PagingResult<ArticleDto>>> GetArticlesPagedAsync(
            int? categoryId,
            string? keyword,
            DateTime? minDate,
            DateTime? maxDate,
            int pageNumber = 1,
            int pageSize = 10
            )
        {
            // Sayfalama işlemi için gerekli parametreleri alıyoruz
            var result = await _articleService.GetArticlesPagedAsync(pageNumber, pageSize,categoryId,keyword,minDate,maxDate);

            // PagedResult döndürüyoruz
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles ="Admin,Researcher")]
        [HttpGet("UserArticles")]
        public async Task<ActionResult<PagingResult<ArticleDto>>> GetUserArticles(int? categoryId,string? keyword,DateTime? minDate,DateTime? maxDate,int pageNumber = 1, int pageSize = 10)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }
            if(role != "Admin" &&  role != "Researcher")
            {
                return Forbid();
            }

            var articles = await _articleService.GetArticlesByUserIdAsync(int.Parse(userId),pageNumber,pageSize,categoryId,keyword,minDate,maxDate);
            return Ok(articles);
        }

        // POST api/<ArticleController>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,Researcher")]  // Sadece doğrulanmış kullanıcıların erişebilmesi için
        [HttpPost]
        public async Task<ActionResult<Article>> AddArticle( [FromForm] IFormFile file, [FromForm] Article article)
        {
            // Kullanıcıyı kimlik doğrulama token'ından alıyoruz
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // JWT token'dan userId alıyoruz
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (file == null || file.Length == 0)
                return BadRequest("PDF dosyası eksik.");

            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);

            article.Content = memoryStream.ToArray();
            if (article.Content == null)
            {
                return BadRequest("Pdf dosyasi yüklenemedi");
            }
                


            if (userId == null)
            {
                return Unauthorized("Geçersiz kullanıcı");
            }
            if (role == "Admin" && role == "Researcher")
            {
                return Forbid();
            }

            // Makale bilgilerine userId ekliyoruz
            article.PublishedBy = int.Parse(userId);  // veya uygun dönüşümü yapın

            var createdArticle = await _articleService.AddArticleAsync(article);
            return CreatedAtAction(nameof(GetArticle), new { id = createdArticle.Id }, createdArticle);
        }


        [HttpGet("{id}/Download")]
        public async Task<IActionResult> DownloadArticle(int id)
        {
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
                return NotFound("Makale bulunamadı.");

            if (article.Content == null || article.Content.Length == 0)
                return NotFound("Makalenin içeriği boş.");

            // Dosya adını makale başlığından oluştur (boşsa fallback)
            var fileName = $"{article.Title?.Replace(" ", "_") ?? "Makale"}_{id}.pdf";

            return File(article.Content, "application/pdf", fileName);
        }


        // PUT api/<ArticleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Article updatedArticle)
        {
            if (updatedArticle == null || id <= 0)
            {
                return BadRequest("Geçersiz veri.");
            }

            var result = await _articleService.UpdateArticleAsync(id, updatedArticle);

            if (!result)
            {
                return NotFound("Makale bulunamadı.");
            }

            return Ok("Makale başarıyla güncellendi.");
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
        [HttpDelete("MultiDelete")]
        public async Task<IActionResult> DeleteArticles([FromBody] List<int> articleIds)
        {
            if (articleIds == null || !articleIds.Any())
            {
                return BadRequest("Silinecek makale ID'leri geçerli değil.");
            }

            var result = await _articleService.DeleteArticlesAsync(articleIds);

            if (result)
            {
                return Ok("Makaleler başarıyla silindi.");
            }
            else
            {
                return StatusCode(500, "Makaleler silinirken bir hata oluştu.");
            }
        }




    }
}
