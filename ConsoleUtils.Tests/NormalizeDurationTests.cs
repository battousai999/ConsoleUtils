using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class NormalizeDurationTests
    {
        [Fact]
        public void WhenDurationIsSubSecond_ThenDisplaysInMilliseconds()
        {
            var duration = TimeSpan.FromMilliseconds(500.1);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalMilliseconds.ToString("0 ms"), value);
        }

        [Fact]
        public void WhenDurationIsSubMinute_ThenDisplaysInSeconds()
        {
            var duration = TimeSpan.FromSeconds(20.2);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalSeconds.ToString("0.00 seconds"), value);
        }

        [Fact]
        public void WhenDurationIsSubHour_ThenDisplaysInMinutes()
        {
            var duration = TimeSpan.FromMinutes(30.3);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalMinutes.ToString("0.00 minutes"), value);
        }

        [Fact]
        public void WhenDurationIsSubDay_ThenDisplaysInHours()
        {
            var duration = TimeSpan.FromHours(2.4);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalHours.ToString("0.00 hours"), value);
        }

        [Fact]
        public void WhenDurationIsLongerThanDay_ThenDisplayInDays()
        {
            var duration = TimeSpan.FromDays(9.5);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalDays.ToString("0.00 days"), value);
        }
    }
}
