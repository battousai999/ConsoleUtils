using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using utils = Battousai.Utils;

namespace ConsoleUtils.Tests
{
    public class ConsoleUtilsTests
    {
        public ConsoleUtilsTests()
        {
            utils.ConsoleUtils.RegisterConsoleWriter(null);
            utils.ConsoleUtils.RegisterConsoleReader(null);
        }

        [Fact]
        public void RunLoggingExceptions_should_consume_exceptions()
        {
            utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                throw new InvalidOperationException("Test exception");
            });
        }

        [Fact]
        public void RunLoggingExceptions_should_perform_the_action()
        {
            var value = 0;

            utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                value = 99;
            });

            Assert.Equal(99, value);
        }

        [Fact]
        public void RunLoggingExceptions_should_log_a_thrown_exception()
        {
            var log = "";
            var exceptionMessage = "Test exception";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.RunLoggingExceptions(() =>
            {
                throw new InvalidOperationException(exceptionMessage);
            });

            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void RunLoggingExceptions_should_capture_console_readline_when_appropriate()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

            Assert.True(hasCalledReadline);
        }

        [Fact]
        public void RunLoggingExceptions_should_not_capture_console_readline_when_inappropriate()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, false);

            Assert.False(hasCalledReadline);
        }

        [Fact]
        public void RunLoggingExceptions_should_display_duration_when_appropriate()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, true);

            Assert.Contains("Finished in", log);
        }

        [Fact]
        public void RunLoggingExceptions_should_not_display_duration_when_inappropriate()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptions(() => { Thread.Sleep(100); }, false, false);

            Assert.DoesNotContain("Finished in", log);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_consume_exceptions()
        {
            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                throw new InvalidOperationException("Test exception");
            });
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_perform_the_action()
        {
            var value = 0;

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                value = 99;
            });

            Assert.Equal(99, value);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_log_a_thrown_exception()
        {
            var log = "";
            var exceptionMessage = "Test exception";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () =>
            {
                await Task.Delay(100);
                throw new InvalidOperationException(exceptionMessage);
            });

            Assert.DoesNotContain("AggregateException", log);
            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_capture_console_readline_when_appropriate()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, true);

            Assert.True(hasCalledReadline);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_not_capture_console_readline_when_inappropriate()
        {
            var hasCalledReadline = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledReadline = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false);

            Assert.False(hasCalledReadline);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_display_duration_when_appropriate()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false, true);

            Assert.Contains("Finished in", log);
        }

        [Fact]
        public void RunLoggingExceptionsAsync_should_not_display_duration_when_inappropriate()
        {
            string log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => log += x, true);

            utils.ConsoleUtils.RunLoggingExceptionsAsync(async () => { await Task.Delay(100); }, false, false);

            Assert.DoesNotContain("Finished in", log);
        }

        [Fact]
        public void NormalizeDuration_should_return_milliseconds()
        {
            var duration = TimeSpan.FromMilliseconds(500);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalMilliseconds.ToString("0 ms"), value);
        }

        [Fact]
        public void NormalizeDuration_should_return_seconds()
        {
            var duration = TimeSpan.FromSeconds(20);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalSeconds.ToString("0.00 seconds"), value);
        }

        [Fact]
        public void NormalizeDuration_should_return_minutes()
        {
            var duration = TimeSpan.FromMinutes(30);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalMinutes.ToString("0.00 minutes"), value);
        }

        [Fact]
        public void NormalizeDuration_should_return_hours()
        {
            var duration = TimeSpan.FromHours(2);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalHours.ToString("0.00 hours"), value);
        }

        [Fact]
        public void NormalizeDuration_should_return_days()
        {
            var duration = TimeSpan.FromDays(9);
            var value = utils.ConsoleUtils.NormalizeDuration(duration);

            Assert.Equal(duration.TotalDays.ToString("0.00 days"), value);
        }

        [Fact]
        public void Log_should_write_to_console()
        {
            var log = "";
            var testString = "This is a test string.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(testString);

            Assert.Equal(testString, log);
        }

        [Fact]
        public void Log_should_write_only_newline_when_called_with_no_parameters()
        {
            var log = "";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.Log();

            Assert.Equal(Environment.NewLine, log);
        }

        [Fact]
        public void Log_should_work_with_params()
        {
            var log = "";
            var testString = "This is a test string with {0} {1}.";
            var parameters = new List<string> { "some", "parameters" };

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(testString, parameters[0], parameters[1]);

            Assert.Equal(String.Format(testString, parameters[0], parameters[1]), log);
        }

        [Fact]
        public void LogException_should_log_the_type_and_exception_message()
        {
            var log = "";
            var exceptionMessage = "This is a test exception message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                throw new InvalidOperationException(exceptionMessage);
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains("InvalidOperationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void LogException_should_log_the_given_error_message()
        {
            var log = "";
            var errorMessage = "This is a test error message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                throw new InvalidOperationException("");
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(errorMessage, ex);
            }

            Assert.Contains(errorMessage, log);
        }

        [Fact]
        public void LogException_should_log_the_type_and_exception_message_for_inner_exception()
        {
            var log = "";
            var exceptionMessage = "This is a test exception message.";

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            try
            {
                try
                {
                    throw new ApplicationException(exceptionMessage);
                }
                catch (Exception innerEx)
                {
                    throw new InvalidOperationException("Some outer exception message.", innerEx);
                }
            }
            catch (Exception ex)
            {
                utils.ConsoleUtils.LogException(ex);
            }

            Assert.Contains("ApplicationException", log);
            Assert.Contains(exceptionMessage, log);
        }

        [Fact]
        public void MeasureDuration_should_report_a_measured_duration()
        {
            var waitSpan = TimeSpan.FromMilliseconds(100);

            var duration = utils.ConsoleUtils.MeasureDuration(() =>
            {
                System.Threading.Thread.Sleep(waitSpan);
            });

            Assert.True(duration >= waitSpan);
        }

        [Fact]
        public void MeasureDuration_should_report_a_large_duration_for_a_long_running_action()
        {
            var waitSpan = TimeSpan.FromSeconds(3);

            var duration = utils.ConsoleUtils.MeasureDuration(() =>
            {
                System.Threading.Thread.Sleep(waitSpan);
            });

            Assert.True(duration >= waitSpan);
        }

        [Fact]
        public void Iterate_should_loop_expected_number_of_times_1()
        {
            var counter = 0;
            var iterations = 1000;

            utils.ConsoleUtils.Iterate(iterations, () => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void Iterate_should_loop_expected_number_of_times_2()
        {
            var counter = 0;
            var iterations = 999;

            utils.ConsoleUtils.Iterate(iterations, _ => { counter++; });

            Assert.Equal(iterations, counter);
        }

        [Fact]
        public void Iterate_should_pass_the_correct_iteration_number_1()
        {
            var expectedValues = new List<int> { 0, 1, 2, 3, 4, 5 };
            var list = new List<int>();

            utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); });

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }

        [Fact]
        public void Iterate_should_pass_the_correct_iteration_number_2()
        {
            var expectedValues = new List<int> { 3, 4, 5, 6 };
            var list = new List<int>();

            utils.ConsoleUtils.Iterate(expectedValues.Count, x => { list.Add(x); }, 3);

            Assert.Equal(expectedValues.Count, list.Count);

            for (int i = 0; i < list.Count; i++)
            {
                Assert.Equal(expectedValues[i], list[i]);
            }
        }

        [Fact]
        public void RegisterConsoleReader_should_allow_registered_reader_to_be_invoked()
        {
            var hasCalledConsoleReader = false;

            utils.ConsoleUtils.RegisterConsoleReader(() =>
            {
                hasCalledConsoleReader = true;
                return "";
            });

            utils.ConsoleUtils.RunLoggingExceptions(() => { }, true);

            Assert.True(hasCalledConsoleReader);
        }

        [Fact]
        public void RegisterConsoleWriter_should_allow_registered_reader_to_be_invoked()
        {
            var hasCalledConsoleWriter = false;

            utils.ConsoleUtils.RegisterConsoleWriter(_ =>
            {
                hasCalledConsoleWriter = true;
            });

            utils.ConsoleUtils.Log("testing...");

            Assert.True(hasCalledConsoleWriter);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_true_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + Environment.NewLine + str2 + Environment.NewLine;

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, true);

            utils.ConsoleUtils.Log(str1);
            utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }

        [Fact]
        public void RegisterConsoleWriter_should_honor_false_injectNewline_parameter()
        {
            var log = "";
            var str1 = "test";
            var str2 = "string";
            var expectedValue = str1 + str2;

            utils.ConsoleUtils.RegisterConsoleWriter(x => { log += x; }, false);

            utils.ConsoleUtils.Log(str1);
            utils.ConsoleUtils.Log(str2);

            Assert.Equal(expectedValue, log);
        }
    }
}
