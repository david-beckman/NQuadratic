using System;
using System.Collections.Generic;
using System.Linq;

namespace Quadratic
{
    public class Math2
    {
        public static IEnumerable<(long, long)> Factor(long value, bool includeNegativeEquivelent = false)
        {
            if (value == 0)
            {
                yield return (0L, 0L);
            }

            var max = value == 0 ? long.MaxValue : Math.Floor(Math.Sqrt(Math.Abs(value)));

            for (var i=1; i<=max; i++)
            {
                if (value % i != 0) continue;
                var alternate = value / i;
                
                yield return (i, alternate);
                if (includeNegativeEquivelent)
                    yield return (-i, -alternate);
            }
            
            yield break;
        }

        public static long Gcd(params long[] numbers)
        {
            return Gcd((IList<long>)numbers);
        }

        public static long Gcd(IEnumerable<long> numbers)
        {
            return numbers?.Aggregate(Gcd) ?? 0;
        }

        public static long Gcd(long first, long second)
        {
            var result = second == 0 ? first : Gcd(second, first % second);
            return result < 0 ? -result : result;
        }
    }
}
