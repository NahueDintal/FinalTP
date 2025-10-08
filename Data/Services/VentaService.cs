using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class VentaService
{
    private readonly AppDbContext _context;

    public VentaService()
    {
        _context = new AppDbContext();
    }

    public async Task<bool> RegistrarVentaAsync(Venta venta)
    {
        try
        {
            // Validar stock antes de registrar
            foreach (var detalle in venta.Detalles)
            {
                var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                if (producto == null || producto.Stock < detalle.Cantidad)
                {
                    Console.WriteLine($"❌ Stock insuficiente para {producto?.Nombre}");
                    return false;
                }
            }

            // Actualizar stock y guardar venta
            foreach (var detalle in venta.Detalles)
            {
                var producto = await _context.Productos.FindAsync(detalle.ProductoId);
                producto!.Stock -= detalle.Cantidad;
            }

            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();
            
            Console.WriteLine($"✅ Venta registrada - Total: ${venta.Total}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error al registrar venta: {ex.Message}");
            return false;
        }
    }

    public decimal CalcularTotalVenta(List<DetalleVenta> detalles)
    {
        return detalles.Sum(d => d.Subtotal);
    }
}
