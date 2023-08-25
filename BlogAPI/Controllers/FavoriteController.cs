namespace BlogAPI.Controllers
{
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            this.favoriteService = favoriteService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Favorite favorite)
        {
            var existingFavorite = await this.favoriteService.GetById(favorite.FavoriteId);

            if (existingFavorite != null)
            {
                this.favoriteService.Delete(existingFavorite);
                return this.Ok(existingFavorite);
            }

            this.favoriteService.Add(favorite);

            return this.Ok(favorite);
        }
    }
}
