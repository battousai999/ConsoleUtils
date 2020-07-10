using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Battousai.Utils.EnumerableUtils;

namespace ConsoleUtils.Tests
{
    public class EnumerableUtilsTests
    {
        private readonly string TestElement1 = "test-element-1";
        private readonly string TestElement2 = "test-element-2";
        private readonly string TestElement3 = "test-element-3";

        [Fact]
        public void ReturnsNoElements_IfEnumerableOfCalledWithNoParameters()
        {
            var results = EnumerableUtils.EnumerableOf<string>();

            results.ShouldBeEmpty();
        }

        [Fact]
        public void ReturnsSingleElement_IfEnumerableOfCalledWithSingleParameter()
        {
            var results = EnumerableUtils.EnumerableOf(TestElement1);

            results.Count().ShouldBe(1);
        }

        [Fact]
        public void ReturnsMultipleElements_IfEnumerableOfCalledWithMultipleParameters()
        {
            var results = EnumerableUtils.EnumerableOf(TestElement1, TestElement2);

            results.Count().ShouldBeGreaterThan(1);
        }

        [Fact]
        public void ReturnsSameElements_WhenEnumerableOfCalledWithParameters()
        {
            var results = EnumerableUtils.EnumerableOf(TestElement1, TestElement2, TestElement3);

            var expected = new List<string> { TestElement1, TestElement2, TestElement3 };

            results.ToList().ShouldBe(expected);
        }

        [Fact]
        public void ReturnsSingleElement_WhenToSingletonCalled()
        {
            var results = TestElement1.ToSingleton();

            results.Count().ShouldBe(1);
        }

        [Fact]
        public void ReturnsSameElement_WhenToSingletonCalledWithParameter()
        {
            var results = TestElement2.ToSingleton();

            results.First().ShouldBe(TestElement2);
        }
    }
}
