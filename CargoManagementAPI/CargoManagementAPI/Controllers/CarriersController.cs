namespace CargoManagementAPI.Controllers
{
    using CargoManagementAPI.Models;
    using CargoManagementAPI.Service;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")] 
    [ApiController] 
    public class CarriersController : ControllerBase
    {
        private readonly IGenericService<Carrier> _carrierService;

        // Constructor: Bağımlılık enjeksiyonu ile servis sınıfı alınır
        public CarriersController(IGenericService<Carrier> carrierService)
        {
            _carrierService = carrierService;
        }

        // Tüm taşıyıcıları getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var carriers = await _carrierService.GetAllAsync();
            return Ok(carriers);
        }

        // Belirtilen ID'ye sahip taşıyıcıyı getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var carrier = await _carrierService.GetByIdAsync(id);
            if (carrier == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            return Ok(carrier);
        }

        // Yeni bir taşıyıcı ekler
        [HttpPost]
        public async Task<IActionResult> Add(Carrier carrier)
        {
            var result = await _carrierService.AddAsync(carrier);
            return CreatedAtAction(nameof(GetById), new { id = carrier.Id }, new { message = result });
        }

        // Belirtilen ID'ye sahip taşıyıcıyı günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Carrier carrier)
        {
            if (id != carrier.Id)
            {
                return BadRequest(); // ID uyuşmazlığı durumunda 400 döndür
            }
            var result = await _carrierService.UpdateAsync(carrier);
            return Ok(new { message = result });
        }

        // Belirtilen ID'ye sahip taşıyıcıyı siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var carrier = await _carrierService.GetByIdAsync(id);
            if (carrier == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            var result = await _carrierService.DeleteAsync(id);
            return Ok(new { message = result });
        }
    }
}
