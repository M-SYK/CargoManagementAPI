namespace CargoManagementAPI.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IGenericService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<string> AddAsync(T entity);
        Task<string> UpdateAsync(T entity);
        Task<string> DeleteAsync(int id);
    }
}
