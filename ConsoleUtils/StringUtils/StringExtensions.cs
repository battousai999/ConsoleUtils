using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Battousai.Utils.StringUtils
{
    public static class StringExtensions
    {
        /// <summary>
        /// Surrounds the given 'text' with the strings given in 'openBracket' and 'closeBracket'.
        /// </summary>
        /// <param name="text">The text to surround</param>
        /// <param name="openBracket">The opening text with which to surround</param>
        /// <param name="closeBracket">The closing text with which to surround</param>
        /// <param name="bracketIfEmpty">A flag indicating whether to surround a null or empty text</param>
        /// <returns>The surrounded text.</returns>
        public static string Bracket(this string text, string openBracket, string closeBracket, bool bracketIfEmpty = false)
        {
            if (!bracketIfEmpty && String.IsNullOrWhiteSpace(text))
                return text;

            return $"{openBracket}{text ?? ""}{closeBracket}";
        }

        /// <summary>
        /// Pads the given 'text' with space (on the right) up to a given 'size'.  If the 'text' is too large for 
        /// the 'size', then the 'text' is truncated and the 'ellipsis' string is appended to the text.
        /// </summary>
        /// <param name="text">The text to pad</param>
        /// <param name="size">The length of the resulting string</param>
        /// <param name="ellipsis">The string to append if the text is larger than the given 'size'</param>
        /// <returns>A string of length 'size' that may or may not (depending upon the length of 'text') end with 
        /// the 'ellipsis' string.</returns>
        public static string PadRightWithEllipsis(this string text, int size, string ellipsis = "...")
        {
            if (String.IsNullOrWhiteSpace(text))
                return String.Empty.PadRight(size);

            var ellipsisLength = ellipsis?.Length ?? 0;

            if (size < (ellipsisLength + 1))
                return text.Substring(0, size);
            else if (text.Length > size)
                return $"{text.Substring(0, size - ellipsisLength)}{ellipsis ?? ""}";
            else
                return text.PadRight(size);
        }

        /// <summary>
        /// Pads the given 'text' with space (on the left) up to a given 'size'.  If the 'text' is too large for 
        /// the 'size', then the 'text' is truncated and the 'ellipsis' string is appended to the text.
        /// </summary>
        /// <param name="text">The text to pad</param>
        /// <param name="size">The length of the resulting string</param>
        /// <param name="ellipsis">The string to append if the text is larger than the give 'size'</param>
        /// <returns>A string of length 'size' that may or may not (depending upon the length of 'text') end with
        /// the 'ellipsis' string.</returns>
        public static string PadLeftWithEllipsis(this string text, int size, string ellipsis = "...")
        {
            if (String.IsNullOrWhiteSpace(text))
                return String.Empty.PadRight(size);

            var ellipsisLength = ellipsis?.Length ?? 0;

            if (size < (ellipsisLength + 1))
                return text.Substring(0, size);
            else if (text.Length > size)
                return $"{text.Substring(0, size - ellipsisLength)}{ellipsis ?? ""}";
            else
                return text.PadLeft(size);
        }

        /// <summary>
        /// Returns the string 'text' with the string 'ending' appended to it if it does not already end with 'ending'.
        /// </summary>
        /// <param name="text">The text to which to potentially append 'ending'</param>
        /// <param name="ending">The string to append</param>
        /// <returns>The string 'text' with the string 'ending' appended if not already ending with 'ending'.</returns>
        public static string EnsureEndsWith(this string text, string ending) => EnsureEndsWith(text, ending, StringComparison.Ordinal);

        /// <summary>
        /// Returns the string 'text' with the string 'ending' appended to it if it does not already end with 'ending'.
        /// </summary>
        /// <param name="text">The text to which to potentially append 'ending'</param>
        /// <param name="ending">The string to append</param>
        /// <param name="comparison">The comparison to use to test whether 'text' already ends with 'ending'</param>
        /// <returns>The string 'text' with the string 'ending' appended if not already ending with 'ending'.</returns>
        public static string EnsureEndsWith(this string text, string ending, StringComparison comparison)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (ending == null)
                throw new ArgumentNullException(nameof(ending));

            if (text.EndsWith(ending, comparison))
                return text;
            else
                return $"{text}{ending}";
        }

        /// <summary>
        /// Returns the string 'text' with the string 'starting' prepended to it if it does not already start with 'starting'.
        /// </summary>
        /// <param name="text">The text to which to potentially prepend 'starting'</param>
        /// <param name="starting">The string to prepend</param>
        /// <returns>The string 'text' with the string 'starting' prepended if not already starting with 'starting'.</returns>
        public static string EnsureStartsWith(this string text, string starting) => EnsureStartsWith(text, starting, StringComparison.Ordinal);

        /// <summary>
        /// Returns the string 'text' with the string 'starting' prepended to it if it does not already start with 'starting'.
        /// </summary>
        /// <param name="text">The text to which to potentially prepend 'starting'</param>
        /// <param name="starting">The string to prepend</param>
        /// <param name="comparison">The comparison to use to test whether 'text' already starts with 'starting'</param>
        /// <returns>The string 'text' with the string 'starting' prepended if not already starting with 'starting'.</returns>
        public static string EnsureStartsWith(this string text, string starting, StringComparison comparison)
        {
            if (text == null)
                throw new ArgumentNullException(nameof(text));

            if (starting == null)
                throw new ArgumentNullException(nameof(starting));

            if (text.StartsWith(starting, comparison))
                return text;
            else
                return $"{starting}{text}";
        }

        /// <summary>
        /// Returns the string 'text' with the substring 'ending' removed from its end, if the string 'text' ends with the string 'ending'.
        /// Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'ending' from the end</param>
        /// <param name="ending">The string to remove from the end of 'text'</param>
        /// <returns>The string 'text' with the substring 'ending' removed from its end.</returns>
        public static string RemoveTrailing(this string text, string ending) => RemoveTrailing(text, ending, StringComparison.Ordinal);

        /// <summary>
        /// Returns the string 'text' with the substring 'ending' removed from its end, if the string 'text' ends with the string 'ending'.
        /// Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'ending' from the end</param>
        /// <param name="ending">The string to remove from the end of 'text'</param>
        /// <param name="comparison">The comparison to use to test whether 'text' ends with 'ending'</param>
        /// <returns>The string 'text' with the substring 'ending' removed from its end.</returns>
        public static string RemoveTrailing(this string text, string ending, StringComparison comparison)
        {
            if (!String.IsNullOrEmpty(ending) && (text?.EndsWith(ending, comparison) ?? false))
                return text.Remove(text.Length - ending.Length);
            else
                return text;
        }

        /// <summary>
        /// Returns the string 'text' with the substring 'starting' removed from its beginning, if the string 'text' starts with the
        /// string 'starting'.  Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'starting' from the beginning</param>
        /// <param name="starting">The string to remove from the beginning of 'text'</param>
        /// <returns>The string 'text' with the substring 'ending' removed from its beginning.</returns>
        public static string RemoveLeading(this string text, string starting) => RemoveLeading(text, starting, StringComparison.Ordinal);

        /// <summary>
        /// Returns the string 'text' with the substring 'starting' removed from its beginning, if the string 'text' starts with the
        /// string 'starting'.  Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'starting' from the beginning</param>
        /// <param name="starting">The string to remove from the beginning of 'text'</param>
        /// <param name="comparison">The comparison to use to test whether 'text' starts with 'starting'</param>
        /// <returns>The string 'text' with the substring 'ending' removed from its beginning.</returns>
        public static string RemoveLeading(this string text, string starting, StringComparison comparison)
        {
            if (!String.IsNullOrEmpty(starting) && (text?.StartsWith(starting, comparison) ?? false))
                return text.Remove(0, starting.Length);
            else
                return text;
        }

        /// <summary>
        /// Returns the string 'text with the substring 'surrounding' removed from its beginning (if the string 'text' starts with the
        /// string 'surrounding') and the substring 'surrounding' removed from its end (if the string 'text' ends with the string 
        /// 'surrounding').  Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'surrounding' from the beginning and 'surrounding' from the end</param>
        /// <param name="surrounding">The string to remove from the beginning and end of 'text'</param>
        /// <returns>The string 'text' with the substring 'starting' removed from its beginning and the substring 'ending' removed
        /// from its end.</returns>
        public static string RemoveSurrounding(this string text, string surrounding) => text.RemoveSurrounding(surrounding, StringComparison.Ordinal);

        /// <summary>
        /// Returns the string 'text' with the substring 'starting' removed from its beginning (if the string 'text' starts with the
        /// string 'starting') and the substring 'ending' removed from its end (if the string 'text' ends with the string 'ending').
        /// Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'starting' from the beginning and 'ending' from the end</param>
        /// <param name="starting">The string to remove from the beginning of 'text'</param>
        /// <param name="ending">The string to remove from the end of 'text'</param>
        /// <returns>The string 'text' with the substring 'starting' removed from its beginning and the substring 'ending' removed
        /// from its end.</returns>
        public static string RemoveSurrounding(this string text, string starting, string ending)
        {
            return text.RemoveSurrounding(starting, ending, StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the string 'text with the substring 'surrounding' removed from its beginning (if the string 'text' starts with the
        /// string 'surrounding') and the substring 'surrounding' removed from its end (if the string 'text' ends with the string 
        /// 'surrounding').  Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'surrounding' from the beginning and 'surrounding' from the end</param>
        /// <param name="surrounding">The string to remove from the beginning and end of 'text'</param>
        /// <param name="comparison">The comparison to use to test whether 'text' starts with 'surrounding' or ends with 'surrounding'</param>
        /// <returns>The string 'text' with the substring 'starting' removed from its beginning and the substring 'ending' removed
        /// from its end.</returns>
        public static string RemoveSurrounding(this string text, string surrounding, StringComparison comparison)
        {
            return text.RemoveSurrounding(surrounding, surrounding, comparison);
        }

        /// <summary>
        /// Returns the string 'text' with the substring 'starting' removed from its beginning (if the string 'text' starts with the
        /// string 'starting') and the substring 'ending' removed from its end (if the string 'text' ends with the string 'ending').
        /// Otherwise, the string 'text' is returned unchanged.
        /// </summary>
        /// <param name="text">The text from which to remove 'starting' from the beginning and 'ending' from the end</param>
        /// <param name="starting">The string to remove from the beginning of 'text'</param>
        /// <param name="ending">The string to remove from the end of 'text'</param>
        /// <param name="comparison">The comparison to use to test whether 'text' starts with 'starting' or ends with 'ending'</param>
        /// <returns>The string 'text' with the substring 'starting' removed from its beginning and the substring 'ending' removed
        /// from its end.</returns>
        public static string RemoveSurrounding(this string text, string starting, string ending, StringComparison comparison)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            return text
                .RemoveLeading(starting, comparison)
                .RemoveTrailing(ending, comparison);
        }

        /// <summary>
        /// Returns a stream containing the contents of 'str' encoded with the given 'encoding' (if specified).  Note: the
        /// returned stream should be disposed by the caller.
        /// </summary>
        /// <param name="str">The string that will make up the contents of the returned stream</param>
        /// <param name="encoding">The encoding to use to encode the contents in the stream</param>
        /// <returns>A stream containing 'str' as its contents.</returns>
        public static Stream ToStream(this string str, Encoding encoding = null)
        {
            var stream = new MemoryStream();

            using (var writer = new StreamWriter(stream, encoding ?? new UTF8Encoding(false, true), 1024, true))
            {
                writer.Write(str ?? "");
                writer.Flush();

                stream.Position = 0;

                return stream;
            }
        }
    }
}
