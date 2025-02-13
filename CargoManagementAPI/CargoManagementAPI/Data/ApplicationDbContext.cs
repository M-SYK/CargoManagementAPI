namespace CargoManagementAPI.Data
{
    using CargoManagementAPI.Models;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContext : DbContext
    {
        // Constructor: DbContextOptions nesnesini alarak veritabanı bağlantısını yapılandırır.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Veritabanı tablolarını temsil eden DbSet'ler
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CarrierConfiguration> CarrierConfigurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Carrier ve CarrierConfiguration arasındaki birebir ilişkiyi tanımlar.
            modelBuilder.Entity<CarrierConfiguration>()
                .HasOne(c => c.Carrier)
                .WithOne(c => c.CarrierConfiguration)
                .HasForeignKey<CarrierConfiguration>(c => c.CarrierId);

            // BaseEntity sınıfını miras alan tüm varlıklara CreatedDate ve UpdatedDate için varsayılan değerler atar.
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    // CreatedDate alanına varsayılan olarak GETDATE() SQL fonksiyonunu atar.
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("CreatedDate")
                        .HasDefaultValueSql("GETDATE()");

                    // UpdatedDate alanına varsayılan olarak GETDATE() SQL fonksiyonunu atar.
                    modelBuilder.Entity(entityType.ClrType)
                        .Property("UpdatedDate")
                        .HasDefaultValueSql("GETDATE()");
                }
            }
        }
    }
}
