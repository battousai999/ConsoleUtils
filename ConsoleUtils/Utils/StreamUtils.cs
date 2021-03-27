using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Battousai.Utils.Utils
{
    public static class StreamUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="bufferSize"></param>
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
