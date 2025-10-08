using Core.Entities;

namespace Data.Services;

public class VentaService
{
    public decimal CalcularTotalVenta(List<DetalleVenta> detalles)
    {
        return detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
    }

    // Agregar este método
    public bool ValidarStock(Producto producto, int cantidadRequerida)
    {
        return producto.Stock >= cantidadRequerida;
    }
}
