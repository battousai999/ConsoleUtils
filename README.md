# ConsoleUtils
This library is composed of a collection of helper methods that I use when creating prototype console applications for experimenting with code.

Typically when I used this library in an experiment, I will include the library with a static using statement (so that I don't have to qualify the helper methods in the library with "ConsoleUtils.").

``` C#
using static Battousai.Utils.ConsoleUtils;
```

I use the RunLoggingExceptions() method to wrap around the experimental code, catching any exceptions (logging the exception's stack trace to the console) and injecting a "Press enter key to continue" (since I'll typically be running the code in Visual Studio, and otherwise the console in which the results are displayed closes before I can see the results).  I also use the Log() method as a shortcut replacement for Console.WriteLine().

For example:
``` C#
using System;
using System.Collections.Generic;
using System.Linq;
using static Battousai.Utils.ConsoleUtils;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RunLoggingExceptions(() =>
            {
                var list1 = new List<int> { 1, 2, 3, 4, 1, 2, 3, 4 };
                var list2 = new List<int> { 4 };

                Log("Does the LINQ Except method treat the lists as sets?");
                Log("(That is, do the results only contain distinct elements?)");
                Log();

                var results = list1.Except(list2).ToList();

                Log("Results = " + String.Join(", ", results));
            },
            true);
        }
    }
}
```

The library also contains some other helper methods for things such as measuring duration and iterating actions.
