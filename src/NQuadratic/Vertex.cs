//-----------------------------------------------------------------------
// <copyright file="Vertex.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>Represents a quadratic equation in vertex form: <code>a(x-x₀)²+y₀</code>.</summary>
    public class Vertex : Base
    {
        /// <summary>Initializes a new instance of the <see cref="Vertex" /> class.</summary>
        /// <param name="a">The <c>a</c> value.</param>
        /// <param name="x0">The <c>x₀</c> value.</param>
        /// <param name="y0">The <c>y₀</c> value.</param>
        /// <exception cref="ArgumentException"><paramref name="a" /> is <value>0</value>; this is not a quadratic equation.</exception>
        public Vertex(long a, long x0, long y0)
        {
            if (a == 0L)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Strings.Arg_ZeroNonQuadraticFormat, nameof(a)),
                    nameof(a));
            }

            this.A = a;
            this.X0 = x0;
            this.Y0 = y0;
        }

        /// <summary>Gets the <c>a</c> value.</summary>
        /// <returns>The <c>a</c> value.</returns>
        public long A { get; }

        /// <summary>Gets the <c>x₀</c> value.</summary>
        /// <returns>The <c>x₀</c> value.</returns>
        public long X0 { get; }

        /// <summary>Gets the <c>y₀</c> value.</summary>
        /// <returns>The <c>y₀</c> value.</returns>
        public long Y0 { get; }

        /// <summary>Converts a quadratic equation from the <see cref="Standard" /> form into the <see cref="Vertex" /> form.</summary>
        /// <param name="standard">The equation to convert.</param>
        /// <returns>The equation in <see cref="Vertex" /> form or <value>null</value> if the result would have non-integer values.</returns>
        public static Vertex FromStandard(Standard standard)
        {
            if (standard == null)
            {
                return null;
            }

            var x0 = GetX0(standard);
            if (x0 == null)
            {
                return null;
            }

            var y0 = GetY0(standard, x0.Value);

            return new Vertex(standard.A, x0.Value, y0);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = new StringBuilder();

            Base.AppendValue(result, this.A, true);

            if (this.X0 != 0)
            {
                // NOTE: Normal form is (x - x0)^2
                result.Append("(x");
                Base.AppendValue(result, -this.X0);
                result.Append(")");
            }
            else
            {
                result.Append("x");
            }

            result.Append(Base.SquareCharacter);

            Base.AppendValue(result, this.Y0);

            return result.ToString();
        }

        private static long? GetX0(Standard standard)
        {
            // x0 = -b / 2a
            var numerator = -standard.B;
            var denominator = 2 * standard.A;

            return numerator % denominator == 0
                ? numerator / denominator
                : (long?)null;
        }

        private static long GetY0(Standard standard, long x0)
        {
            // y0 = c - a x0^2
            return standard.C - (standard.A * x0 * x0);
        }
    }
}
