using System;
using System.Text;

namespace Quadratic
{
    public abstract class Base
    {
        protected const char SquareCharacter = '\u00b2';

        protected static void AppendValue(StringBuilder builder, long value, bool signOnly = false)
        {
            if (value == 0)
            {
                return;
            }

            if (value < 0)
            {
                builder.Append(signOnly ? "-" : " - ");
            }
            else if (!signOnly)
            {
                builder.Append(" + ");
            }

            if (!signOnly || (value != 1 && value != -1))
            {
                builder.Append(Math.Abs(value));
            }
        }
    }
}
