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
    public class RunLoggingExceptionTests
    {
        public RunLoggingExceptionTests()
        {
            Utils.ConsoleUtils.RegisterConsoleWriter(null);
            Utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void WhenCalled_ThenConsumesExceptions()
        {
            Utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                throw new InvalidOperationException("Test exception");
            });
        }

        [Fact]
        public void WhenCalled_ThenPerformsAction()
        {
            var value = 0;

            Utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                value = 99;
            });

            Assert.Equal(99, value);
        }

        [Fact]
        public void WhenActionThrowsException_ThenExceptionIsLogged()
        {
            var log = "";
            var exceptionMessage = "Test exception";

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            Utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });

            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void WhenFlaggedToPause_ThenConsoleReadlineIsCaptured()
        {
            var hasCalledReadline = false;

            Utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            Utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

            Assert.True(hasCalledReadline);
        }

        [Fact]
        public void WhenNotFlaggedToPause_ThenConsoleReadlineIsNotCaptured()
        {
            var hasCalledReadline = false;

            Utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            Utils.ConsoleUtils.RunLoggingExceptions(() => { }, false);

            Assert.False(hasCalledReadline);
        }

        [Fact]
        public void WhenFlaggedToDisplayDuration_ThenDurationIsLogged()
        {
            string log = "";

            Utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            Utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, true);

            Assert.Contains("Finished in", log);
        }

        [Fact]
        public void WhenNotFlaggedToDisplayDuration_ThenDurationIsNotLogged()
        {
            string log = "";

            Utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            Utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, false);

            Assert.DoesNotContain("Finished in", log);
        }

    }
}
