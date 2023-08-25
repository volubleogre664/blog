namespace BlogAPI.Interfaces
{
    using BlogAPI.Models;

    public interface IArticleService : IGenericService<Article>
    {
        List<object> GetFeedArticles();

        Task<object?> GetArticle(int articleId);

        Task<List<Article>> GetFeedArticles(int userId);

        new Task Delete(Article entity);
    }
}
