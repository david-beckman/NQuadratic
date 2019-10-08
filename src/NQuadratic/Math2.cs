//-----------------------------------------------------------------------
// <copyright file="Math2.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A collection of math functions beyond what is provided in <see cref="Math" />.</summary>
    public static class Math2
    {
        /// <summary>Returns a set of factors that make up the passed <paramref name="value" />.</summary>
        /// <param name="value">The value to factor.</param>
        /// <param name="includeNegativeEquivelent">
        ///     <value>true</value> to include both the positive and negative versions of the factors; <value>false</value> to only include
        ///     the positive version. For example, <value>-4</value> can return <value>(1, -4)</value> and <value>(2, -2)</value> vs
        ///     <value>(1, -4)</value>, <value>(-1, 4)</value>, <value>(2, -2)</value>, and <value>(-2, 2)</value>.
        /// </param>
        /// <param name="includeReverseEquivelent">
        ///     <value>true</value> to include both orderings for the factors; <value>false</value> to include the smaller value first. For
        ///     example, <value>4</value> can return <value>(1, 4)</value> and <value>(2, 2)</value> vs <value>(1, 4)</value>,
        ///     <value>(4, 1)</value>, and <value>(2, 2)</value>.
        /// </param>
        /// <returns>
        ///     The set of factors for the <paramref name="value" /> as tuples. Some <paramref name="includeNegativeEquivelent" /> will
        ///     always double the factors; <paramref name="includeReverseEquivelent" /> will double the factors except for squares.
        /// </returns>
        /// <exception cref="ArgumentException">
        ///     <paramref name="value" /> is <value>0</value>. A finite set of factors cannot be determined in this case.
        /// </exception>
        public static IEnumerable<(long, long)> Factor(long value, bool includeNegativeEquivelent = false, bool includeReverseEquivelent = false)
        {
            if (value == 0)
            {
                throw new ArgumentException(Strings.Arg_FactorZero, nameof(value));
            }

            var max = Math.Floor(Math.Sqrt(Math.Abs(value)));

            for (var i = 1; i <= max; i++)
            {
                if (value % i != 0)
                {
                    continue;
                }

                var alternate = value / i;

                yield return (i, alternate);
                if (includeNegativeEquivelent)
                {
                    yield return (-i, -alternate);
                }

                if (includeReverseEquivelent && Math.Abs(alternate) != i)
                {
                    yield return (alternate, i);
                    if (includeNegativeEquivelent)
                    {
                        yield return (-alternate, -i);
                    }
                }
            }

            yield break;
        }

        /// <summary>Returns the GCD (Greatest common denominator) for the set of <paramref name="numbers" />.</summary>
        /// <param name="numbers">The set of numbers to run the GCD algorithm against.</param>
        /// <returns>The result of the GCD algorithm or <value>0</value> if the set is empty. This number is always positive.</returns>
        public static long Gcd(params long[] numbers)
        {
            return Gcd((IList<long>)numbers);
        }

        /// <summary>Returns the GCD (Greatest common denominator) for the set of <paramref name="numbers" />.</summary>
        /// <param name="numbers">The set of numbers to run the GCD algorithm against.</param>
        /// <returns>The result of the GCD algorithm or <value>0</value> if the set is empty. This number is always positive.</returns>
        public static long Gcd(IEnumerable<long> numbers)
        {
            try
            {
                return numbers?.Aggregate(Gcd) ?? 0;
            }
            catch (InvalidOperationException)
            {
                // Sequence contains no elements.
                return 0;
            }
        }

        /// <summary>Returns the GCD (Greatest common denominator) for the two numbers passed.</summary>
        /// <param name="first">The first number to include when running the GCD algorithm.</param>
        /// <param name="second">The second number to include when running the GCD algorithm.</param>
        /// <returns>The result of the GCD algorithm. This number is always positive.</returns>
        public static long Gcd(long first, long second)
        {
            var result = second == 0 ? first : Gcd(second, first % second);
            return result < 0 ? -result : result;
        }
    }
}
