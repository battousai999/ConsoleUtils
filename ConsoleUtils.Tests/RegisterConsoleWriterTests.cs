using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RegisterConsoleWriterTests
    {
        public RegisterConsoleWriterTests()
        {
            Utils.ConsoleUtils.RegisterConsoleWriter(null);
        }

        [Fact]
        public void RegisterConsoleWriter_should_allow_registered_reader_to_be_invoked()
        {
            var hasCalledConsoleWriter = false;

            Utils.ConsoleUtils.RegisterConsoleWriter(_ =>
            {
                hasCalledConsoleWriter = true;
            });

            Utils.ConsoleUtils.Log("testing...");

            Assert.True(hasCalledConsoleWriter);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_true_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + Environment.NewLine + str2 + Environment.NewLine;

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            Utils.ConsoleUtils.Log(str1);
            Utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_false_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + str2;

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            Utils.ConsoleUtils.Log(str1);
            Utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }
    }
}
