//-----------------------------------------------------------------------
// <copyright file="FactoredTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;

    using Xunit;

    public class FactoredTests
    {
        private readonly Random random;

        private readonly Factored defaultFactored;

        public FactoredTests()
        {
            this.random = new Random();
            this.defaultFactored = NextFactored(this.random);
        }

        public static Factored NextFactored(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            var eh = NextRelativelyPrimePair(random);
            var fg = NextRelativelyPrimePair(random);

            return new Factored(
                random.NextNonZeroInt64(),
                eh.Item1,
                eh.Item2,
                fg.Item1,
                fg.Item2);
        }

        [Fact]
        public void ConstructorShouldErrorOnZeroDef()
        {
            Assert.Throws<ArgumentException>(() => new Factored(0, 1, 1, 1, 1));
            Assert.Throws<ArgumentException>(() => new Factored(1, 0, 1, 1, 1));
            Assert.Throws<ArgumentException>(() => new Factored(1, 1, 1, 0, 1));
        }

        [Fact]
        public void ConstructorShouldErrorOnOverflow()
        {
            var pair = NextRelativelyPrimePair(this.random);

            Assert.Throws<OverflowException>(
                () => new Factored(long.MaxValue, pair.Item1 * 2L, pair.Item2 * 2L, pair.Item1, pair.Item2));
        }

        [Fact]
        public void ConstructorShouldCleanUpPairs()
        {
            var eh = NextRelativelyPrimePair(this.random);
            long e = eh.Item1;
            long h = eh.Item2;
            var fg = NextRelativelyPrimePair(this.random);
            long f = fg.Item1;
            long g = fg.Item2;

            var equation = new Factored(1, e * 2, h * 2, f * 3, g * 3);

            Assert.Equal(6, equation.D);
            Assert.Equal(e, equation.E);
            Assert.Equal(f, equation.F);
            Assert.Equal(g, equation.G);
            Assert.Equal(h, equation.H);
        }

        [Fact]
        public void ConstructorShouldMapValuesCorrectly()
        {
            var d = this.random.NextNonZeroInt64();
            var eh = NextRelativelyPrimePair(this.random);
            var e = eh.Item1;
            var h = eh.Item2;
            var fg = NextRelativelyPrimePair(this.random);
            var f = fg.Item1;
            var g = fg.Item2;

            var equation = new Factored(d, e, h, f, g);

            Assert.Equal(d, equation.D);
            Assert.Equal(e, equation.E);
            Assert.Equal(f, equation.F);
            Assert.Equal(g, equation.G);
            Assert.Equal(h, equation.H);
        }

        [Fact]
        public void GetHashCodeShouldReturnD()
        {
            Assert.Equal((int)this.defaultFactored.D, this.defaultFactored.GetHashCode());
        }

        [Fact]
        public void EqualsShouldReturnFalseForNonFactored()
        {
            Assert.False(this.defaultFactored.Equals(this.defaultFactored.ToString()));
        }

        [Fact]
        public void EqualsShouldReturnFalseForNull()
        {
            Assert.False(this.defaultFactored.Equals((object)null));
            Assert.False(this.defaultFactored.Equals((Factored)null));
            Assert.False(this.defaultFactored.Equals((Standard)null));
        }

        [Fact]
        public void EqualsShouldReturnTrueForSameInstance()
        {
            Assert.True(this.defaultFactored.Equals(this.defaultFactored));
        }

        [Fact]
        public void EqualsShouldReturnTrueForIdenticalInstance()
        {
            var clone = new Factored(
                this.defaultFactored.D,
                this.defaultFactored.E,
                this.defaultFactored.H,
                this.defaultFactored.F,
                this.defaultFactored.G);
            Assert.True(this.defaultFactored.Equals((object)clone));
        }

        [Fact]
        public void EqualsShouldReturnFalseForDifferentInstances()
        {
            var notClone = new Factored(
                this.defaultFactored.D,
                this.defaultFactored.E,
                this.defaultFactored.H,
                this.defaultFactored.F - 1,
                this.defaultFactored.G - 1);
            Assert.False(this.defaultFactored.Equals((object)notClone));
        }

        [Fact]
        public void EqualsShouldReturnTrueForEquivelantEquations()
        {
            /*
             * 5(4x - 3)(2x + 1)
             * 5 * [(4 * 2)x² + (4 * 1)x + (-3 * 2)x + (-3 * 1)]
             * 5 * [8x² + 4x - 6x - 3]
             * 40x² - 10x - 15
             */
            Assert.True(new Factored(5, 4, -3, 2, 1).Equals((object)new Standard(40, -10, -15)));
        }

        [Fact]
        public void ToStringShouldWork()
        {
            var factored = new Factored(2, -3, 4, 5, -6);

            Assert.Equal("2(-3x + 4)(5x - 6)", factored.ToString());
        }

        [Fact]
        public void ToStringShouldHandleSquares()
        {
            var factored = new Factored(-2, 1, 0, 1, 0);

            Assert.Equal("-2x²", factored.ToString());
        }

        [Fact]
        public void FromStandardShouldErrorOnNull()
        {
            Assert.Throws<ArgumentNullException>(() => Factored.FromStandard(null));
        }

        [Fact]
        public void FromStandardShouldTriviallyHandleZeroC()
        {
            var pair = NextRelativelyPrimePair(this.random);
            var factored = Factored.FromStandard(new Standard(pair.Item1, pair.Item2, 0));

            Assert.Equal(1, factored.D);
            Assert.Equal(1, factored.E);
            Assert.Equal(0, factored.H);
            Assert.Equal(pair.Item1, factored.F);
            Assert.Equal(pair.Item2, factored.G);
        }

        [Fact]
        public void FromStandardShouldFactorCorrectly()
        {
            /*
             * 5(4x - 3)(2x + 1)
             * 5 * [(4 * 2)x² + (4 * 1)x + (-3 * 2)x + (-3 * 1)]
             * 5 * [8x² + 4x - 6x - 3]
             * 40x² - 10x - 15
             */
            var factored = Factored.FromStandard(new Standard(40, -10, -15));

            Assert.Equal(new Factored(5, 4, -3, 2, 1), factored);
        }

        [Fact]
        public void FromStandardShouldReturnNullForUnfactorableEquation()
        {
            var factored = Factored.FromStandard(new Standard(1, 2, 3));

            Assert.Null(factored);
        }

        private static (int, int) NextRelativelyPrimePair(Random random)
        {
            (int, int) pair;

            do
            {
                pair = (random.NextNonZeroInt32(), random.Next());
            }
            while (Math2.Gcd(pair.Item1, pair.Item2) != 1);

            return pair;
        }
    }
}
