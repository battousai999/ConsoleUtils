using System;
using Xunit;
using Shouldly;
using Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class PropertyEqualityComparerTests
    {
        public class TestClass
        {
            public int IntValue { get; set; }
            public string StrValue { get; set; }
        }

        public class ConstructorTests
        {
            [Fact]
            public void ThrowsException_IfConstructorCalledWithoutProjection()
            {
                Should.Throw<ArgumentNullException>(() => { new PropertyEqualityComparer<TestClass, int>(null); });
            }

            [Fact]
            public void DoesNotThrowException_IfConstructorCalledWithoutComparer()
            {
                Should.NotThrow(() => { new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, comparer: null); });
            }
        }

        public class EqualsTests
        {
            [Fact]
            public void ReturnsTrue_IfBothParametersAreNull()
            {
                var subject = new PropertyEqualityComparer<TestClass, int>(x => x.IntValue);

                var results = subject.Equals(null, null);

                results.ShouldBeTrue();
            }

            [Fact]
            public void ReturnsFalse_IfEitherButNotBothParametersAreNull()
            {
                var test = new TestClass();
                var subject = new PropertyEqualityComparer<TestClass, int>(x => x.IntValue);

                var results1 = subject.Equals(null, test);
                var results2 = subject.Equals(test, null);

                results1.ShouldBeFalse();
                results2.ShouldBeFalse();
            }

            [Fact]
            public void ReturnsTrue_IfProjectionsOfBothParametersAreNull()
            {
                var test1 = new TestClass { StrValue = null };
                var test2 = new TestClass { StrValue = null };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                var results = subject.Equals(test1, test2);

                results.ShouldBeTrue();
            }

            [Fact]
            public void ReturnsFalse_IfProjectionsOfEitherButNotBothParametersAreNull()
            {
                var test1 = new TestClass { StrValue = "testing" };
                var test2 = new TestClass { StrValue = null };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                var results1 = subject.Equals(test1, test2);
                var results2 = subject.Equals(test2, test1);

                results1.ShouldBeFalse();
                results2.ShouldBeFalse();
            }

            [Fact]
            public void ReturnsTrue_IfProjectionsOfBothParametersAreEqual()
            {
                var test1 = new TestClass { IntValue = 99 };
                var test2 = new TestClass { IntValue = 99 };
                var subject = new PropertyEqualityComparer<TestClass, int>(x => x.IntValue);

                var results = subject.Equals(test1, test2);

                results.ShouldBeTrue();
            }

            [Fact]
            public void ReturnsFalse_IfProjectionsOfBothParametersAreNotEqual()
            {
                var test1 = new TestClass { IntValue = 99 };
                var test2 = new TestClass { IntValue = 42 };
                var subject = new PropertyEqualityComparer<TestClass, int>(x => x.IntValue);

                var results = subject.Equals(test1, test2);

                results.ShouldBeFalse();
            }

            [Fact]
            public void ReturnsTrue_IfProjectionsOfBothParametersAreEqualWithComparer()
            {
                var test1 = new TestClass { StrValue = "TESTING" };
                var test2 = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, StringComparer.OrdinalIgnoreCase);

                var results = subject.Equals(test1, test2);

                results.ShouldBeTrue();
            }

            [Fact]
            public void ReturnsFalse_IfProjectionsOfBothParametersAreNotEqualWithComparer()
            {
                var test1 = new TestClass { StrValue = "TESTING" };
                var test2 = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, StringComparer.Ordinal);

                var results = subject.Equals(test1, test2);

                results.ShouldBeFalse();
            }

            [Fact]
            public void IsReflexive_IfCalledWithComparer()
            {
                var test = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, StringComparer.Ordinal);

                var results = subject.Equals(test, test);

                results.ShouldBeTrue();
            }

            [Fact]
            public void IsReflexive_IfCalledWithoutComparer()
            {
                var test = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                var results = subject.Equals(test, test);

                results.ShouldBeTrue();
            }
        }

        public class GetHashCodeTests
        {
            [Fact]
            public void ReturnsSameValue_IfProjectionsOfDifferentObjectsAreEqual()
            {
                var test1 = new TestClass { StrValue = "testing" };
                var test2 = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                var results1 = subject.GetHashCode(test1);
                var results2 = subject.GetHashCode(test2);
                var results3 = subject.Equals(test1, test2);

                results3.ShouldBeTrue();
                results1.ShouldBe(results2);
            }

            [Fact]
            public void ReturnsSameValue_IfCalledMultipleSubsequentTimes()
            {
                var test = new TestClass { StrValue = "This is a test." };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                var results1 = subject.GetHashCode(test);
                var results2 = subject.GetHashCode(test);

                results1.ShouldBe(results2);
            }

            [Fact]
            public void ReturnsSameValue_IfProjectionsOfDifferentObjectsAreEqualWithComparer()
            {
                var test1 = new TestClass { StrValue = "TESTING" };
                var test2 = new TestClass { StrValue = "testing" };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, StringComparer.OrdinalIgnoreCase);

                var results1 = subject.GetHashCode(test1);
                var results2 = subject.GetHashCode(test2);
                var results3 = subject.Equals(test1, test2);

                results3.ShouldBeTrue();
                results1.ShouldBe(results2);
            }

            [Fact]
            public void ReturnsSameValue_IfCalledMultipleSubsequentTimesWithComparer()
            {
                var test = new TestClass { StrValue = "This is a test." };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue, StringComparer.Ordinal);

                var results1 = subject.GetHashCode(test);
                var results2 = subject.GetHashCode(test);

                results1.ShouldBe(results2);
            }

            [Fact]
            public void DoesNotThrowException_IfCalledWithNullObject()
            {
                var subject = new PropertyEqualityComparer<TestClass, int>(x => x.IntValue);

                Should.NotThrow(() => { subject.GetHashCode(null); });
            }

            [Fact]
            public void DoesNotThrowException_IfCalledWithObjectWhoseProjectionIsNull()
            {
                var test = new TestClass { StrValue = null };
                var subject = new PropertyEqualityComparer<TestClass, string>(x => x.StrValue);

                Should.NotThrow(() => { subject.GetHashCode(test); });
            }
        }
    }
}
