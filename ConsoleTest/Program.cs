using Core.Entities;
using Data.Services;
using Data;
using Microsoft.EntityFrameworkCore;
using ConsoleTest.TestScenarios;
using ConsoleTest.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("🧪 CONSOLA DE PRUEBAS - CASA COMIDA CASERA");
        Console.WriteLine("===========================================\n");

        // Menú interactivo
        while (true)
        {
            Console.WriteLine("\n🎯 ¿Qué quieres probar?");
            Console.WriteLine("1. Pruebas de Productos");
            Console.WriteLine("2. Pruebas de Ventas");
            Console.WriteLine("3. Pruebas de Stock");
            Console.WriteLine("4. Pruebas de Base de Datos");
            Console.WriteLine("5. Generar Datos de Prueba");
            Console.WriteLine("6. Ejecutar TODAS las pruebas");
            Console.WriteLine("0. Salir");
            Console.Write("Selecciona: ");

            var opcion = Console.ReadLine();
            
            switch (opcion)
            {
                case "1":
                    await ProductoTests.Ejecutar();
                    break;
                case "2":
                    await VentaTests.Ejecutar();
                    break;
                case "3":
                    await StockTests.Ejecutar();
                    break;
                case "4":
                    await PruebasBaseDatos();
                    break;
                case "5":
                    await DataGenerator.GenerarDatosPrueba();
                    break;
                case "6":
                    await EjecutarTodasLasPruebas();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("❌ Opción inválida");
                    break;
            }
        }
    }

    static async Task PruebasBaseDatos()
    {
        Console.WriteLine("\n🔍 Probando conexión a Base de Datos...");
        
        try
        {
            using var context = new AppDbContext();
            
            // Probar conexión
            var puedeConectar = await context.Database.CanConnectAsync();
            Console.WriteLine(puedeConectar ? "✅ BD Conectada" : "❌ BD No conectada");
            
            // Estadísticas
            var productosCount = await context.Productos.CountAsync();
            var ventasCount = await context.Ventas.CountAsync();
            
            Console.WriteLine($"📊 Productos en BD: {productosCount}");
            Console.WriteLine($"📊 Ventas en BD: {ventasCount}");
            
            // Probar una consulta compleja
            var productosConStock = await context.Productos
                .Where(p => p.Stock > 0)
                .OrderBy(p => p.Nombre)
                .ToListAsync();
                
            Console.WriteLine($"📦 Productos con stock: {productosConStock.Count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"💥 Error: {ex.Message}");
        }
    }

    static async Task EjecutarTodasLasPruebas()
    {
        Console.WriteLine("\n🚀 EJECUTANDO SUITE COMPLETA DE PRUEBAS...");
        
        await ProductoTests.Ejecutar();
        await VentaTests.Ejecutar();
        await StockTests.Ejecutar();
        await PruebasBaseDatos();
        
        Console.WriteLine("\n🎉 TODAS LAS PRUEBAS COMPLETADAS");
    }
}
