namespace Core.Entities;
public class Venta
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public decimal Total { get; set; }
    public string? Cliente { get; set; }
    public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
}
