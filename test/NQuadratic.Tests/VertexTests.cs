//-----------------------------------------------------------------------
// <copyright file="VertexTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;

    using Xunit;

    public class VertexTests
    {
        private readonly Random random;

        private readonly Vertex defaultVertex;

        public VertexTests()
        {
            this.random = new Random();
            this.defaultVertex = NextVertex(this.random);
        }

        public static Vertex NextVertex(Random random)
        {
            if (random == null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            return new Vertex(
                random.NextNonZeroInt64(),
                random.NextInt64(),
                random.NextInt64());
        }

        [Fact]
        public void ConstructorShouldErrorOnZero()
        {
            Assert.Throws<ArgumentException>(() => new Vertex(0, 0, 0));
        }

        [Fact]
        public void ConstructorShouldMapValues()
        {
            var a = this.random.NextNonZeroInt64();
            var x0 = this.random.NextInt64();
            var y0 = this.random.NextInt64();

            var vertex = new Vertex(a, x0, y0);

            Assert.Equal(a, vertex.A);
            Assert.Equal(x0, vertex.X0);
            Assert.Equal(y0, vertex.Y0);
        }

        [Fact]
        public void FromStandardShouldReturnNullOnNull()
        {
            Assert.Null(Vertex.FromStandard(null));
        }

        [Fact]
        public void FromStandardShouldErrorOnOverlyLargeY0()
        {
            Assert.Throws<OverflowException>(() => Vertex.FromStandard(new Standard(1, 1L << 60, 0)));
        }

        [Fact]
        public void FromStandardShouldReturnNullOnNonintegerX0()
        {
            // x0 = -b / 2a
            Assert.Null(Vertex.FromStandard(new Standard(1, 1, 1)));
        }

        [Fact]
        public void FromStandardShouldReturnParseEquation()
        {
            var vertex = Vertex.FromStandard(new Standard(2, -4, 2));

            Assert.Equal(2, vertex.A);
            Assert.Equal(1, vertex.X0);
            Assert.Equal(0, vertex.Y0);
        }

        [Fact]
        public void ToStringShouldReturnStandardOnSimpleSquare()
        {
            var vertex = new Vertex(1, 0, 0);

            Assert.Equal("x²", vertex.ToString());
        }

        [Fact]
        public void ToStringShouldReturnExpandedFormAsAppropriate()
        {
            var vertex = new Vertex(2, 3, 4);

            Assert.Equal("2(x - 3)² + 4", vertex.ToString());
        }

        [Fact]
        public void ToHashCodeShouldReturnA()
        {
            Assert.Equal((int)this.defaultVertex.A, this.defaultVertex.GetHashCode());
        }

        [Fact]
        public void EqualsShouldReturnFalseOnNull()
        {
            Assert.False(this.defaultVertex.Equals((Vertex)null));
            Assert.False(this.defaultVertex.Equals((Standard)null));
            Assert.False(this.defaultVertex.Equals((object)null));
        }

        [Fact]
        public void EqualsShouldReturnTrueOnSameInstance()
        {
            Assert.True(this.defaultVertex.Equals(this.defaultVertex));
        }

        [Fact]
        public void EqualsShouldReturnTrueOnIdenticalInstance()
        {
            var clone = new Vertex(this.defaultVertex.A, this.defaultVertex.X0, this.defaultVertex.Y0);
            Assert.True(this.defaultVertex.Equals((object)clone));

            Assert.True(new Vertex(this.defaultVertex.A, 0, 0).Equals((object)new Standard(this.defaultVertex.A, 0, 0)));
        }

        [Fact]
        public void EqualsShouldReturnFalseOnNonEquation()
        {
            Assert.False(this.defaultVertex.Equals(this.defaultVertex.ToString()));
        }
    }
}
