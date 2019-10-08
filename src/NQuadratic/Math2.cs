using System;
using System.Collections.Generic;
using System.Linq;

namespace NQuadratic
{
    public class Math2
    {
        public static IEnumerable<(long, long)> Factor(long value, bool includeNegativeEquivelent = false, bool includeReverseEquivelent = false)
        {
            if (value == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot factor 0 into a finite set.");
            }

            var max = Math.Floor(Math.Sqrt(Math.Abs(value)));

            for (var i=1; i<=max; i++)
            {
                if (value % i != 0) continue;
                var alternate = value / i;
                
                yield return (i, alternate);
                if (includeNegativeEquivelent)
                {
                    yield return (-i, -alternate);
                }

                if (includeReverseEquivelent)
                {
                    yield return (alternate, i);
                    if (includeNegativeEquivelent)
                    {
                        yield return (-alternate, -i);
                    }
                }
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
