using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RunLoggingExceptionsAsyncTests
    {
        public RunLoggingExceptionsAsyncTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
            utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void WhenCalled_ThenConsumesExceptions()
        {
            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                throw new InvalidOperationException("Test exception");
            });
        }

        [Fact]
        public void WhenCalled_ThenPerformsAction()
        {
            var value = 0;

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                value = 99;
            });

            Assert.Equal(99, value);
        }

        [Fact]
        public void WhenActionThrowsException_ThenExceptionIsLogged()
        {
            var log = "";
            var exceptionMessage = "Test exception";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                throw new InvalidOperationException(exceptionMessage);
            });

            Assert.DoesNotContain("AggregateException", log);
            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void WhenFlaggedToPause_ThenConsoleReadlineIsCaptured()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, true);

            Assert.True(hasCalledReadline);
        }

        [Fact]
        public void WhenNotFlaggedToPause_ThenConsoleReadlineIsNotCaptured()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false);

            Assert.False(hasCalledReadline);
        }

        [Fact]
        public void WhenFlaggedToDisplayDuration_ThenDurationIsLogged()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false, true);

            Assert.Contains("Finished in", log);
        }

        [Fact]
        public void WhenNotFlaggedToDisplayDuration_ThenDurationIsNotLogged()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false, false);

            Assert.DoesNotContain("Finished in", log);
        }
    }
}
