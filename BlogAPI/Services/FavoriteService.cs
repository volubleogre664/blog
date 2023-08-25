namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    public class FavoriteService : GenericService<Favorite>, IFavoriteService
    {
        public FavoriteService(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
