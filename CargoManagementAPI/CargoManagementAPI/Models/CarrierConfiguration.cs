namespace CargoManagementAPI.Models
{
    public class CarrierConfiguration : BaseEntity
    {
        public int CarrierId { get; set; }
        public int CarrierMaxDesi { get; set; }
        public int CarrierMinDesi { get; set; }
        public decimal CarrierCost { get; set; }
        public Carrier? Carrier { get; set; }
    }
}
