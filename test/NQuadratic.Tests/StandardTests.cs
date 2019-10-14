//-----------------------------------------------------------------------
// <copyright file="StandardTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;

    using Xunit;

    public class StandardTests
    {
        private readonly Random random;

        private readonly Standard defaultStandard;

        public StandardTests()
        {
            this.random = new Random();
            this.defaultStandard = NextStandard(this.random);
        }

        public static Standard NextStandard(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return new Standard(
                random.NextNonZeroInt64(),
                random.NextInt64(),
                random.NextInt64());
        }

        [Fact]
        public void ConstructorShouldErrorOnZero()
        {
            Assert.Throws<ArgumentException>(() => new Standard(0, 0, 0));
        }

        [Fact]
        public void ConstructorShouldMapValues()
        {
            var a = this.random.NextNonZeroInt64();
            var b = this.random.NextInt64();
            var c = this.random.NextInt64();

            var standard = new Standard(a, b, c);

            Assert.Equal(a, standard.A);
            Assert.Equal(b, standard.B);
            Assert.Equal(c, standard.C);
        }

        [Fact]
        public void ToStringShouldReturnSimpleStringOnSimpleSquare()
        {
            Assert.Equal("x²", new Standard(1, 0, 0).ToString());
        }

        [Fact]
        public void ToStringShouldReturnFullStringOnFullEquation()
        {
            Assert.Equal("3x² - 2x + 1", new Standard(3, -2, 1).ToString());
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(3, -2, 1)]
        [InlineData(2, -4, 2)]
        public void ToVertexShouldBeTheSameAsVertexFromStandard(long a, long b, long c)
        {
            var standard = new Standard(a, b, c);

            Assert.Equal(Vertex.FromStandard(standard), standard.ToVertex());
        }

        [Theory]
        [InlineData(1, 0, 0)]
        [InlineData(1, -2, 1)]
        [InlineData(3, -2, 1)]
        [InlineData(40, -10, -15)]
        public void ToFactoredShouldBeTheSameAsFactoredFromStandard(long a, long b, long c)
        {
            var standard = new Standard(a, b, c);

            Assert.Equal(Factored.FromStandard(standard), standard.ToFactored());
        }
    }
}
