using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battousai.Utils
{
    public class Aggregator<T>
    {
        private readonly Func<T, T, T> aggregation;
        private T currentValue;

        public Aggregator(Func<T, T, T> aggregation, T seed = default(T))
        {
            this.aggregation = aggregation;
            currentValue = seed;
        }

        public T CurrentValue => currentValue;

        public void Add(T value)
        {
            currentValue = aggregation(currentValue, value);
        }
    }
}
