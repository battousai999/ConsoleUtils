using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class IterateTests
    {
        [Fact]
        public void WhenCalledWithNoParameterAction_ThenActionCalledExpectedNumberOfTimes()
        {
            var counter = 0;
            var iterations = 1000;

            Utils.ConsoleUtils.Iterate(iterations, () => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void WhenCalledWithSingleParameterAction_ThenActionCalledExpectedNumberOfTimes()
        {
            var counter = 0;
            var iterations = 999;

            Utils.ConsoleUtils.Iterate(iterations, _ => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void WhenCalledWithSingleParameterAction_ThenCorrectIterationValuesPassed()
        {
            var expectedValues = new List<int> { 0, 1, 2, 3, 4, 5 };
            var list = new List<int>();

            Utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); });

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }

        [Fact]
        public void WhenCalledWithStartingInterationValue_ThenCorrectIterationValuesPassed()
        {
            var expectedValues = new List<int> { 3, 4, 5, 6 };
            var list = new List<int>();

            Utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); }, 3);

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }

        [Fact]
        public void WhenCalledReturningTotalDuration_ThenReturnsNonZeroDuration()
        {
            var duration = Utils.ConsoleUtils.Iterate(10, () => Thread.Sleep(TimeSpan.FromMilliseconds(100)), false);

            Assert.True(duration > TimeSpan.Zero);
        }

        [Fact]
        public void WhenCalledReturningTotalDuration_ThenReturnsLargerDurationWhenLargerIterations()
        {
            var duration1 = Utils.ConsoleUtils.Iterate(2, () => Thread.Sleep(TimeSpan.FromMilliseconds(100)), false);
            var duration2 = Utils.ConsoleUtils.Iterate(10, () => Thread.Sleep(TimeSpan.FromMilliseconds(100)), false);

            Assert.True(duration2 > duration1);
        }

        [Fact]
        public void WhenCalledReturningAverageDuration_ThenReturnsNonZeroDuration()
        {
            var duration = Utils.ConsoleUtils.Iterate(10, () => Thread.Sleep(TimeSpan.FromMilliseconds(100)), true);

            Assert.True(duration > TimeSpan.Zero);
        }

        [Fact]
        public void WhenCalledReturningAverageDuration_ThenReturnsApproximatelyAverageDuration()
        {
            TimeSpan averageDuration = TimeSpan.Zero;
            int iterations = 10;

            var totalDuration = Utils.ConsoleUtils.MeasureDuration(() =>
            {
                averageDuration = Utils.ConsoleUtils.Iterate(iterations, () => Thread.Sleep(TimeSpan.FromMilliseconds(100)), true);
            });

            var calculatedDurationTicks = averageDuration.Ticks * iterations;
            var deviation = (double) Math.Abs(totalDuration.Ticks - calculatedDurationTicks) / totalDuration.Ticks;

            Assert.True(deviation < 0.05);
        }
    }
}
