using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class MeasureDurationTests
    {
        [Fact]
        public void WhenCalled_ThenReturnsDuration()
        {
            var waitSpan = TimeSpan.FromMilliseconds(100);

            var duration = utils.ConsoleUtils.MeasureDuration(() =>
            {
                System.Threading.Thread.Sleep(waitSpan);
            });

            Assert.True(duration >= waitSpan);
        }

        [Fact]
        public void WhenCalledWithLongRunningAction_ThenReturnsLargeDuration()
        {
            var waitSpan = TimeSpan.FromSeconds(1);

            var duration = utils.ConsoleUtils.MeasureDuration(() =>
            {
                System.Threading.Thread.Sleep(waitSpan);
            });

            Assert.True(duration >= waitSpan);
        }
    }
}
