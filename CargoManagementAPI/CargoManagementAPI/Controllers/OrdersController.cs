using CargoManagementAPI.Models;
using CargoManagementAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CargoManagementAPI.Controllers
{
    [Route("api/[controller]")] // API için varsayılan route belirlenir
    [ApiController] 
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        // Constructor: Bağımlılık enjeksiyonu ile servis sınıfı alınır
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Tüm siparişleri getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        // Belirtilen ID'ye sahip siparişi getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            return Ok(order);
        }

        // Yeni bir sipariş ekler
        [HttpPost]
        public async Task<IActionResult> Add(Order order)
        {
            var result = await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, new { message = result });
        }

        // Belirtilen ID'ye sahip siparişi günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest(); // ID uyuşmazlığı durumunda 400 döndür
            }
            var result = await _orderService.UpdateAsync(order);
            return Ok(new { message = result });
        }

        // Belirtilen ID'ye sahip siparişi siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            var result = await _orderService.DeleteAsync(id);
            return Ok(new { message = result });
        }
    }
}
