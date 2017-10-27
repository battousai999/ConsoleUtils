using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Battousai.Utils
{
    public static class ConsoleUtils
    {
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
                Console.ReadLine();
            }
        }

        public static void Log()
        {
            Console.WriteLine();
        }

        public static void Log(string message)
        {
            Console.WriteLine(message);
        }

        public static void Log(string message, params object[] parameters)
        {
            Console.WriteLine(message, parameters);
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
                Regex logExceptionMessageEndingRegex = new Regex(@"[:-]\s*$");

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

        public static void Iterate(int iterations, Action<int> action)
        {
            Enumerable.Repeat(1, iterations)
                .ToList()
                .ForEach(x => action(x));
        }
    }
}
