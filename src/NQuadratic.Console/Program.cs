//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Console
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Console = System.Console;

    internal class Program
    {
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Allowed on main.")]
        private static void Main(string[] args)
        {
            // Support Cygwin
            var lang = Environment.GetEnvironmentVariable("LANG");
            if (!string.IsNullOrEmpty(lang) && lang.EndsWith("UTF-8", StringComparison.OrdinalIgnoreCase))
            {
                Console.OutputEncoding = Encoding.UTF8;
            }

            try
            {
                var parameters = args
                    .Select(arg => arg.Split("="))
                    .ToDictionary(arg => arg[0], arg => long.Parse(arg[1], NumberStyles.Integer, CultureInfo.CurrentCulture));

                Print(new Standard(parameters["a"], parameters["b"], parameters["c"]));
            }
            catch (Exception e)
            {
                Console.WriteLine(Strings.Usage);
                Console.WriteLine();
                Console.WriteLine(e);
            }
        }

        private static void Print(Standard standard)
        {
            Console.Write(Strings.StandardFormName);
            Console.Write(Strings.FormNameAndValueSeparator);
            Console.WriteLine(standard);

            var vertex = standard.ToVertex();
            if (vertex != null)
            {
                Console.Write(Strings.VertexFormName);
                Console.Write(Strings.FormNameAndValueSeparator);
                Console.WriteLine(vertex);
            }

            var factored = standard.ToFactored();
            if (factored != null)
            {
                Console.Write(Strings.FactoredFormName);
                Console.Write(Strings.FormNameAndValueSeparator);
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
