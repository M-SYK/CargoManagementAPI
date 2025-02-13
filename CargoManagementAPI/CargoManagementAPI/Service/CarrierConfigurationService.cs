using CargoManagementAPI.Models;
using CargoManagementAPI.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoManagementAPI.Service
{
    public class CarrierConfigurationService : IGenericService<CarrierConfiguration>
    {
        private readonly IGenericRepository<CarrierConfiguration> _repository;

        // Constructor: Repository bağımlılığı enjekte edilir
        public CarrierConfigurationService(IGenericRepository<CarrierConfiguration> repository)
        {
            _repository = repository;
        }

        // Tüm CarrierConfiguration kayıtlarını getirir
        public async Task<IEnumerable<CarrierConfiguration>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Belirtilen ID'ye sahip CarrierConfiguration kaydını getirir
        public async Task<CarrierConfiguration> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Yeni bir CarrierConfiguration kaydı ekler
        public async Task<string> AddAsync(CarrierConfiguration entity)
        {
            await _repository.AddAsync(entity);
            return "Kayıt eklendi.";
        }

        // Mevcut bir CarrierConfiguration kaydını günceller
        public async Task<string> UpdateAsync(CarrierConfiguration entity)
        {
            await _repository.UpdateAsync(entity);
            return "Kayıt güncellendi.";
        }

        // Belirtilen ID'ye sahip CarrierConfiguration kaydını siler
        public async Task<string> DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
            return $"{id} ID'li kayıt silindi.";
        }
    }
}
