namespace BlogAPI.Interfaces
{
    using BlogAPI.Models;

    public interface IUserService : IGenericService<User>
    {
        User? GetByNameIdentifier(string email);
    }
}
