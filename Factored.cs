using System;
using System.Text;

namespace Quadratic
{
    public class Factored : Base
    {
        public Factored(long d, long e, long h, long f, long g)
        {
            if (d == 0L)
            {
                throw new ArgumentException("d must be non-zero; this is not a quadratic equation.", nameof(d));
            }

            if (e == 0L)
            {
                throw new ArgumentException("e must be non-zero; this is not a quadratic equation.", nameof(e));
            }

            if (f == 0L)
            {
                throw new ArgumentException("f must be non-zero; this is not a quadratic equation.", nameof(f));
            }

            this.D = d;
            this.E = e;
            this.F = f;
            this.G = g;
            this.H = h;
        }

        public long D { get; }
        public long E { get; }
        public long F { get; }
        public long G { get; }
        public long H { get; }

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

        public static Factored FromStandard(Standard standard)
        {
            if (standard == null)
            {
                throw new ArgumentNullException(nameof(standard));
            }

            /*
             * y = d(ex + h)(fx + g)
             *   = d(efx^2 + egx + fhx + gh)
             *   = d(efx^2 + (eg + fh)x + gh)
             *
             * y = ax^2 + bx + c
             *   = d(a'x^2 + b'x + c')
             *
             * d = gcd(a, b, c)
             * a' = a/d
             *    = ef
             * b' = b/d
             *    = eg + fh
             * c' = c/d
             *    = gh
             */

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
                    
                    if (e * g + f * h == bPrime)
                    {
                        return new Factored(d, e, h, f, g);
                    }
                }
            }

            return null;
        }
    }
}
