using System;
using System.Text;

namespace NQuadratic
{
    public class Vertex : Base
    {
        public Vertex(long a, long x0, long y0)
        {
            if (a == 0L)
            {
                throw new ArgumentException("a must be non-zero; this is not a quadratic equation.", nameof(a));
            }

            this.A = a;
            this.X0 = x0;
            this.Y0 = y0;
        }

        public long A { get; }
        public long X0 { get; }
        public long Y0 { get; }

        public override string ToString()
        {
            var result = new StringBuilder();

            Base.AppendValue(result, this.A, true);

            if (this.X0 != 0)
            {
                // NOTE: Normal form is (x - x0)^2
                result.Append("(x");
                Base.AppendValue(result, -this.X0);
                result.Append(")");
            }
            else
            {
                result.Append("x");
            }

            result.Append(Base.SquareCharacter);

            Base.AppendValue(result, this.Y0);

            return result.ToString();
        }

        public static Vertex FromStandard(Standard standard)
        {
            if (standard == null)
            {
                throw new ArgumentNullException(nameof(standard));
            }

            /*
             * y = a(x - x0)^2 + y0
             *   = a(x^2 - 2 x0 x + x0^2) + y0
             *   = ax^2 - 2a x0 x + a x0^2 + y0
             *
             * y = ax^2 + bx + c
             *
             * b = -2a x0
             * x0 = -b / 2a
             *
             * c = a x0^2 + y0
             * y0 = c - a x0^2
             *    = c - b^2 / 4a
             */
            var x0 = GetX0(standard);
            if (x0 == null)
            {
                return null;
            }

            var y0 = GetY0(standard, x0.Value);
            
            return new Vertex(standard.A, x0.Value, y0);
        }

        private static long? GetX0(Standard standard)
        {
            // x0 = -b / 2a
            var numerator = -standard.B;
            var denominator = 2 * standard.A;

            return numerator % denominator == 0
                ? numerator / denominator
                : (long?)null;
        }

        private static long GetY0(Standard standard, long x0)
        {
            // y0 = c - a x0^2
            return standard.C - (standard.A * x0 * x0);
        }
    }
}
