using System;
using System.Collections.Generic;

namespace Battousai.Utils
{
    public class PropertyEqualityComparer<TBase, TProp> : IEqualityComparer<TBase> where TProp : IEquatable<TProp>
    {
        private readonly Func<TBase, TProp> projection;
        private readonly IEqualityComparer<TProp> comparer;

        public PropertyEqualityComparer(Func<TBase, TProp> projection)
        {
            if (projection == null)
                throw new ArgumentNullException(nameof(projection));

            this.projection = projection;
            this.comparer = null;
        }

        public PropertyEqualityComparer(Func<TBase, TProp> projection, IEqualityComparer<TProp> comparer)
        {
            if (projection == null)
                throw new ArgumentNullException(nameof(projection));

            this.projection = projection;
            this.comparer = comparer;
        }

        public bool Equals(TBase x, TBase y)
        {
            if (x == null && y == null)
                return true;

            if (x == null || y == null)
                return false;

            var xValue = projection(x);
            var yValue = projection(y);

            if (xValue == null && yValue == null)
                return true;

            if (xValue == null || yValue == null)
                return false;

            if (comparer == null)
                return xValue.Equals(yValue);
            else
                return comparer.Equals(xValue, yValue);
        }

        public int GetHashCode(TBase obj)
        {
            if (obj == null)
                return 0;

            var value = projection(obj);

            if (value == null)
                return 0;

            if (comparer == null)
                return value.GetHashCode();
            else
                return comparer.GetHashCode(value);
        }
    }
}
