using Core.Entities;
using Data;
using Microsoft.EntityFrameworkCore;

namespace ConsoleTest.Helpers;

public static class DataGenerator
{
    public static async Task GenerarDatosPrueba()
    {
        Console.WriteLine("\n🎲 Generando datos de prueba...");

        try
        {
            using var context = new AppDbContext();

            // Verificar si ya hay datos
            if (await context.Productos.AnyAsync())
            {
                ConsoleHelper.EscribirAdvertencia("Ya existen datos en la base de datos");
                return;
            }

            // Productos de ejemplo para casa de comida
            var productos = new List<Producto>
            {
                new() { Nombre = "Empanada Carne", Precio = 120, Stock = 50, Categoria = "Salado" },
                new() { Nombre = "Empanada Pollo", Precio = 120, Stock = 40, Categoria = "Salado" },
                new() { Nombre = "Empanada Jamón Queso", Precio = 130, Stock = 35, Categoria = "Salado" },
                new() { Nombre = "Tarta Jamón Queso", Precio = 180, Stock = 20, Categoria = "Salado" },
                new() { Nombre = "Tarta Verdura", Precio = 170, Stock = 15, Categoria = "Salado" },
                new() { Nombre = "Alfajor", Precio = 80, Stock = 30, Categoria = "Dulce" },
                new() { Nombre = "Medialuna", Precio = 60, Stock = 25, Categoria = "Dulce" },
                new() { Nombre = "Café", Precio = 100, Stock = 100, Categoria = "Bebida" },
                new() { Nombre = "Té", Precio = 80, Stock = 80, Categoria = "Bebida" }
            };

            await context.Productos.AddRangeAsync(productos);
            await context.SaveChangesAsync();

            ConsoleHelper.EscribirExito($"Se generaron {productos.Count} productos de prueba");
        }
        catch (Exception ex)
        {
            ConsoleHelper.EscribirError($"Error generando datos: {ex.Message}");
        }
    }
}
