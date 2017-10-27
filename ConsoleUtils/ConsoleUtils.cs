using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Battousai.Utils
{
    public static class ConsoleUtils
    {
        private static readonly Regex logExceptionMessageEndingRegex = new Regex(@"[:-]\s*$");

        private static Action<string> consoleWriter = null;
        private static Func<string> consoleReader = null;
        private static bool injectNewlineForWriter = false;

        public static void RunLoggingExceptions(Action action, bool isPauseAtEnd = false)
        {
            try
            {
                action();
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

        public static void Log(string message = "")
        {
            WriteToConsoleWriter(message ?? "");
        }

        public static void Log(string message, params object[] parameters)
        {
            WriteToConsoleWriter(String.Format(message ?? "", parameters));
        }

        public static void LogException(Exception ex)
        {
            LogException(null, ex);
        }

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

        public static TimeSpan MeasureDuration(Action action)
        {
            var startTime = DateTime.Now;

            action();

            return DateTime.Now.Subtract(startTime);
        }

        public static void Iterate(int iterations, Action action)
        {
            Iterate(iterations, _ => action());
        }

        public static void Iterate(int iterations, Action<int> action, int startingInterval = 0)
        {
            Enumerable.Range(startingInterval, iterations)
                .ToList()
                .ForEach(x => action(x));
        }

        public static void RegisterConsoleWriter(Action<string> writer, bool injectNewline = false)
        {
            consoleWriter = writer;
            injectNewlineForWriter = injectNewline;
        }

        public static void RegisterConsoleReader(Func<string> reader)
        {
            consoleReader = reader;
        }
    }
}
