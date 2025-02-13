using CargoManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CargoManagementAPI.Service
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task<string> AddOrderAsync(Order order);
        Task<string> UpdateAsync(Order order);
        Task<string> DeleteAsync(int id);
    }
}
