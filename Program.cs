using System.Collections.Generic;

namespace SimpleJson
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: add negative tests.

            TestRunner.RunTest("Samples/Basics.txt", new Dictionary<string, object>() {
                { "a", 123 },
                { "b", 456 },
                { "c", 7.89 },
                { "d", 0.0123 },
                { "e", null },
                { "f", true },
                { "g", false },
                { "h", new object[] { 1, 2, 3 } },
                { "i", "hello, world!" },
                { "j", "\"Hello, World!\"\n" },
                { "k", new object[] { true, false, null, "" } },
                { "l", new Dictionary<string, object>()
                    {
                        { "a", 1 },
                        { "b", null },
                        { "c", 3.14 }
                    }
                },
            });

            TestRunner.RunTest("Samples/EndingComma.txt", new Dictionary<string, object>()
            {
                { "a", new object[] { 1, 2, 3 } },
                { "b", null },
            }, new JsonOption[] { JsonOption.ALLOW_TRAILING_COMMA });

            TestRunner.RunTest("Samples/Comments.txt", new Dictionary<string, object>()
            {
                { "a", "a" },
            }, new JsonOption[] { JsonOption.ALLOW_COMMENTS });

            TestRunner.RunTest("Samples/DecimalFirstChar.txt", new Dictionary<string, object>()
            {
                { "a", 0.123 },
            }, new JsonOption[] { JsonOption.ALLOW_DECIMAL_AS_FLOAT_FIRST_CHAR });


        }
    }
}
