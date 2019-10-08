//-----------------------------------------------------------------------
// <copyright file="Base.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic
{
    using System;
    using System.Text;

    /// <summary>The base class for the various quadratic forms.</summary>
    public abstract class Base
    {
        /// <summary>The unicode character for a superscript 2.</summary>
        protected const char SquareCharacter = '\u00b2';

        /// <summary>
        ///     Adds the passed <paramref name="value" /> to the end of the in-progress expression. Note that zero is neither added nor
        ///     concatenated.
        /// </summary>
        /// <param name="expressionBuilder">The in-progress expression.</param>
        /// <param name="value">The value to add to the expression.</param>
        /// <param name="signOnly">
        ///     <value>true</value> to concatentate the value with its sign only; <value>false</value> to add it.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="expressionBuilder" /> is <value>null</value>.</exception>
        protected static void AppendValue(StringBuilder expressionBuilder, long value, bool signOnly = false)
        {
            if (value == 0)
            {
                return;
            }

            if (expressionBuilder == null)
            {
                throw new ArgumentNullException(nameof(expressionBuilder));
            }

            if (value < 0)
            {
                expressionBuilder.Append(signOnly ? "-" : " - ");
            }
            else if (!signOnly)
            {
                expressionBuilder.Append(" + ");
            }

            if (!signOnly || (value != 1 && value != -1))
            {
                expressionBuilder.Append(Math.Abs(value));
            }
        }
    }
}
