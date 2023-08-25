namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.EntityFrameworkCore;

    public class ArticleService : GenericService<Article>, IArticleService
    {
        private readonly ApplicationDbContext context;

        public ArticleService(ApplicationDbContext context) 
            : base(context)
        {
            this.context = context;
        }

        public new async Task Delete(Article article)
        {
            await this.DeleteFavoritesBookmarksAndComments(article.ArticleId);

            base.Delete(article);
        }

        public async Task<object?> GetArticle(int articleId)
        {
            var article = await base.GetById(articleId);

            if (article == null)
            {
                return null;
            }

            return new
            {
                article.AuthorId,
                article.ArticleId,
                article.ArticleTitle,
                article.ArticleBody,
                article.ArticlePublishDate,
                article.ArticleCoverImage,
                Likes = await this.context.Favorites.Where(_ => _.ArticleId == articleId).ToListAsync(),
                Comments = await this.GetComments(articleId),
                Bookmarks = await this.context.Bookmarks.Where(_ => _.ArticleId == articleId).ToListAsync(),
            };
        }

        public List<object> GetFeedArticles()
        {
            var articles = this.context.Articles
                .Join(
                    this.context.Users,
                    article => article.AuthorId,
                    user => user.UserId,
                    (article, user) => new { article, user }
                )
                .OrderByDescending(_ => _.article.ArticlePublishDate)
                .Select(_ => new
                {
                    _.article.ArticleId,
                    _.article.ArticleTitle,
                    _.article.ArticlePublishDate,
                    _.article.ArticleCoverImage,
                    Likes = this.context.Favorites.Count(f => f.ArticleId == _.article.ArticleId),
                    Comments = this.context.Comments.Count(c => c.ArticleId == _.article.ArticleId),
                    Author = new
                    {
                        _.user.UserId,
                        _.user.Firstname,
                        _.user.Lastname,
                        _.user.Bio,
                        _.user.UserPicture
                    }
                });

            return articles.ToList<object>();
        }

        public async Task<List<Article>> GetFeedArticles(int userId)
        {
            var articles = await this.context.Articles.Where(_ => _.AuthorId == userId).ToListAsync();

            return articles;
        }

        private async Task DeleteFavoritesBookmarksAndComments(int articleId)
        {
            var comments = await this.context.Comments.Where(_ => _.ArticleId == articleId).ToListAsync();
            var favorites = await this.context.Favorites.Where(_ => _.ArticleId == articleId).ToListAsync();
            var bookmarks = await this.context.Bookmarks.Where(_ => _.ArticleId == articleId).ToListAsync();

            this.context.Comments.RemoveRange(comments);
            this.context.Favorites.RemoveRange(favorites);
            this.context.Bookmarks.RemoveRange(bookmarks);

            this.context.SaveChanges();
        }

        private async Task<List<object>> GetComments(int articleId)
        {
            var comments = await this.context.Comments
                .Where(_ => _.ArticleId == articleId)
                .Select(_ => new
                {
                    _.ArticleId,
                    _.CommentId,
                    _.CommentBody,
                    _.CommentDate,
                    Author = this.context.Users.FirstOrDefault(user => user.UserId == _.CommentAuthorId)
                }).ToListAsync<object>();

            return comments;
        }
    }
}
