using System;
using System.Threading;

class Program
{
    static void Main()
    {
        ShowHeader();

        if (!ValidateAccess()) {
            ShowExitMessage();
            return;
        }
            
        if (!WantsService()) {
            ShowExitMessage();
            return; 
        }
            
        do
        {
            ProcessService();

        } while (WantsService("¿Desea solicitar otro servicio? (S/N): "));

        ShowExitMessage();
    }

    static void ShowHeader()
    {
        Console.WriteLine("++++++++++ BIENVENIDO A LAVASPA HUILA ++++++++++");
        Console.WriteLine("Nombre del estudiante: Edwin Camilo Orozco Marquez");
        Console.WriteLine("LinkeInd: https://www.linkedin.com/in/corozcodev/");
        Console.WriteLine("Nombre de la aplicación: LAVASPA HUILA");
        Console.WriteLine("Curso: Estructura de Datos");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++");
    }

    static bool ValidateAccess()
    {
        Console.Write("\nIngrese la contraseña de acceso: ");
        
        string password = "";
        while (true)
        {
            var key = Console.ReadKey(true); 
            if (key.Key == ConsoleKey.Enter)
                break;
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b"); 
            }
            else if (key.KeyChar != 0)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
        }

        if (password != "HUILA")
        {
            Console.WriteLine("\nContraseña incorrecta. Cerrando la aplicación.");
            return false;
        }

        Console.Write("\nVerificando acceso");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine("\nAcceso concedido. Bienvenido al sistema.\n");
        return true;
    }

    static bool WantsService(string message = "\n¿Desea solicitar un servicio? (S/N): ")
    {
        Console.Write(message);
        return Console.ReadLine().Trim().ToUpper() == "S";
    }

    static void ProcessService()
    {
        Console.Write("\nIngrese su cédula: ");
        string cedula = Console.ReadLine();
        Console.Write("Ingrese su nombre completo: ");
        string nombre = Console.ReadLine();
        
        int estrato = SelectSocioeconomicStratum();

        int opcion = SelectService();
        decimal valorServicio = GetServiceValue(opcion);
        decimal descuento = GetDiscount(estrato, valorServicio);
        decimal valorFinal = valorServicio - descuento;
        
        Console.Write("\nCalculando");
        for (int i = 0; i < 3; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
        }
        Console.WriteLine("\nCálculo completado.");

        ShowSummary(cedula, nombre, opcion, estrato, valorFinal, descuento);
    }

    static int SelectSocioeconomicStratum() 
    {
        Console.WriteLine("\n++++++++++ SELECCIONE SU ESTRATO SOCIOECONÓMICO ++++++++++");
        Console.WriteLine("1. Estrato 1");
        Console.WriteLine("2. Estrato 2");
        Console.WriteLine("3. Estrato 3");
        Console.WriteLine("4. Estrato 4");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        Console.Write("Ingrese la opción deseada (1-4): ");
        return int.Parse(Console.ReadLine());
    }

    static int SelectService()
    {
        Console.WriteLine("\n++++++++++ SELECCIONE SU SERVICIO ++++++++++");
        Console.WriteLine("1. Lavado Sencillo - $25,000");
        Console.WriteLine("2. Lavado General - $45,000");
        Console.WriteLine("3. Lavado General y Polichada - $90,000");
        Console.WriteLine("4. Lavado General, Polichada y Cojinería - $140,000");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++");
        Console.Write("Ingrese la opción deseada (1-4): ");
        return int.Parse(Console.ReadLine());
    }

    static void ShowSummary(string cedula, string nombre, int opcion, int estrato, decimal valorFinal, decimal descuento)
    {
        Console.WriteLine("\n++++++++++ RESUMEN DEL SERVICIO ++++++++++");
        Console.WriteLine($"Identificación: {cedula}");
        Console.WriteLine($"Nombre: {nombre}");
        Console.WriteLine($"Servicio: {GetServiceName(opcion)}");
        Console.WriteLine($"Estrato: {estrato}");
        Console.WriteLine($"Valor a pagar: ${valorFinal}");
        Console.WriteLine($"Descuento aplicado: ${descuento}");
        Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++");
    }

    static void ShowExitMessage()
    {
        Console.WriteLine("Saliendo del sistema. ¡Gracias por usar LAVASPA HUILA!");
    }

    static decimal GetServiceValue(int opcion) => opcion switch
    {
        1 => 25000,
        2 => 45000,
        3 => 90000,
        4 => 140000,
        _ => 0
    };

    static string GetServiceName(int opcion) => opcion switch
    {
        1 => "Lavado Sencillo",
        2 => "Lavado General",
        3 => "Lavado General y Polichada",
        4 => "Lavado General, Polichada y Cojinería",
        _ => "Desconocido"
    };

    static decimal GetDiscount(int estrato, decimal valor) => estrato switch
    {
        1 or 2 => valor * 0.20m,
        3 or 4 => valor * 0.15m,
        _ => valor * 0.10m
    };
}

