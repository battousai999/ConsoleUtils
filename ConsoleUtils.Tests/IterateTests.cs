using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class IterateTests
    {
        [Fact]
        public void WhenCalledWithNoParameterAction_ThenActionCalledExpectedNumberOfTimes()
        {
            var counter = 0;
            var iterations = 1000;

            utils.ConsoleUtils.Iterate(iterations, () => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void WhenCalledWithSingleParameterAction_ThenActionCalledExpectedNumberOfTimes()
        {
            var counter = 0;
            var iterations = 999;

            utils.ConsoleUtils.Iterate(iterations, _ => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void WhenCalledWithSingleParameterAction_ThenCorrectIterationValuesPassed()
        {
            var expectedValues = new List<int> { 0, 1, 2, 3, 4, 5 };
            var list = new List<int>();

            utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); });

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }

        [Fact]
        public void WhenCalledWithStartingInterationValue_ThenCorrectIterationValuesPassed()
        {
            var expectedValues = new List<int> { 3, 4, 5, 6 };
            var list = new List<int>();

            utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); }, 3);

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }
    }
}
