using System;
using System.Linq;

namespace Quadratic
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parameters = args
                    .Select(arg => arg.Split("="))
                    .ToDictionary(arg => arg[0], arg => long.Parse(arg[1]));

                Print(new Standard(parameters["a"], parameters["b"], parameters["c"]));
            }
            catch (Exception e)
            {
                Console.WriteLine("Usage: dotnet run a={value} b={value} c={value}");
                Console.WriteLine();
                Console.WriteLine(e);
            }
        }

        static void Print(Standard standard)
        {
            Console.Write("Standard: ");
            Console.WriteLine(standard);

            var vertex = standard.ToVertex();
            if (vertex != null)
            {
                Console.Write("Vertex: ");
                Console.WriteLine(vertex);
            }

            var factored = standard.ToFactored();
            if (factored != null)
            {
                Console.Write("Factored: ");
                Console.WriteLine(factored);
            }
        }
    }
}

/*
 * $ dotnet run a=2 b=-12 c=16
 * Standard: 2x² - 12x + 16
 * Vertex: 2(x - 3)² - 2
 * Factored: 2(x - 4)(x - 2)
 *
 * $ dotnet run a=1 b=-2 c=0
 * Standard: x² - 2x
 * Vertex: (x - 1)² - 1
 * Factored: x(x - 2)
 *
 * $ dotnet run a=10 b=-6 c=13
 * Standard: 10x² - 6x + 13
 *
 * $ dotnet run a=1 b=5 c=6
 * Standard: x² + 5x + 6
 * Factored: (x + 3)(x + 2)
 *
 * $ dotnet run a=2 b=4 c=-3
 * Standard: 2x² + 4x - 3
 * Vertex: 2(x + 1)² - 5
 */
