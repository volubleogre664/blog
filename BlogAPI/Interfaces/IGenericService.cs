namespace BlogAPI.Interfaces
{
    public interface IGenericService<T>
        where T : class
    {
        void Update(T entity);

        void Delete(T entity);

        Task<T?> GetById(int id);

        Task<List<T>> GetAll();

        void Add(T entity);
    }
}
