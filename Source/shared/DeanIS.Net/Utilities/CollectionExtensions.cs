using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanIS.Net.Utilities
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty(this ICollection collection)
        {
            return (collection == null || collection.Count == 0);
        }
    }
}
