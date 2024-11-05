using gestionDeHotel.Models;
using Microsoft.EntityFrameworkCore;

namespace gestionDeHotel.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets para las entidades
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Habitacion> Habitaciones { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reserva>()
                .Property(r => r.RowVersion)
                .IsRowVersion(); // Configuración para la concurrencia optimista
        }
    }
}