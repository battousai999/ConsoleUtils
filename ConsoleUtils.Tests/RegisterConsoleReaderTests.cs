using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RegisterConsoleReaderTests
    {
        public RegisterConsoleReaderTests()
        {
            utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void WhenRegistered_ThenUsesReader()
        {
            var hasCalledConsoleReader = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledConsoleReader = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

            Assert.True(hasCalledConsoleReader);
        }
    }
}
