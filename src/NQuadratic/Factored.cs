//-----------------------------------------------------------------------
// <copyright file="Factored.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    ///     Represents a quadratic equation in factored form: <code>d(ex + h)(fx + g)</code>. Note that this implies that the pairs, e-h
    ///     and f-g, are relatively prime.
    /// </summary>
    public class Factored : Base, IEquatable<Factored>, IEquatable<Standard>
    {
        /// <summary>Initializes a new instance of the <see cref="Factored" /> class.</summary>
        /// <param name="d">The <c>d</c> value.</param>
        /// <param name="e">The <c>e</c> value.</param>
        /// <param name="h">The <c>h</c> value.</param>
        /// <param name="f">The <c>f</c> value.</param>
        /// <param name="g">The <c>g</c> value.</param>
        /// <exception cref="ArgumentException">
        ///     <paramref name="d" />, <paramref name="d" />, or <paramref name="d" /> is <value>0</value>; this is not a quadratic
        ///     equation.
        /// </exception>
        /// <exception cref="OverflowException">
        ///     The product of <paramref name="d" /> and the <see cref="Math2.Gcd(long, long)" /> of the <paramref name="e" />,
        ///     <paramref name="h" /> pair and the <paramref name="f" />, <paramref name="g" /> pair exceeds the bounds of a
        ///     <see cref="long" />.
        /// </exception>
        public Factored(long d, long e, long h, long f, long g)
        {
            if (d == 0L)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Strings.Arg_ZeroNonQuadraticFormat, nameof(d)),
                    nameof(d));
            }

            if (e == 0L)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Strings.Arg_ZeroNonQuadraticFormat, nameof(e)),
                    nameof(e));
            }

            if (f == 0L)
            {
                throw new ArgumentException(
                    string.Format(CultureInfo.CurrentCulture, Strings.Arg_ZeroNonQuadraticFormat, nameof(f)),
                    nameof(f));
            }

            var ehGcd = Math2.Gcd(e, h);
            var fgGcd = Math2.Gcd(f, g);

            checked
            {
                this.D = d * ehGcd * fgGcd;
            }

            this.E = e / ehGcd;
            this.F = f / fgGcd;
            this.G = g / fgGcd;
            this.H = h / ehGcd;
        }

        /// <summary>Gets the <c>d</c> value.</summary>
        /// <returns>The <c>d</c> value.</returns>
        public long D { get; }

        /// <summary>Gets the <c>e</c> value.</summary>
        /// <returns>The <c>e</c> value.</returns>
        public long E { get; }

        /// <summary>Gets the <c>f</c> value.</summary>
        /// <returns>The <c>f</c> value.</returns>
        public long F { get; }

        /// <summary>Gets the <c>g</c> value.</summary>
        /// <returns>The <c>g</c> value.</returns>
        public long G { get; }

        /// <summary>Gets the <c>h</c> value.</summary>
        /// <returns>The <c>h</c> value.</returns>
        public long H { get; }

        /// <summary>Converts a quadratic equation from the <see cref="Standard" /> form into the <see cref="Factored" /> form.</summary>
        /// <param name="standard">The equation to convert.</param>
        /// <returns>
        ///     The equation in <see cref="Factored" /> form or <value>null</value> if the result would have non-integer values.
        /// </returns>
        public static Factored FromStandard(Standard standard)
        {
            if (standard == null)
            {
                throw new ArgumentNullException(nameof(standard));
            }

            var d = Math2.Gcd(standard.A, standard.B, standard.C);
            var aPrime = standard.A / d;
            var bPrime = standard.B / d;
            var cPrime = standard.C / d;

            if (cPrime == 0)
            {
                return new Factored(d, 1, 0, aPrime, bPrime);
            }

            foreach (var cPrimeFactor in Math2.Factor(cPrime, true, true))
            {
                var g = cPrimeFactor.Item1;
                var h = cPrimeFactor.Item2;

                foreach (var aPrimeFactor in Math2.Factor(aPrime))
                {
                    var e = aPrimeFactor.Item1;
                    var f = aPrimeFactor.Item2;

                    if ((e * g) + (f * h) == bPrime)
                    {
                        return new Factored(d, e, h, f, g);
                    }
                }
            }

            return null;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var result = new StringBuilder();

            Base.AppendValue(result, this.D, true);

            AppendSegment(result, this.E, this.H);

            if (this.E == this.F && this.H == this.G)
            {
                result.Append(SquareCharacter);
            }
            else
            {
                AppendSegment(result, this.F, this.G);
            }

            return result.ToString();
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return (int)this.D;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            var factored = obj as Factored;
            if (factored != null)
            {
                return this.Equals(factored);
            }

            var standard = obj as Standard;
            if (standard != null)
            {
                return this.Equals(standard);
            }

            return false;
        }

        /// <inheritdoc />
        public bool Equals(Factored other)
        {
            return other != null &&
                this.D == other.D &&
                ((this.E == other.E &&
                this.F == other.F &&
                this.G == other.G &&
                this.H == other.H) ||
                (this.E == other.F &&
                this.F == other.E &&
                this.G == other.H &&
                this.H == other.G));
        }

        /// <inheritdoc />
        public bool Equals(Standard other)
        {
            return other != null && this.Equals(FromStandard(other));
        }

        private static void AppendSegment(StringBuilder builder, long factor, long addor)
        {
            if (factor == 1 && addor == 0)
            {
                builder.Append("x");
                return;
            }

            builder.Append("(");
            Base.AppendValue(builder, factor, true);
            builder.Append("x");
            Base.AppendValue(builder, addor);
            builder.Append(")");
        }
    }
}
