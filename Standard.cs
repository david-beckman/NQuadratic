using System;
using System.Text;

namespace Quadratic
{
    public class Standard : Base
    {
        public Standard(long a, long b, long c)
        {
            if (a == 0L)
            {
                throw new ArgumentException("a must be non-zero; this is not a quadratic equation.", nameof(a));
            }

            this.A = a;
            this.B = b;
            this.C = c;
        }

        public long A { get; }
        public long B { get; }
        public long C { get; }

        public override string ToString()
        {
            var result = new StringBuilder();

            Base.AppendValue(result, this.A, true);
            result.Append("x");
            result.Append(Base.SquareCharacter);

            if (this.B != 0)
            {
                Base.AppendValue(result, this.B);
                result.Append("x");
            }

            Base.AppendValue(result, this.C);
            
            return result.ToString();
        }

        public Vertex ToVertex()
        {
            return Vertex.FromStandard(this);
        }

        public Factored ToFactored()
        {
            return Factored.FromStandard(this);
        }
    }
}
