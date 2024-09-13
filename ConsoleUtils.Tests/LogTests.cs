using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class LogTests
    {
        public LogTests()
        {
            Utils.ConsoleUtils.RegisterConsoleWriter(null);
        }

        [Fact]
        public void WhenValueIsLogged_ThenConsoleContainsValue()
        {
            var log = "";
            var testString = "This is a test string.";

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            Utils.ConsoleUtils.Log(testString);

            Assert.Equal(testString, log);
        }

        [Fact]
        public void WhenCalledWithNoParameters_ThenOnlyNewlineIsLogged()
        {
            var log = "";

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            Utils.ConsoleUtils.Log();

            Assert.Equal(Environment.NewLine, log);
        }

        [Fact]
        public void WhenCalledWithStringFormatParameters_ThenFormattedValueIsLogged()
        {
            var log = "";
            var testString = "This is a test string with {0} {1}.";
            var parameters = new List<string> { "some", "parameters" };

            Utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            Utils.ConsoleUtils.Log(testString, parameters[0], parameters[1]);

            Assert.Equal(String.Format(testString, parameters[0], parameters[1]), log);
        }
    }
}
