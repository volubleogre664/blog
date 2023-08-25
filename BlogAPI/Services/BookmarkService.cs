namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;
    using BlogAPI.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class BookmarkService : GenericService<Bookmark>, IBookmarkService
    {
        private readonly ApplicationDbContext context;

        public BookmarkService(ApplicationDbContext context) 
            : base(context)
        {
            this.context = context;
        }

        public async Task<List<Article>> GetUserBookmarks(int userId)
        {
            var bookmarks = await this.context.Bookmarks.Where(_ => _.BookmarkOwnerId == userId).Select(_ => _.ArticleId).ToListAsync();
            var articles = await this.context.Articles.Where(_ => bookmarks.Contains(_.ArticleId)).ToListAsync();


            return articles;
        }
    }
}
