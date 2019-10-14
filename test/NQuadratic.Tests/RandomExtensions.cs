//-----------------------------------------------------------------------
// <copyright file="RandomExtensions.cs" company="N/A">
//     Copyright © 2019 David Beckman. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace NQuadratic.Tests
{
    using System;

    public static class RandomExtensions
    {
        public static long NextNonZeroInt64(this Random value)
        {
            long result;

            do
            {
                result = value.NextInt64();
            }
            while (result == 0);

            return result;
        }

        public static int NextNonZeroInt32(this Random value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            int result;

            do
            {
                result = value.Next();
            }
            while (result == 0);

            return result;
        }

        public static long NextInt64(this Random value)
        {
            return BitConverter.ToInt64(NextBytes(value, 8));
        }

        public static byte[] NextBytes(this Random value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var buffer = new byte[length];
            value.NextBytes(buffer);
            return buffer;
        }
    }
}
