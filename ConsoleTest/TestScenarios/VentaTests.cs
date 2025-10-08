using Core.Entities;
using Data.Services;

namespace ConsoleTest.TestScenarios;

public static class VentaTests
{
    public static async Task Ejecutar()
    {
        Console.WriteLine("\n💰 PRUEBAS DE VENTAS");
        Console.WriteLine("====================");
        
        await ProbarCalculoTotales();
        await ProbarProcesoVentaCompleto();
        await ProbarValidacionesStock();
        
        ConsoleHelper.PresionarParaContinuar();
    }

    static async Task ProbarCalculoTotales()
    {
        ConsoleHelper.EscribirInfo("Probando cálculo de totales...");
        
        var detalles = new List<DetalleVenta>
        {
            new() { Cantidad = 2, PrecioUnitario = 150 },
            new() { Cantidad = 1, PrecioUnitario = 200 },
            new() { Cantidad = 3, PrecioUnitario = 80 }
        };

        var ventaService = new VentaService();
        var total = ventaService.CalcularTotalVenta(detalles);
        
        ConsoleHelper.EscribirExito($"Total calculado: ${total}");
        
        foreach (var detalle in detalles)
        {
            Console.WriteLine($"   {detalle.Cantidad} x ${detalle.PrecioUnitario} = ${detalle.Subtotal}");
        }
    }

    static async Task ProbarProcesoVentaCompleto()
    {
        ConsoleHelper.EscribirInfo("Probando proceso completo de venta...");
        
        try
        {
            var ventaService = new VentaService();
            
            // Crear una venta de prueba
            var venta = new Venta
            {
                Fecha = DateTime.Now,
                Cliente = "Cliente de Prueba",
                Detalles = new List<DetalleVenta>
                {
                    new() { ProductoId = 1, Cantidad = 2, PrecioUnitario = 120 },
                    new() { ProductoId = 6, Cantidad = 1, PrecioUnitario = 80 }
                }
            };

            // Calcular total
            venta.Total = ventaService.CalcularTotalVenta(venta.Detalles);
            
            ConsoleHelper.EscribirExito($"Venta creada - Total: ${venta.Total}");
            ConsoleHelper.EscribirExito("Proceso de venta simulado correctamente");
        }
        catch (Exception ex)
        {
            ConsoleHelper.EscribirError($"Error en proceso de venta: {ex.Message}");
        }
    }
    
    static async Task ProbarValidacionesStock()
    {
        ConsoleHelper.EscribirInfo("Probando validaciones de stock...");
        
        var producto = new Producto { Nombre = "Producto Test", Stock = 2 };
        var cantidadRequerida = 5;
        
        if (producto.Stock < cantidadRequerida)
        {
            ConsoleHelper.EscribirAdvertencia($"Stock insuficiente: {producto.Stock} disponible, {cantidadRequerida} requerido");
        }
        else
        {
            ConsoleHelper.EscribirExito("Stock suficiente");
        }
    }
}
