using SharedKernel.Application.Utils.Extensions;

namespace ConsoleApplication.Utils {

    internal static class Printer {

        internal static void PrintLine (string line, int leftMarginSize = 2) {
            if (leftMarginSize > 0)
                line = line.AddLeftMargin(leftMarginSize);
            Console.WriteLine(line);
        }

    }

}