namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    public class CommentService : GenericService<Comment>, ICommentService
    {
        public CommentService(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
