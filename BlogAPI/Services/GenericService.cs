namespace BlogAPI.Services
{
    using BlogAPI.Data;
    using BlogAPI.Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class GenericService<T> : IGenericService<T>
        where T : class
    {
        private readonly ApplicationDbContext context;

        public GenericService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
            this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
            this.context.SaveChanges();
        }

        public async Task<List<T>> GetAll()
        {
            return await this.context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await this.context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            this.context.Set<T>().Update(entity);
            this.context.SaveChanges();
        }
    }
}
