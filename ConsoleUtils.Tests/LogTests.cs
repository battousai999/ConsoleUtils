using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class LogTests
    {
        public LogTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
        }

        [Fact]
        public void WhenValueIsLogged_ThenConsoleContainsValue()
        {
            var log = "";
            var testString = "This is a test string.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(testString);

            Assert.Equal(testString, log);
        }

        [Fact]
        public void WhenCalledWithNoParameters_ThenOnlyNewlineIsLogged()
        {
            var log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.Log();

            Assert.Equal(Environment.NewLine, log);
        }

        [Fact]
        public void WhenCalledWithStringFormatParameters_ThenFormattedValueIsLogged()
        {
            var log = "";
            var testString = "This is a test string with {0} {1}.";
            var parameters = new List<string> { "some", "parameters" };

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(testString, parameters[0], parameters[1]);

            Assert.Equal(String.Format(testString, parameters[0], parameters[1]), log);
        }
    }
}
