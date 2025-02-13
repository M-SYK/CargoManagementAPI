using CargoManagementAPI.Models;
using CargoManagementAPI.Repository;

namespace CargoManagementAPI.Service
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntity
    {
        private readonly IGenericRepository<T> _repository;

        // Constructor ile repository bağımlılığını enjekte eder
        public GenericService(IGenericRepository<T> repository)
        {
            _repository = repository;
        }

        // Tüm kayıtları getirir
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Belirtilen ID'ye sahip kaydı getirir
        public async Task<T> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Yeni kayıt ekler
        public async Task<string> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            return "Kayıt eklendi.";
        }

        // Mevcut bir kaydı günceller
        public async Task<string> UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            return "Kayıt güncellendi.";
        }

        // Belirtilen ID'ye sahip kaydı siler
        public async Task<string> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return $"{id} ID'li kayıt silindi.";
        }
    }
}
