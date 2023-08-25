namespace BlogAPI.Controllers
{
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await this.userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return this.Ok(user);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            var nameIdentifier = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = this.userService.GetByNameIdentifier(nameIdentifier);

            return this.Ok(user);
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            
            this.userService.Add(user);

            return this.Ok(user);
        }

        [HttpPut]
        public IActionResult Put(User user)
        {
            this.userService.Update(user);

            return this.Ok(user);
        }
    }
}
