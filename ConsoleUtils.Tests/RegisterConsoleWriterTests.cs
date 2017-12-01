using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class RegisterConsoleWriterTests
    {
        public RegisterConsoleWriterTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
        }

        [Fact]
        public void RegisterConsoleWriter_should_allow_registered_reader_to_be_invoked()
        {
            var hasCalledConsoleWriter = false;

            utils.ConsoleUtils.RegisterConsoleWriter(_ =>
            {
                hasCalledConsoleWriter = true;
            });

            utils.ConsoleUtils.Log("testing...");

            Assert.True(hasCalledConsoleWriter);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_true_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + Environment.NewLine + str2 + Environment.NewLine;

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.Log(str1);
            utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_false_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + str2;

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(str1);
            utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }
    }
}
