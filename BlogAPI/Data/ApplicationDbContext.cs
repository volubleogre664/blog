namespace BlogAPI.Data
{
    using BlogAPI.Models;

    using Microsoft.EntityFrameworkCore;
 
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<Favorite> Favorites { get; set;}
    }
}
