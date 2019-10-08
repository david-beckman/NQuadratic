//-----------------------------------------------------------------------
// <copyright file="Standard.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>Represents a quadratic equation in standard form: <code>ax²+bx+c</code>.</summary>
    public class Standard : Base
    {
        /// <summary>Initializes a new instance of the <see cref="Standard" /> class.</summary>
        /// <param name="a">The <c>a</c> value.</param>
        /// <param name="b">The <c>b</c> value.</param>
        /// <param name="c">The <c>c</c> value.</param>
        /// <exception cref="ArgumentException"><paramref name="a" /> is <value>0</value>; this is not a quadratic equation.</exception>
        public Standard(long a, long b, long c)
        {
            if (a == 0L)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Strings.Arg_ZeroNonQuadraticFormat, nameof(a)),
                    nameof(a));
            }

            this.A = a;
            this.B = b;
            this.C = c;
        }

        /// <summary>Gets the <c>a</c> value.</summary>
        /// <returns>The <c>a</c> value.</returns>
        public long A { get; }

        /// <summary>Gets the <c>b</c> value.</summary>
        /// <returns>The <c>b</c> value.</returns>
        public long B { get; }

        /// <summary>Gets the <c>c</c> value.</summary>
        /// <returns>The <c>c</c> value.</returns>
        public long C { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = new StringBuilder();

            Base.AppendValue(result, this.A, true);
            result.Append("x");
            result.Append(Base.SquareCharacter);

            if (this.B != 0)
            {
                Base.AppendValue(result, this.B);
                result.Append("x");
            }

            Base.AppendValue(result, this.C);

            return result.ToString();
        }

        /// <summary>Converts this quadratic equation into the <see cref="Vertex" /> form.</summary>
        /// <returns>
        ///     The equation in <see cref="Vertex" /> form or <value>null</value> if the result would have non-integer values.
        /// </returns>
        public Vertex ToVertex()
        {
            return Vertex.FromStandard(this);
        }

        /// <summary>Converts this quadratic equation into the <see cref="Factored" /> form.</summary>
        /// <returns>
        ///     The equation in <see cref="Factored" /> form or <value>null</value> if the result would have non-integer values.
        /// </returns>
        public Factored ToFactored()
        {
            return Factored.FromStandard(this);
        }
    }
}
