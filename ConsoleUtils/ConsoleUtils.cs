using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battousai.Utils
{
    public static class ConsoleUtils
    {
        private static readonly Regex logExceptionMessageEndingRegex = new Regex(@"[:-]\s*$");

        private static Action<string> consoleWriter = null;
        private static Func<string> consoleReader = null;
        private static bool injectNewlineForWriter = false;

        /// <summary>
        /// This method executes the action, optionally pausing afterwards (with a message to press
        /// the enter key to continue and waiting for a line of text from the console or from a registered
        /// "console reader").  If any exceptions are thrown during the execution of the action, they
        /// will be logged to the console (or to a registered "console writer").
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <param name="isPauseAtEnd">A flag indicating whether to pause after executing the action.</param>
        /// <param name="displayDuration">A flag indicating whether to display duration of execution.</param>
        public static void RunLoggingExceptions(Action action, bool isPauseAtEnd = false, bool displayDuration = true)
        {
            try
            {
                var duration = MeasureDuration(action);

                if (displayDuration)
                    Log($"Finished in {NormalizeDuration(duration)}.");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            if (isPauseAtEnd)
            {
                Log("Press <enter> to continue...");
                ReadFromConsoleReader();
            }
        }

        /// <summary>
        /// This method executes the asynchronous action, optionally pausing afterwards (with a message 
        /// to press the enter key to continue and waiting for a line of text from the console or from a 
        /// registered "console reader").  If any exceptions are thrown during the execution of the action,
        /// they will be logged to the console (or to a registered "console writer").
        /// </summary>
        /// <param name="action">The asynchronous action to execute.</param>
        /// <param name="isPauseAtEnd">A flag indicating whether to pause after executing the action.</param>
        /// <param name="displayDuration">A flag indicating whether to display duration of execution.</param>
        public static void RunLoggingExceptionsAsync(Func<Task> action, bool isPauseAtEnd = false, bool displayDuration = true) 
        {
            RunLoggingExceptions(() =>
            {
                action().GetAwaiter().GetResult();
            },
            isPauseAtEnd,
            displayDuration);
        }

        /// <summary>
        /// This method maps a TimeSpan duration to a string representation of the duration in units of milliseconds,
        /// seconds, minutes, hours, or days, as appropriate.
        /// </summary>
        /// <param name="duration">A duration.</param>
        /// <returns>A string representation of the duration.</returns>
        public static string NormalizeDuration(TimeSpan duration)
        {
            if (duration > TimeSpan.FromDays(1))
                return $"{duration.TotalDays:0.00} days";
            else if (duration > TimeSpan.FromHours(1))
                return $"{duration.TotalHours:0.00} hours";
            else if (duration > TimeSpan.FromMinutes(1))
                return $"{duration.TotalMinutes:0.00} minutes";
            else if (duration > TimeSpan.FromSeconds(1))
                return $"{duration.TotalSeconds:0.00} seconds";
            else
                return $"{duration.TotalMilliseconds} ms";
        }

        private static string ReadFromConsoleReader()
        {
            if (consoleReader == null)
                return Console.ReadLine();
            else
                return consoleReader();
        }

        private static void WriteToConsoleWriter(string message)
        {
            if (consoleWriter == null)
                Console.WriteLine(message);
            else if (injectNewlineForWriter)
                consoleWriter(message + Environment.NewLine);
            else
                consoleWriter(message);
        }

        /// <summary>
        /// The method logs a string to the console (or to a registered "console writer").
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message = "")
        {
            WriteToConsoleWriter(message ?? "");
        }
        /// <summary>
        /// The method logs a string to the console (or to a registered "console writer").  The parameters
        /// passed to this method follow the same pattern as the String.Format function.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Log(string message, params object[] parameters)
        {
            WriteToConsoleWriter(String.Format(message ?? "", parameters));
        }

        /// <summary>
        /// This method logs an exception to the console (or to a registered "console writer"). 
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(Exception ex)
        {
            LogException(null, ex);
        }

        /// <summary>
        /// This method logs an exception to the console (or to a registered "console writer").  The text
        /// logged includes the given message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(string message, Exception ex)
        {
            Log();

            if (String.IsNullOrWhiteSpace(message))
                Log($"Caught exception: <{ex.GetType().Name}> {ex.Message}");
            else
            {
                if (!logExceptionMessageEndingRegex.IsMatch(message))
                    message += ": ";

                Log($"{message}<{ex.GetType().Name}> {ex.Message}");
            }

            Log(ex.StackTrace);

            var iex = ex.InnerException;

            while (iex != null)
            {
                Log();
                Log($"[Inner Exception] <{iex.GetType().Name}> {iex.Message}");
                Log(iex.StackTrace);

                iex = iex.InnerException;
            }

            Log();
        }

        /// <summary>
        /// This method executes the given action, returning the amount of time it took to execute
        /// the action.
        /// </summary>
        /// <param name="action">The action to execute and measure.</param>
        /// <returns>A timespan representing the amount of time to execute the action.</returns>
        public static TimeSpan MeasureDuration(Action action)
        {
            var startTime = DateTime.Now;

            action();

            return DateTime.Now.Subtract(startTime);
        }

        /// <summary>
        /// This method executes the given action a number of times equal to the "iterations" parameter.
        /// </summary>
        /// <param name="iterations">The number of times to execute the action.</param>
        /// <param name="action">The action to execute.</param>
        public static void Iterate(int iterations, Action action)
        {
            Iterate(iterations, _ => action());
        }

        /// <summary>
        /// This method executes the given action (passing as a parameter an iteration index) a number of
        /// times equal to the "iterations" parameter.  The iteration index (passed to the action) starts 
        /// with the given "startingInterval" parameter value.
        /// </summary>
        /// <param name="iterations">The number of times to execute the action.</param>
        /// <param name="action">The action to execute.</param>
        /// <param name="startingInterval">
        /// The starting value of the iteration index (passed to the action).
        /// </param>
        public static void Iterate(int iterations, Action<int> action, int startingInterval = 0)
        {
            Enumerable.Range(startingInterval, iterations)
                .ToList()
                .ForEach(x => action(x));
        }

        /// <summary>
        /// This method registers a "console writer" action that receives logged text (instead of the 
        /// console) when the RunLoggingExceptions, RunLoggingExceptionsAsync, and Log* methods are invoked.
        /// If a null value is passed for the "writer" parameter, then the console will be used.
        /// </summary>
        /// <param name="writer">The action to receive logged text.</param>
        /// <param name="injectNewline">
        /// A flag indicating whether to append a new-line to each line of logged text.
        /// </param>
        public static void RegisterConsoleWriter(Action<string> writer, bool injectNewline = false)
        {
            consoleWriter = writer;
            injectNewlineForWriter = injectNewline;
        }

        /// <summary>
        /// This method registers a "console reader" function that provides text (instead of the console)
        /// when the RunLoggingExceptions and RunLoggingExceptionsAsync methods pause and wait for the 
        /// enter key to be pressed.  If a null value is passed for the "reader" parameter, then the 
        /// console will be used.
        /// </summary>
        /// <param name="reader">The function to provide text.</param>
        public static void RegisterConsoleReader(Func<string> reader)
        {
            consoleReader = reader;
        }
    }
}
