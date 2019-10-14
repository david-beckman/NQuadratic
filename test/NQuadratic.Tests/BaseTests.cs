//-----------------------------------------------------------------------
// <copyright file="BaseTests.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;

    using Xunit;

    public class BaseTests
    {
        private Random random = new Random();

        [Fact]
        public void AppendValueShouldErrorWhenExpressionBuilderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => BaseWrapper.AppendValueWrapper(null, 1, false));
        }

        [Fact]
        public void AppendValueShouldDoNothingForZero()
        {
            var builder = new StringBuilder();
            BaseWrapper.AppendValueWrapper(builder, 0, false);

            Assert.Equal(0, builder.Length);
        }

        [Fact]
        public void AppendValueShouldDoNothingForOneSignOnly()
        {
            var builder = new StringBuilder();
            BaseWrapper.AppendValueWrapper(builder, 1, true);

            Assert.Equal(0, builder.Length);
        }

        [Fact]
        public void AppendValueShouldOnlyByHyphenForNegativeOneSignOnly()
        {
            var builder = new StringBuilder();
            BaseWrapper.AppendValueWrapper(builder, -1, true);

            Assert.Equal("-", builder.ToString());
        }

        [Fact]
        public void AppendValueShouldHaveSubtrationForNegative()
        {
            var value = ((long)this.random.Next()) + 1;
            var builder = new StringBuilder();
            BaseWrapper.AppendValueWrapper(builder, -value, false);

            Assert.Equal(" - " + value, builder.ToString());
        }

        [Fact]
        public void AppendValueShouldHaveAdditionForPositive()
        {
            var value = ((long)this.random.Next()) + 1;
            var builder = new StringBuilder();
            BaseWrapper.AppendValueWrapper(builder, value, false);

            Assert.Equal(" + " + value, builder.ToString());
        }

        [SuppressMessage(
            "Microsoft.Performance",
            "CA1812:AvoidUninstantiatedInternalClasses",
            Justification = "Can't mark an inherited class as static.")]
        private class BaseWrapper : Base
        {
            internal static void AppendValueWrapper(StringBuilder expressionBuilder, long value, bool signOnly)
            {
                Base.AppendValue(expressionBuilder, value, signOnly);
            }
        }
    }
}
