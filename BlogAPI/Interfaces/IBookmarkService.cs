namespace BlogAPI.Interfaces
{
    using BlogAPI.Models;
    
    public interface IBookmarkService : IGenericService<Bookmark>
    {
        Task<List<Article>> GetUserBookmarks(int userId);
    }
}
