namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;
    using BlogAPI.Models;

    public class UserService : GenericService<User>, IUserService
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext context) 
            : base(context)
        {
            this.context = context;
        }

        public User? GetByNameIdentifier(string nameIdentifier)
        {
            var user = this.context.Users.FirstOrDefault(x => x.IdentityAccountId == nameIdentifier);

            return user;
        }
    }
}
