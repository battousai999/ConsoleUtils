using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class LogExceptionTests
    {
        public LogExceptionTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
        }

        [Fact]
        public void WhenCalled_ThenTypeAndExceptionMessageIsLogged()
        {
            var log = "";
            var exceptionMessage = "This is a test exception message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void WhenCalledWithMessage_ThenMessageIsLogged()
        {
            var log = "";
            var errorMessage = "This is a test error message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                throw new InvalidOperationException("");
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(errorMessage, ex);
            }

            Assert.Contains(errorMessage, log);
        }

        [Fact]
        public void WhenInnerExceptionIsIncluded_ThenInnerExceptionIsLogged()
        {
            var log = "";
            var exceptionMessage = "This is a test exception message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                try
                {
                    throw new ApplicationException(exceptionMessage);
                }
                catch (Exception innerEx)
                {
                    throw new InvalidOperationException("Some outer exception message.", innerEx);
                }
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains("ApplicationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void WhenCalledWithExceptionData_ThenDataIsLogged()
        {
            var log = "";
            var dataKey = "exception-data-key";
            var dataValue = "exception-data-value";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                var exception = new ApplicationException("This is a test exception message.");

                exception.Data[dataKey] = dataValue;

                throw exception;
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains(dataKey, log);
            Assert.Contains(dataValue, log);
        }

        [Fact]
        public void WhenCalledWithoutExceptionData_ThenDoesNotLogExceptionDataHeader()
        {
            var log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                throw new ApplicationException("This is a test exception message.");
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.DoesNotContain("EXCEPTION DATA:", log);
        }

        [Fact]
        public void WhenInnerExceptionHavingDataIsIncluded_ThenDataIsLogged()
        {
            var log = "";
            var dataKey = "exception-data-key";
            var dataValue = "exception-data-value";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                try
                {
                    var exception = new ApplicationException("This is a test exception message.");

                    exception.Data[dataKey] = dataValue;

                    throw exception;
                }
                catch (Exception innerEx)
                {
                    throw new InvalidOperationException("Some outer exception message.", innerEx);
                }
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains(dataKey, log);
            Assert.Contains(dataValue, log);
        }
    }
}
