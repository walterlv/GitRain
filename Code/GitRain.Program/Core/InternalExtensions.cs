using System;
using System.Collections.Generic;

namespace Cvte.GitRain
{
    internal static class InternalExtensions
    {
        public static void ForEach<T>([NotNull] this IEnumerable<T> sequence, [NotNull] Action<T> action)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");
            if (action == null) throw new ArgumentNullException("action");
            foreach (var item in sequence)
            {
                action(item);
            }
        }
    }
}
