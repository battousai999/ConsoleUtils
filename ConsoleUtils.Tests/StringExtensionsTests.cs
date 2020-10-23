using System;
using System.Text;
using Xunit;
using Shouldly;
using Battousai.Utils.StringUtils;
using System.IO;

namespace ConsoleUtils.Tests
{
    public class StringExtensionsTests
    {
        public class BracketTests
        {
            private const string TestString = "test-string";

            [Fact]
            public void ReturnsNull_IfCalledWithNullText()
            {
                string value = null;
                var results = value.Bracket("(", ")");

                results.ShouldBeNull();
            }

            [Fact]
            public void ReturnsEmptyString_IfCalledWithEmptyText()
            {
                var results = String.Empty.Bracket("(", ")");

                results.ShouldBe(String.Empty);
            }

            [Fact]
            public void ReturnsBracketedText_IfCalledWithNonEmptyText()
            {
                var results = TestString.Bracket("(", ")");

                results.ShouldBe($"({TestString})");
            }

            [Fact]
            public void ReturnsOriginalText_IfCalledWithEmptyBrackets()
            {
                var results = TestString.Bracket("", "");

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsBracketedEmptyString_IfCalledWithEmptyTextAndBracketIfEmptyIsTrue()
            {
                var results = String.Empty.Bracket("(", ")", true);

                results.ShouldBe("()");
            }

            [Fact]
            public void ReturnsBracketedEmptyString_IfCalledWithNullTextAndBracketIfEmptyIsTrue()
            {
                string value = null;
                var results = value.Bracket("(", ")", true);

                results.ShouldBe("()");
            }
        }

        public class PadRightWithEllipsisTests
        {
            private const string TestString = "test-string";
            private readonly int Padding = 10;

            [Fact]
            public void ReturnsCorrectlySizedStringOfSpaces_IfCalledWithNullText()
            {
                string value = null;
                var results = value.PadRightWithEllipsis(Padding);

                results.ShouldBe(new String(' ', Padding));
            }

            [Fact]
            public void ReturnsCorrectlySizedStringOfSpaces_IfCalledWithEmptyText()
            {
                var results = String.Empty.PadRightWithEllipsis(Padding);

                results.ShouldBe(new String(' ', Padding));
            }

            [Fact]
            public void ReturnsEmptyString_IfCalledWithSizeOfZero()
            {
                var results = TestString.PadRightWithEllipsis(0);

                results.ShouldBe(String.Empty);
            }

            [Fact]
            public void ReturnsPaddedTextWithoutEllipsis_IfCalledWithTextOfSizeSmallerThanPassedSize()
            {
                var results = TestString.PadRightWithEllipsis(Padding + 20);

                results.ShouldBe(TestString.PadRight(Padding + 20));
            }

            [Fact]
            public void ReturnsTextWithoutEllipsis_IfCalledWithTextOfSameSizeAsPassedSize()
            {
                var results = TestString.PadRightWithEllipsis(TestString.Length);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTruncatedTextWithoutEllipsis_IfCalledWithPassedSizeSmallerThanLengthOfEllipsis()
            {
                var results = TestString.PadRightWithEllipsis(2);

                results.ShouldBe(TestString.Substring(0, 2));
            }

            [Fact]
            public void ReturnsTruncatedTextWithEllipsis_IfCalledWithTextOfSizeGreaterThanPassedSize()
            {
                var results = TestString.PadRightWithEllipsis(Padding);

                results.ShouldBe($"{TestString.Substring(0, Padding - 3)}...");
            }

            [Fact]
            public void ReturnsTruncatedTextWithCustomEllipsis_IfCalledWithTextOfSizeGreaterThanPassedSizeAndCustomEllipsis()
            {
                var ellipsis = "****";
                var results = TestString.PadRightWithEllipsis(Padding, ellipsis);

                results.ShouldBe($"{TestString.Substring(0, Padding - ellipsis.Length)}{ellipsis}");
            }
        }

        public class PadLeftWithEllipsisTests
        {
            private const string TestString = "test-string";
            private readonly int Padding = 10;

            [Fact]
            public void ReturnsCorrectlySizedStringOfSpaces_IfCalledWithNullText()
            {
                string value = null;
                var results = value.PadLeftWithEllipsis(Padding);

                results.ShouldBe(new String(' ', Padding));
            }

            [Fact]
            public void ReturnsCorrectlySizedStringOfSpaces_IfCalledWithEmptyText()
            {
                var results = String.Empty.PadLeftWithEllipsis(Padding);

                results.ShouldBe(new String(' ', Padding));
            }

            [Fact]
            public void ReturnsEmptyString_IfCalledWithSizeOfZero()
            {
                var results = TestString.PadLeftWithEllipsis(0);

                results.ShouldBe(String.Empty);
            }

            [Fact]
            public void ReturnsPaddedTextWithoutEllipsis_IfCalledWithTextOfSizeSmallerThanPassedSize()
            {
                var results = TestString.PadLeftWithEllipsis(Padding + 20);

                results.ShouldBe(TestString.PadLeft(Padding + 20));
            }

            [Fact]
            public void ReturnsTextWithoutEllipsis_IfCalledWithTextOfSameSizeAsPassedSize()
            {
                var results = TestString.PadLeftWithEllipsis(TestString.Length);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTruncatedTextWithoutEllipsis_IfCalledWithPassedSizeSmallerThanLengthOfEllipsis()
            {
                var results = TestString.PadLeftWithEllipsis(2);

                results.ShouldBe(TestString.Substring(0, 2));
            }

            [Fact]
            public void ReturnsTruncatedTextWithEllipsis_IfCalledWithTextOfSizeGreaterThanPassedSize()
            {
                var results = TestString.PadLeftWithEllipsis(Padding);

                results.ShouldBe($"{TestString.Substring(0, Padding - 3)}...");
            }

            [Fact]
            public void ReturnsTruncatedTextWithCustomEllipsis_IfCalledWithTextOfSizeGreaterThanPassedSizeAndCustomEllipsis()
            {
                var ellipsis = "****";
                var results = TestString.PadLeftWithEllipsis(Padding, ellipsis);

                results.ShouldBe($"{TestString.Substring(0, Padding - ellipsis.Length)}{ellipsis}");
            }
        }

        public class EnsureEndsWithTests
        {
            private const string TestString = "test-string";
            private const string Ending = "test-ending";

            [Fact]
            public void ReturnsEnding_IfCalledOnEmptyString()
            {
                var results = String.Empty.EnsureEndsWith(Ending);

                results.ShouldBe(Ending);
            }

            [Fact]
            public void ThrowsException_IfCalledOnNull()
            {
                string value = null;
                Should.Throw<ArgumentNullException>(() => value.EnsureEndsWith(Ending));
            }

            [Fact]
            public void ReturnsWithEnding_IfCalledOnNonEmptyString()
            {
                var results = TestString.EnsureEndsWith(Ending);

                results.ShouldBe(TestString + Ending);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringEndingWithEnding()
            {
                var value = TestString + Ending;
                var results = value.EnsureEndsWith(Ending);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsWithEnding_IfCalledOnStringEndingWithWrongCasedEnding()
            {
                var value = TestString + Ending.ToUpper();
                var results = value.EnsureEndsWith(Ending);

                results.ShouldBe(value + Ending);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithComparerAndOnStringEndingWithWrongCasedEnding()
            {
                var value = TestString + Ending.ToUpper();
                var results = value.EnsureEndsWith(Ending, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithEmptyEnding()
            {
                var results = TestString.EnsureEndsWith(String.Empty);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ThrowsException_IfCalledWithNullEnding()
            {
                Should.Throw<ArgumentNullException>(() => TestString.EnsureEndsWith(null));
            }
        }

        public class EnsureStartsWithTests
        {
            private const string TestString = "test-string";
            private const string Beginning = "test-beginning";

            [Fact]
            public void ReturnsBeginning_IfCalledOnEmptyString()
            {
                var results = String.Empty.EnsureStartsWith(Beginning);

                results.ShouldBe(Beginning);
            }

            [Fact]
            public void ThrowsException_IfCalledOnNull()
            {
                string value = null;
                Should.Throw<ArgumentNullException>(() => value.EnsureStartsWith(Beginning));
            }

            [Fact]
            public void ReturnsWithBeginning_IfCalledOnNonEmptyString()
            {
                var results = TestString.EnsureStartsWith(Beginning);

                results.ShouldBe(Beginning + TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringBeginningWithBeginning()
            {
                var value = Beginning + TestString;
                var results = value.EnsureStartsWith(Beginning);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsWithBeginning_IfCalledOnStringBeginningWithWrongCasedBeginning()
            {
                var value = Beginning.ToUpper() + TestString;
                var results = value.EnsureStartsWith(Beginning);

                results.ShouldBe(Beginning + value);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithComparerAndOnStringBeginningWithWrongCasedEnding()
            {
                var value = Beginning.ToUpper() + TestString;
                var results = value.EnsureStartsWith(Beginning, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithEmptyBeginning()
            {
                var results = TestString.EnsureStartsWith(String.Empty);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ThrowsException_IfCalledWithNullBeginning()
            {
                Should.Throw<ArgumentNullException>(() => TestString.EnsureStartsWith(null));
            }
        }

        public class RemoveTrailingTests
        {
            private const string TestString = "test-string";
            private const string Ending = "test-ending";

            [Fact]
            public void ReturnsTextWithEndingRemoved_IfCalledOnStringEndingWithEnding()
            {
                var value = TestString + Ending;
                var results = value.RemoveTrailing(Ending);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringNotEndingWithEnding()
            {
                var results = TestString.RemoveTrailing(Ending);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsNull_IfCalledOnNull()
            {
                string value = null;
                var results = value.RemoveTrailing(Ending);

                results.ShouldBeNull();
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithNullEnding()
            {
                var results = TestString.RemoveTrailing(null);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithEmptyEnding()
            {
                var results = TestString.RemoveTrailing(String.Empty);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringEndingWithWrongCaseEnding()
            {
                var value = TestString + Ending.ToUpper();
                var results = value.RemoveTrailing(Ending);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsTextWithEndingRemoved_IfCalledWithComparerAndOnStringEndingWithWrongCaseEnding()
            {
                var value = TestString + Ending.ToUpper();
                var results = value.RemoveTrailing(Ending, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(TestString);
            }
        }

        public class RemoveLeadingTests
        {
            private const string TestString = "test-string";
            private const string Beginning = "test-beginning";

            [Fact]
            public void ReturnsTextWithBeginningRemoved_IfCalledOnStringBeginningWithBeginning()
            {
                var value = Beginning + TestString;
                var results = value.RemoveLeading(Beginning);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringNotBeginningWithBeginning()
            {
                var results = TestString.RemoveLeading(Beginning);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsNull_IfCalledOnNull()
            {
                string value = null;
                var results = value.RemoveLeading(Beginning);

                results.ShouldBeNull();
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithNullBeginning()
            {
                var results = TestString.RemoveLeading(null);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithEmptyBeginning()
            {
                var results = TestString.RemoveLeading(String.Empty);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringBeginningWithWrongCaseBeginning()
            {
                var value = Beginning.ToUpper() + TestString;
                var results = value.RemoveLeading(Beginning);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsTextWithBeginningRemoved_IfCalledWithComparerAndOnStringBeginningWithWrongCaseBeginning()
            {
                var value = Beginning.ToUpper() + TestString;
                var results = value.RemoveLeading(Beginning, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(TestString);
            }
        }

        public class RemoveSurroundingTests
        {
            private const string TestString = "test-string";
            private const string Beginning = "test-beginning";
            private const string Ending = "test-ending";
            private const string Surrounding = "test-surrounding";

            [Fact]
            public void ReturnsTextWithSurroundingRemoved_IfCalledOnStringHavingSurroundingOnBothEnds()
            {
                var value = Surrounding + TestString + Surrounding;
                var results = value.RemoveSurrounding(Surrounding);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTextWithBeginningRemoved_IfCalledOnStringWithBeginningMatchingSurrounding()
            {
                var value = Surrounding + TestString;
                var results = value.RemoveSurrounding(Surrounding);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTextWithEndingRemoved_IfCalledOnStringWithEndingMatchingSurrounding()
            {
                var value = TestString + Surrounding;
                var results = value.RemoveSurrounding(Surrounding);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledOnStringWithoutSurrounding()
            {
                var results = TestString.RemoveSurrounding(Surrounding);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsPassedValue_IfCalledWithNullSurrounding()
            {
                var results = TestString.RemoveSurrounding(null);

                results.ShouldBe(TestString);
            }

            [Theory]
            [InlineData("OUTER-middle-outer", "outer", "OUTER-middle-")]
            [InlineData("outer-middle-OUTER", "outer", "-middle-OUTER")]
            [InlineData("OUTER-middle-OUTER", "outer", "OUTER-middle-OUTER")]
            public void ReturnsCorrectValue_IfCalledWithComparerAndOnStringHavingWrongCaseSurroundingOnEitherSide(string value, string surrounding, string expected)
            {
                var results = value.RemoveSurrounding(surrounding);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("OUTER-middle-outer", "outer", "-middle-")]
            [InlineData("outer-middle-OUTER", "outer", "-middle-")]
            [InlineData("OUTER-middle-OUTER", "outer", "-middle-")]
            public void ReturnsTextWithSurroundingRemoved_IfCalledWithComparerAndOnStringHavingWrongCaseSurroundingOnEitherSide(string value, string surrounding, string expected)
            {
                var results = value.RemoveSurrounding(surrounding, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(expected);
            }

            [Fact]
            public void ReturnsNull_IfCalledOnNull()
            {
                string value = null;
                var results = value.RemoveSurrounding(Surrounding);

                results.ShouldBeNull();
            }

            [Fact]
            public void ReturnsTextWithSurroundRemoved_IfCalledOnStringHavingBeggingingAndEnding()
            {
                var value = Beginning + TestString + Ending;
                var results = value.RemoveSurrounding(Beginning, Ending);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTextWithBeginningRemoved_IfCalledOnStringWithBeginningMatchingBeginning()
            {
                var value = Beginning + TestString;
                var results = value.RemoveSurrounding(Beginning, Ending);

                results.ShouldBe(TestString);
            }

            [Fact]
            public void ReturnsTextWithEndingRemoved_IfCalledOnStringWithEndingMatchingEnding()
            {
                var value = TestString + Ending;
                var results = value.RemoveSurrounding(Beginning, Ending);

                results.ShouldBe(TestString);
            }

            [Theory]
            [InlineData("value-ending", "ending", "value-")]
            [InlineData("value", "ending", "value")]
            public void ReturnsCorrectValue_IfCalledOnStringWithEmptyBeginning(string value, string ending, string expected)
            {
                var results = value.RemoveSurrounding(String.Empty, ending);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("beginning-value", "beginning", "-value")]
            [InlineData("value", "beginning", "value")]
            public void ReturnsCorrectValue_IfCalledOnStringWithEmptyEnding(string value, string beginning, string expected)
            {
                var results = value.RemoveSurrounding(beginning, String.Empty);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("value-ending", "ending", "value-")]
            [InlineData("value", "ending", "value")]
            public void ReturnsCorrectValue_IfCalledWithNullBeginning(string value, string ending, string expected)
            {
                var results = value.RemoveSurrounding(null, ending);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("beginning-value", "beginning", "-value")]
            [InlineData("value", "beginning", "value")]
            public void ReturnsCorrectValue_IfCalledWithNullEnding(string value, string beginning, string expected)
            {
                var results = value.RemoveSurrounding(beginning, null);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("beginning-middle-ENDING", "beginning", "ending", "-middle-ENDING")]
            [InlineData("BEGINNING-middle-ending", "beginning", "ending", "BEGINNING-middle-")]
            [InlineData("BEGINNING-middle-ENDING", "beginning", "ending", "BEGINNING-middle-ENDING")]
            public void ReturnsCorrectValue_IfCalledOnStringHavingWrongCaseBeginningOrEnding(string value, string beginning, string ending, string expected)
            {
                var results = value.RemoveSurrounding(beginning, ending);

                results.ShouldBe(expected);
            }

            [Theory]
            [InlineData("beginning-middle-ENDING", "beginning", "ending", "-middle-")]
            [InlineData("BEGINNING-middle-ending", "beginning", "ending", "-middle-")]
            [InlineData("BEGINNING-middle-ENDING", "beginning", "ending", "-middle-")]
            public void ReturnsTextWithSurroundingRemoved_IfCalledWithComparerAndOnStringHavingWrongCaseBeginningOrEnding(string value, string beginning, string ending, string expected)
            {
                var results = value.RemoveSurrounding(beginning, ending, StringComparison.OrdinalIgnoreCase);

                results.ShouldBe(expected);
            }
        }

        public class ToStreamTests
        {
            [Fact]
            public void ReturnsStreamWithCorrectContent_IfCalledWithNonEmptyString()
            {
                var value = "test-string";
                var stream = value.ToStream();
                var results = GetStreamContents(stream);

                results.ShouldBe(value);
            }

            private string GetStreamContents(Stream stream, Encoding encoding = null)
            {
                Func<StreamReader> createReader = () =>
                {
                    if (encoding == null)
                        return new StreamReader(stream);
                    else
                        return new StreamReader(stream, encoding);
                };

                using (var reader = createReader())
                {
                    return reader.ReadToEnd();
                }
            }

            [Fact]
            public void ReturnsStreamWithCorrectContent_IfCalledWithEncoderAndNonEmptyString()
            {
                var value = "test-string";
                var encoder = Encoding.Unicode;
                var stream = value.ToStream(encoder);
                var results = GetStreamContents(stream, encoder);

                results.ShouldBe(value);
            }

            [Fact]
            public void ReturnsEmptyStream_IfCalledWithEmptyString()
            {
                var stream = String.Empty.ToStream();
                var results = GetStreamContents(stream);

                results.ShouldBe(String.Empty);
            }

            [Fact]
            public void ReturnsEmptyStream_IfCalledWithNull()
            {
                string value = null;
                var stream = value.ToStream();
                var results = GetStreamContents(stream);

                results.ShouldBe(String.Empty);
            }
        }
    }
}
