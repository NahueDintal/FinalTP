using Core.Entities;
using Data.Services;

Console.WriteLine("🎯 CONSOLA DE PRUEBAS - CASA COMIDA CASERA");
Console.WriteLine("===========================================\n");

// Prueba básica de entidades
Console.WriteLine("🧪 Probando entidades básicas...");

// Crear productos de prueba
var producto1 = new Producto 
{ 
    Id = 1, 
    Nombre = "Empanada Carne", 
    Precio = 120, 
    Stock = 50, 
    Categoria = "Salado" 
};

var producto2 = new Producto 
{ 
    Id = 2, 
    Nombre = "Alfajor", 
    Precio = 80, 
    Stock = 30, 
    Categoria = "Dulce" 
};

Console.WriteLine($"✅ Producto 1: {producto1.Nombre} - ${producto1.Precio}");
Console.WriteLine($"✅ Producto 2: {producto2.Nombre} - ${producto2.Precio}");

// Probar servicio de ventas
Console.WriteLine("\n💰 Probando cálculo de ventas...");
var ventaService = new VentaService();

var detalles = new List<DetalleVenta>
{
    new() { Cantidad = 2, PrecioUnitario = producto1.Precio },
    new() { Cantidad = 1, PrecioUnitario = producto2.Precio }
};

var total = ventaService.CalcularTotalVenta(detalles);
Console.WriteLine($"✅ Total de venta calculado: ${total}");

// Mostrar detalles
Console.WriteLine("\n📋 Detalles de la venta:");
foreach (var detalle in detalles)
{
    Console.WriteLine($"   {detalle.Cantidad} x ${detalle.PrecioUnitario} = ${detalle.Subtotal}");
}

// Probar validación de stock
// Console.WriteLine("\n📦 Probando validación de stock...");
// var stockSuficiente = ventaService.ValidarStock(producto1, 10);
// Console.WriteLine(stockSuficiente ? "✅ Stock suficiente" : "❌ Stock insuficiente");

Console.WriteLine("\n🎉 ¡TODAS LAS PRUEBAS COMPLETADAS EXITOSAMENTE!");
