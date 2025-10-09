using Core.Entities;
using Data;
using Data.Services;
using Microsoft.EntityFrameworkCore;

var ventaService = new VentaService();
var productoService = new ProductoService();

while (true)
{
    Console.WriteLine("MENÚ PRINCIPAL");
    Console.WriteLine("1. Ingresar nuevo producto a la BD");
    Console.WriteLine("2. Ver productos en BD");
    Console.WriteLine("3. Registrar venta en BD");
    Console.WriteLine("4. Simular venta (sin BD)");
    Console.WriteLine("5. Salir");
    Console.Write("Seleccione una opción: ");

    var opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            await IngresarProductoABaseDeDatos();
            break;
        case "2":
            await VerProductosEnBaseDeDatos();
            break;
        case "3":
            await RegistrarVentaEnBaseDeDatos();
            break;
        case "4":
            SimularVenta();
            break;
        case "5":
            Console.WriteLine("¡Hasta luego! 👋");
            return;
        default:
            Console.WriteLine("❌ Opción no válida");
            break;
    }
}

// Función para ingresar productos a la BD
async Task IngresarProductoABaseDeDatos()
{
    try
    {
        Console.WriteLine("\n--- INGRESAR NUEVO PRODUCTO ---");
        
        Console.Write("Nombre del producto: ");
        var nombre = Console.ReadLine();
        
        Console.Write("Precio: ");
        var precio = decimal.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Stock inicial: ");
        var stock = int.Parse(Console.ReadLine() ?? "0");
        
        Console.Write("Categoría: ");
        var categoria = Console.ReadLine();
        
        Console.Write("Descripción: ");
        var descripcion = Console.ReadLine();

        // Crear el producto
        var producto = new Producto 
        { 
            Nombre = nombre,
            Precio = precio,
            Stock = stock,
            Categoria = categoria,
            Descripcion = descripcion
        };

        // Guardar en base de datos
        using var context = new AppDbContext();
        context.Productos.Add(producto);
        await context.SaveChangesAsync();
        
        Console.WriteLine($"✅ Producto '{producto.Nombre}' guardado en BD con ID: {producto.Id}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al guardar producto: {ex.Message}");
    }
}

// Función para ver productos en BD
async Task VerProductosEnBaseDeDatos()
{
    try
    {
        Console.WriteLine("\n--- PRODUCTOS EN BASE DE DATOS ---");
        
        using var context = new AppDbContext();
        var productos = await context.Productos.ToListAsync();
        
        if (productos.Count == 0)
        {
            Console.WriteLine("📭 No hay productos en la base de datos");
            return;
        }
        
        foreach (var producto in productos)
        {
            Console.WriteLine($"🆔 {producto.Id} | 🍕 {producto.Nombre} | 💰 ${producto.Precio} | 📦 {producto.Stock} | 🏷️ {producto.Categoria}");
        }
        
        Console.WriteLine($"\n📊 Total: {productos.Count} productos");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al leer productos: {ex.Message}");
    }
}

// Función para registrar venta en BD
async Task RegistrarVentaEnBaseDeDatos()
{
    try
    {
        Console.WriteLine("\n--- REGISTRAR VENTA EN BD ---");
        
        // Primero mostrar productos disponibles
        using var context = new AppDbContext();
        var productos = await context.Productos.ToListAsync();
        
        if (productos.Count == 0)
        {
            Console.WriteLine("❌ No hay productos disponibles para vender");
            return;
        }
        
        Console.WriteLine("Productos disponibles:");
        foreach (var producto in productos)
        {
            Console.WriteLine($"  {producto.Id}. {producto.Nombre} - ${producto.Precio} (Stock: {producto.Stock})");
        }
        
        // Crear venta
        var venta = new Venta
        {
            Fecha = DateTime.Now,
            Cliente = "Cliente desde Consola",
            Detalles = new List<DetalleVenta>()
        };
        
        bool continuar = true;
        while (continuar)
        {
            Console.Write("\nID del producto a vender: ");
            var productoId = int.Parse(Console.ReadLine() ?? "0");
            
            Console.Write("Cantidad: ");
            var cantidad = int.Parse(Console.ReadLine() ?? "0");
            
            var producto = productos.FirstOrDefault(p => p.Id == productoId);
            if (producto != null)
            {
                venta.Detalles.Add(new DetalleVenta
                {
                    ProductoId = productoId,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });
                
                Console.WriteLine($"✅ Agregado: {cantidad} x {producto.Nombre}");
            }
            else
            {
                Console.WriteLine("❌ Producto no encontrado");
            }
            
            Console.Write("¿Agregar otro producto? (s/n): ");
            continuar = (Console.ReadLine()?.ToLower() == "s");
        }
        
        venta.Total = venta.Detalles.Sum(d => d.Subtotal);
        
        context.Ventas.Add(venta);
        await context.SaveChangesAsync();
        
        Console.WriteLine($"💰 Venta registrada con ID: {venta.Id} - Total: ${venta.Total}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al registrar venta: {ex.Message}");
    }
}

void SimularVenta()
{
    Console.WriteLine("\n--- SIMULACIÓN DE VENTA ---");
    
    var producto1 = new Producto 
    { 
        Id = 1, 
        Nombre = "Empanada Carne", 
        Precio = 12000, 
        Stock = 50, 
        Categoria = "Salado" 
    };

    var producto2 = new Producto 
    { 
        Id = 2, 
        Nombre = "Alfajor", 
        Precio = 8000, 
        Stock = 30, 
        Categoria = "Dulce" 
    };

    Console.WriteLine($"✅ Producto 1: {producto1.Nombre} - ${producto1.Precio}");
    Console.WriteLine($"✅ Producto 2: {producto2.Nombre} - ${producto2.Precio}");

    var detalles = new List<DetalleVenta>
    {
        new() { Cantidad = 2, PrecioUnitario = producto1.Precio },
        new() { Cantidad = 1, PrecioUnitario = producto2.Precio }
    };

    var total = ventaService.CalcularTotalVenta(detalles);
    Console.WriteLine($"✅ Total de venta calculado: ${total}");

    Console.WriteLine("\n📋 Detalles de la venta:");
    foreach (var detalle in detalles)
    {
        Console.WriteLine($"   {detalle.Cantidad} x ${detalle.PrecioUnitario} = ${detalle.Subtotal}");
    }
}
