using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using Battousai.Utils.StringUtils;

namespace ConsoleUtils.Tests
{
    public class StringExtensionsTests
    {
        public class BracketTests
        {
            private readonly string TestString = "test-string";

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
            private readonly string TestString = "test-string";
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
            private readonly string TestString = "test-string";
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
        }

        public class EnsureStartsWithTests
        {
        }

        public class RemoveTrailingTests
        {
        }

        public class RemoveLeadingTests
        {
        }

        public class RemoveSurroundingTests
        {
        }

        public class ToStreamTests
        {
        }
    }
}
