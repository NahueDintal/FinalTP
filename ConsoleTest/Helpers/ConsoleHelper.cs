namespace ConsoleTest.Helpers;

public static class ConsoleHelper
{
    public static void EscribirExito(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"✅ {mensaje}");
        Console.ResetColor();
    }

    public static void EscribirError(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"❌ {mensaje}");
        Console.ResetColor();
    }

    public static void EscribirAdvertencia(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"⚠️  {mensaje}");
        Console.ResetColor();
    }

    public static void EscribirInfo(string mensaje)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"ℹ️  {mensaje}");
        Console.ResetColor();
    }

    public static void PresionarParaContinuar()
    {
        Console.WriteLine("\n--- Presiona cualquier tecla para continuar ---");
        Console.ReadKey();
    }
}
