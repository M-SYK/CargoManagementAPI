namespace CargoManagementAPI.Models
{
    public class Order : BaseEntity
    {
        public int CarrierId { get; set; }
        public int OrderDesi { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderCarrierCost { get; set; }
    }
}
