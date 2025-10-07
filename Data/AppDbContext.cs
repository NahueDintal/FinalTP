using Microsoft.EntityFrameworkCore;
using Core.Entities;

namespace Data;
public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<DetalleVenta> DetallesVenta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost;Database=CasaComidaCasera;Trusted_Connection=true;TrustServerCertificate=true;");
    }
}
