namespace BlogAPI.Controllers
{
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IUserService userService;

        public ArticleController(IArticleService articleService, IUserService userService)
        {
            this.articleService = articleService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var articles = this.articleService.GetFeedArticles();

            return this.Ok(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await this.articleService.GetArticle(id);

            if (article == null)
            {
                return this.BadRequest(new { message = "Article does not exist or has been deleted by owner" });
            }

            return this.Ok(article);
        }

        [Authorize]
        [HttpGet("/api/[controller]/[action]/{id}")]
        public async Task<IActionResult> GetUserArticles(int id)
        {
            var articles = await this.articleService.GetFeedArticles(id);

            return this.Ok(articles);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(Article article)
        {
           this.articleService.Add(article);

            var author = await this.userService.GetById(article.AuthorId);

            return this.Ok(new
            {
                article.AuthorId,
                article.ArticleId,
                article.ArticlePublishDate,
                article.ArticleTitle,
                article.ArticleCoverImage,
                Author = new
                {
                    author.UserId,
                    author.Firstname,
                    author.Lastname,
                    author.Bio,
                    author.UserPicture
                },
            });
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var article = await this.articleService.GetById(id);

            if (article == null)
            {
                return this.BadRequest(new { message = "Article not found" });
            }

            await this.articleService.Delete(article);

            return this.Ok();
        }
    }
}
