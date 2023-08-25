namespace BlogAPI.Controllers
{
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Comment comment)
        {
            this.commentService.Add(comment);
            var author = await this.userService.GetById(comment.CommentAuthorId);

            return this.Ok(new {
                comment.ArticleId,
                comment.CommentId,
                comment.CommentBody,
                comment.CommentDate,
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
    }
}
