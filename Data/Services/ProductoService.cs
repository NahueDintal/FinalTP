using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Services;

public class ProductoService
{
    private readonly AppDbContext _context;

    public ProductoService()
    {
        _context = new AppDbContext();
    }

    public async Task<List<Producto>> ObtenerTodosAsync()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto?> ObtenerPorIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task<bool> ActualizarStockAsync(int productoId, int nuevoStock)
    {
        try
        {
            var producto = await _context.Productos.FindAsync(productoId);
            if (producto != null)
            {
                producto.Stock = nuevoStock;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error actualizando stock: {ex.Message}");
            return false;
        }
    }
}
