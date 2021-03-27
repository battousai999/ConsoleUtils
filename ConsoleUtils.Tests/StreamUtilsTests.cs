using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Shouldly;
using System.IO;
using Battousai.Utils.Utils;

namespace ConsoleUtils.Tests
{
    public class StreamUtilsTests
    {
        public class CopyStreamTests : IDisposable
        {
            private readonly MemoryStream inputStream;
            private readonly MemoryStream outputStream;
            private readonly string testContents;
            private bool isDisposed;

            public CopyStreamTests()
            {
                testContents = String.Concat(RepeatString("This is a test.").Take(1000));

                inputStream = new MemoryStream(Encoding.UTF8.GetBytes(testContents));
                outputStream = new MemoryStream();
            }

            private IEnumerable<char> RepeatString(string text)
            {
                IEnumerable<IEnumerable<char>> helper()
                {
                    while (true)
                    {
                        yield return text;
                    }
                }

                return helper().SelectMany(x => x);
            }

            [Fact]
            public void ThrowsException_IfCalledWithNullSource()
            {
                Should.Throw<ArgumentNullException>(() =>
                {
                    StreamUtils.CopyStream(null, outputStream);
                });
            }

            [Fact]
            public void ThrowsException_IfCalledWithNullDestination()
            {
                Should.Throw<ArgumentNullException>(() =>
                {
                    StreamUtils.CopyStream(inputStream, null);
                });
            }

            [Fact]
            public void ThrowsExcption_IfCalledWithZeroBufferSize()
            {
                Should.Throw<ArgumentException>(() =>
                {
                    StreamUtils.CopyStream(inputStream, outputStream, 0);
                });
            }

            [Fact]
            public void ThrowsException_IfCalledWithNegativeBufferSize()
            {
                Should.Throw<ArgumentException>(() =>
                {
                    StreamUtils.CopyStream(inputStream, outputStream, -1);
                });
            }

            [Fact]
            public void DoesNotCopyAnything_IfCalledWithEmptySource()
            {
                using (var emptyStream = new MemoryStream())
                {
                    StreamUtils.CopyStream(emptyStream, outputStream);

                    outputStream.ToArray().ShouldBeEmpty();
                }
            }

            [Fact]
            public void CopiesSourceContents_IfCalledWithNonEmptySource()
            {
                StreamUtils.CopyStream(inputStream, outputStream);

                Encoding.UTF8.GetString(outputStream.ToArray()).ShouldBe(testContents);
            }

            [Fact]
            public void CopiesSourceContents_IfCalledWithBufferSizeSmallerThanSourceLength()
            {
                StreamUtils.CopyStream(inputStream, outputStream, testContents.Length / 2);

                Encoding.UTF8.GetString(outputStream.ToArray()).ShouldBe(testContents);
            }

            [Fact]
            public void CopiesSourceContents_IfCalledWithBufferSizeLargerThanSourceContent()
            {
                StreamUtils.CopyStream(inputStream, outputStream, testContents.Length * 2);

                Encoding.UTF8.GetString(outputStream.ToArray()).ShouldBe(testContents);
            }

            [Fact]
            public void CopiesSourceContents_IfCalledWithBufferSizeEqualToSourceContentLength()
            {
                StreamUtils.CopyStream(inputStream, outputStream, testContents.Length);

                Encoding.UTF8.GetString(outputStream.ToArray()).ShouldBe(testContents);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!isDisposed)
                {
                    if (disposing)
                    {
                        inputStream?.Dispose();
                        outputStream?.Dispose();

                        isDisposed = true;
                    }
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
