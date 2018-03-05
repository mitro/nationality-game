using System;
using System.Collections.Generic;
using System.Linq;

namespace NationalityGame.Mechanics.Utils
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(i => Guid.NewGuid()).ToList();
        }
    }
}