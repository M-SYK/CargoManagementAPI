namespace CargoManagementAPI.Models
{
    public class Carrier : BaseEntity
    {
        public string? CarrierName { get; set; }
        public bool CarrierIsActive { get; set; }
        public int CarrierPlusDesiCost { get; set; }
        public int CarrierConfigurationId { get; set; }
        public CarrierConfiguration? CarrierConfiguration { get; set; }
    }
}
