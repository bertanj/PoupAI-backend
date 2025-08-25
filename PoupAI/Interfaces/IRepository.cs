namespace PoupAI.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        Task<IEnumerable<T>> GetByDateAsync(DateTime date);
        Task AddValue(T entity);
        Task UpdateValue(T entity);
        Task DeleteValue(int id);
    }
}
