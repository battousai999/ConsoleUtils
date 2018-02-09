using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battousai.Utils
{
    public class AverageDurationAggregator : Aggregator<TimeSpan>
    {
        public AverageDurationAggregator() : base(AggregationFuncFactory(), TimeSpan.Zero)
        {
        }

        private static Func<TimeSpan, TimeSpan, TimeSpan> AggregationFuncFactory()
        {
            int counter = 1;

            return (acc, val) =>
            {
                var newValue = acc.Ticks + ((val.Ticks - acc.Ticks) / counter);

                counter++;

                return TimeSpan.FromTicks(newValue);
            };
        }
    }
}
