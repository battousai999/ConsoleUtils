using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Battousai.Utils.EnumerableUtils
{
    public static class EnumerableUtils
    {
        /// <summary>
        /// Returns an enumerable containing the elements passed to this method.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the returned enumerable</typeparam>
        /// <param name="elements">The elements contained in the returned enumerable</param>
        /// <returns>An enumerable containing the elements passed to this method.</returns>
        public static IEnumerable<T> EnumerableOf<T>(params T[] elements)
        {
            foreach (var element in elements)
            {
                yield return element;
            }
        }

        /// <summary>
        /// Returns an enumerable containing the single element passed to this method.
        /// </summary>
        /// <typeparam name="T">The type of the element in the returned enumerable</typeparam>
        /// <param name="element">The single element contined in the returned enumerable</param>
        /// <returns>An enumerable containing the single element passed to this method.</returns>
        public static IEnumerable<T> ToSingleton<T>(this T element)
        {
            yield return element;
        }
    }
}
