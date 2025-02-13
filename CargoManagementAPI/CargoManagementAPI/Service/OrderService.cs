using CargoManagementAPI.Data;
using CargoManagementAPI.Models;
using CargoManagementAPI.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CargoManagementAPI.Service
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<CarrierConfiguration> _carrierConfigurationRepository;
        private readonly ApplicationDbContext _context;

        // Constructor ile bağımlılıkları enjekte etme
        public OrderService(IGenericRepository<Order> orderRepository,
                           IGenericRepository<CarrierConfiguration> carrierConfigurationRepository,
                           ApplicationDbContext context)
        {
            _orderRepository = orderRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _context = context;
        }

        // Tüm siparişleri getirir
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        // Yeni sipariş ekler, uygun taşıyıcıyı belirleyerek taşıma maliyetini hesaplar
        public async Task<string> AddOrderAsync(Order order)
        {
            var configurations = await _carrierConfigurationRepository.GetAllAsync();

            // Carrier nesnelerini konfigürasyonlara ekleyerek günceller
            configurations = configurations.Select(c =>
            {
                c.Carrier = _context.Carriers.Find(c.CarrierId);
                return c;
            }).ToList();

            // Siparişin desi değerine uygun taşıyıcıyı bulur
            var suitableCarrier = configurations
                .Where(c => order.OrderDesi >= c.CarrierMinDesi && order.OrderDesi <= c.CarrierMaxDesi)
                .OrderBy(c => c.CarrierCost)
                .FirstOrDefault();

            if (suitableCarrier != null && suitableCarrier.Carrier != null)
            {
                // Taşıyıcı maliyetini hesaplar
                order.OrderCarrierCost = suitableCarrier.CarrierCost +
                                         (order.OrderDesi - suitableCarrier.CarrierMinDesi) *
                                         suitableCarrier.Carrier.CarrierPlusDesiCost;
                order.CarrierId = suitableCarrier.CarrierId;
            }
            else
            {
                // Eğer uygun taşıyıcı yoksa en yakın taşıyıcıyı belirler
                var closestCarrier = configurations
                    .OrderBy(c => Math.Abs(order.OrderDesi - c.CarrierMinDesi))
                    .FirstOrDefault();

                if (closestCarrier != null && closestCarrier.Carrier != null)
                {
                    var additionalDesi = order.OrderDesi - closestCarrier.CarrierMinDesi;
                    order.OrderCarrierCost = closestCarrier.CarrierCost +
                                             (additionalDesi * closestCarrier.Carrier.CarrierPlusDesiCost);
                    order.CarrierId = closestCarrier.CarrierId;
                }
                else
                {
                    throw new Exception("Carrier bilgisi eksik! Lütfen ilgili CarrierConfiguration kaydını kontrol edin.");
                }
            }

            // Siparişi kaydeder
            await _orderRepository.AddAsync(order);
            return "Sipariş eklendi.";
        }

        // Mevcut bir siparişi günceller
        public async Task<string> UpdateAsync(Order order)
        {
            var existingOrder = await _orderRepository.GetByIdAsync(order.OrderId);

            if (existingOrder == null)
            {
                throw new Exception("Güncellenecek sipariş bulunamadı.");
            }

            // Güncellenen değerleri mevcut siparişe aktarır
            existingOrder.CarrierId = order.CarrierId;
            existingOrder.OrderDesi = order.OrderDesi;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.OrderCarrierCost = order.OrderCarrierCost;

            await _orderRepository.UpdateAsync(existingOrder);
            return "Sipariş güncellendi.";
        }

        // Belirtilen ID'ye sahip siparişi siler
        public async Task<string> DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return $"{id} ID'li sipariş silindi.";
        }
    }
}
