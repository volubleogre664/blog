
namespace BlogAPI.Controllers
{
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService bookmarkService;
        private readonly IUserService userService;

        public BookmarkController(IBookmarkService bookmarkService, IUserService userService)
        {
            this.bookmarkService = bookmarkService;
            this.userService = userService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var bookmarks = await this.bookmarkService.GetUserBookmarks(userId);

            var bookmarksWithAuthors = new List<object>();

            foreach (var bookmark in bookmarks)
            {
                var user = await this.userService.GetById(bookmark.AuthorId);

                bookmarksWithAuthors.Add(new
                {
                    bookmark.AuthorId,
                    bookmark.ArticleId,
                    bookmark.ArticlePublishDate,
                    bookmark.ArticleTitle,
                    bookmark.ArticleCoverImage,
                    Author = new
                    {
                        user.UserId,
                        user.Firstname,
                        user.Lastname,
                        user.UserPicture,
                    }
                });
            }

            return this.Ok(bookmarksWithAuthors);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Bookmark bookmark)
        {
            var existingBookmark = await this.bookmarkService.GetById(bookmark.BookmarkId);

            if (existingBookmark != null)
            {
                this.bookmarkService.Delete(existingBookmark);
                return this.Ok(existingBookmark);
            }

            this.bookmarkService.Add(bookmark);
            return this.Ok(bookmark);
        }
    }
}
