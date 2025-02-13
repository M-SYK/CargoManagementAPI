using CargoManagementAPI.Models;
using CargoManagementAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CargoManagementAPI.Controllers
{
    [Route("api/[controller]")] // API için varsayılan route belirlenir
    [ApiController] 
    public class CarrierConfigurationsController : ControllerBase
    {
        private readonly IGenericService<CarrierConfiguration> _carrierConfigurationService;

        // Constructor: Bağımlılık enjeksiyonu ile servis sınıfı alınır
        public CarrierConfigurationsController(IGenericService<CarrierConfiguration> carrierConfigurationService)
        {
            _carrierConfigurationService = carrierConfigurationService;
        }

        // Tüm taşıyıcı yapılandırmalarını getirir
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var configurations = await _carrierConfigurationService.GetAllAsync();
            return Ok(configurations);
        }

        // Belirtilen ID'ye sahip taşıyıcı yapılandırmasını getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var configuration = await _carrierConfigurationService.GetByIdAsync(id);
            if (configuration == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            return Ok(configuration);
        }

        // Yeni bir taşıyıcı yapılandırması ekler
        [HttpPost]
        public async Task<IActionResult> Add(CarrierConfiguration configuration)
        {
            var result = await _carrierConfigurationService.AddAsync(configuration);
            return CreatedAtAction(nameof(GetById), new { id = configuration.Id }, new { message = result });
        }

        // Belirtilen ID'ye sahip taşıyıcı yapılandırmasını günceller
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CarrierConfiguration configuration)
        {
            if (id != configuration.Id)
            {
                return BadRequest(); // ID uyuşmazlığı durumunda 400 döndür
            }
            var result = await _carrierConfigurationService.UpdateAsync(configuration);
            return Ok(new { message = result });
        }

        // Belirtilen ID'ye sahip taşıyıcı yapılandırmasını siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var configuration = await _carrierConfigurationService.GetByIdAsync(id);
            if (configuration == null)
            {
                return NotFound(); // Veri bulunamazsa 404 döndür
            }
            var result = await _carrierConfigurationService.DeleteAsync(id);
            return Ok(new { message = result });
        }
    }
}
