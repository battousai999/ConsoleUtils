using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RunLoggingExceptionTests
    {
        public RunLoggingExceptionTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
            utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void WhenCalled_ThenConsumesExceptions()
        {
            utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                throw new InvalidOperationException("Test exception");
            });
        }

        [Fact]
        public void WhenCalled_ThenPerformsAction()
        {
            var value = 0;

            utils.ConsoleUtils.RunLoggingExceptions(() =>
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

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.RunLoggingExceptions(() =>
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

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

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

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, false);

            Assert.False(hasCalledReadline);
        }

        [Fact]
        public void WhenFlaggedToDisplayDuration_ThenDurationIsLogged()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, true);

            Assert.Contains("Finished in", log);
        }

        [Fact]
        public void WhenNotFlaggedToDisplayDuration_ThenDurationIsNotLogged()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, false);

            Assert.DoesNotContain("Finished in", log);
        }

    }
}
