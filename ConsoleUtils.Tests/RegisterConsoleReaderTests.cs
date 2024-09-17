using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RegisterConsoleReaderTests
    {
        public RegisterConsoleReaderTests()
        {
            Utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void WhenRegistered_ThenUsesReader()
        {
            var hasCalledConsoleReader = false;

            Utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledConsoleReader = true;
                return "";
            });

            Utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

            Assert.True(hasCalledConsoleReader);
        }
    }
}
