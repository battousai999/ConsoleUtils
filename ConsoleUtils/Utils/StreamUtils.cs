using System;
using System.IO;

namespace Battousai.Utils.Utils
{
    public static class StreamUtils
    {
        /// <summary>
        /// Copies content from the 'source' stream to the 'destination' stream.
        /// </summary>
        /// <param name="source">The stream from which to copy</param>
        /// <param name="destination">The stream to which to copy</param>
        /// <param name="bufferSize">The size of the buffer to use when copying</param>
        public static void CopyStream(Stream source, Stream destination, int bufferSize = 4096)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

            if (bufferSize <= 0)
                throw new ArgumentException("Buffer size must be a positive integer.", nameof(bufferSize));

            var buffer = new byte[bufferSize];
            int len;

            while ((len = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, len);
            }
        }
    }
}
