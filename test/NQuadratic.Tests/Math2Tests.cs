//-----------------------------------------------------------------------
// <copyright file="Math2Tests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;
    using System.Linq;

    using Xunit;

    public class Math2Tests
    {
        [Fact]
        public void GcdShouldReturnZeroForZeroValueInputs()
        {
            Assert.Equal(0, Math2.Gcd(0L, 0L));
        }

        [Fact]
        public void GcdShouldReturnZeroForEmptyList()
        {
            Assert.Equal(0, Math2.Gcd());
            Assert.Equal(0, Math2.Gcd(Enumerable.Empty<long>()));
            Assert.Equal(0, Math2.Gcd(null));
        }

        [Theory]
        [InlineData(3, 5)]
        [InlineData(5, 3)]
        [InlineData(8, 55)]
        [InlineData(56, 999)]
        public void GcdShouldReturnOneForReletivePrimes(long value1, long value2)
        {
            Assert.Equal(1, Math2.Gcd(value1, value2));
        }

        [Theory]
        [InlineData(-2, 6)]
        [InlineData(2, -6)]
        [InlineData(-6, 3)]
        [InlineData(-6, -3)]
        [InlineData(-12345, 3)]
        [InlineData(12345, 3)]
        public void GcdShouldReturnTheAbsoluteMinForFullyCompositeValues(long value1, long value2)
        {
            Assert.Equal(Math.Min(Math.Abs(value1), Math.Abs(value2)), Math2.Gcd(value1, value2));
        }

        [Theory]
        [InlineData(3, 12345, 12)]
        public void GcdShouldReturnTheExpectedForPartiallyCompositeValues(long expected, long value1, long value2)
        {
            Assert.Equal(expected, Math2.Gcd(value1, value2));
        }

        [Theory]
        [InlineData(3, 12345, 12, 60)]
        public void GcdShouldReturnTheExpectedForPartiallyCompositeLists(long expected, long value1, long value2, long value3)
        {
            Assert.Equal(expected, Math2.Gcd(value1, value2, value3));
        }

        [Fact]
        public void FactorShouldErrorForZero()
        {
            Assert.Throws<ArgumentException>(() => Math2.Factor(0).ToArray());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(11)]
        [InlineData(13)]
        [InlineData(251)]
        [InlineData(2147483647)]
        public void FactorShouldReturnOneSetStartingWithOneForPrimeValues(long value)
        {
            var factors = Math2.Factor(value).ToArray();

            Assert.Single(factors);
            Assert.Equal(1, factors[0].Item1);
            Assert.Equal(value, factors[0].Item2);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(11)]
        public void FactorShouldReturnAllPairsForPowersOfTwo(int power)
        {
            var value = 1L << power;
            var factors = Math2.Factor(value).ToArray();

            Assert.Equal((power + 2) >> 1, factors.Length);

            for (int i = 0; i < factors.Length; i++)
            {
                Assert.Equal(1L << i, factors[i].Item1);
                Assert.Equal(value, factors[i].Item1 * factors[i].Item2);
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(-4)]
        [InlineData(-432)]
        public void FactorWithIncludeNegativeShouldDoubleResults(long value)
        {
            var positiveFactors = Math2.Factor(value).ToArray();
            var bothFactors = Math2.Factor(value, true).ToArray();

            Assert.Equal(positiveFactors.Length * 2, bothFactors.Length);
            for (var i = 0; i < positiveFactors.Length; i++)
            {
                Assert.Equal(positiveFactors[i], bothFactors[i * 2]);

                Assert.Equal(-positiveFactors[i].Item1, bothFactors[(i * 2) + 1].Item1);
                Assert.Equal(-positiveFactors[i].Item2, bothFactors[(i * 2) + 1].Item2);
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(-4)]
        [InlineData(-432)]
        public void FactorWithIncludeReverseShouldNearlyDoubleResults(long value)
        {
            var incrementingFactors = Math2.Factor(value).ToArray();
            var bothFactors = Math2.Factor(value, includeReverseEquivelent: true).ToArray();

            var lastIncrementing = incrementingFactors[incrementingFactors.Length - 1];
            var isValueSquare = lastIncrementing.Item1 == Math.Abs(lastIncrementing.Item2);
            if (isValueSquare)
            {
                Assert.Equal((incrementingFactors.Length * 2) - 1, bothFactors.Length);
            }
            else
            {
                Assert.Equal(incrementingFactors.Length * 2, bothFactors.Length);
            }

            for (var i = 0; i < incrementingFactors.Length; i++)
            {
                var incrementingFactor = incrementingFactors[i];
                Assert.Equal(incrementingFactor, bothFactors[i * 2]);

                if (!isValueSquare || i != (incrementingFactors.Length - 1))
                {
                    Assert.Equal(incrementingFactor.Item2, bothFactors[(i * 2) + 1].Item1);
                    Assert.Equal(incrementingFactor.Item1, bothFactors[(i * 2) + 1].Item2);
                }
            }
        }

        [Theory]
        [InlineData(4)]
        [InlineData(-4)]
        [InlineData(-432)]
        public void FactorWithBothIncludesShouldNearlyQuadrupleResults(long value)
        {
            var incrementingFactors = Math2.Factor(value).ToArray();
            var allFactors = Math2.Factor(value, true, true).ToArray();

            var lastIncrementing = incrementingFactors[incrementingFactors.Length - 1];
            var isValueSquare = lastIncrementing.Item1 == Math.Abs(lastIncrementing.Item2);
            if (isValueSquare)
            {
                Assert.Equal((incrementingFactors.Length * 4) - 2, allFactors.Length);
            }
            else
            {
                Assert.Equal(incrementingFactors.Length * 4, allFactors.Length);
            }

            for (var i = 0; i < incrementingFactors.Length; i++)
            {
                var incrementingFactor = incrementingFactors[i];
                var incrementingFactor1 = incrementingFactor.Item1;
                var incrementingFactor2 = incrementingFactor.Item2;

                Assert.Equal(incrementingFactor, allFactors[i * 4]);
                Assert.Equal(-incrementingFactor1, allFactors[(i * 4) + 1].Item1);
                Assert.Equal(-incrementingFactor2, allFactors[(i * 4) + 1].Item2);

                if (!isValueSquare || i != (incrementingFactors.Length - 1))
                {
                    Assert.Equal(incrementingFactor2, allFactors[(i * 4) + 2].Item1);
                    Assert.Equal(incrementingFactor1, allFactors[(i * 4) + 2].Item2);

                    Assert.Equal(-incrementingFactor2, allFactors[(i * 4) + 3].Item1);
                    Assert.Equal(-incrementingFactor1, allFactors[(i * 4) + 3].Item2);
                }
            }
        }
    }
}
