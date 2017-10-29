using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJson
{
    class TestRunner
    {
        public static void RunTest(string file, object expected, params JsonOption[] options)
        {
            string json = ReadResource(file);
            JsonParser parser = new JsonParser(json);
            foreach (JsonOption option in options)
            {
                parser.AddOption(option);
            }
            object value = parser.Parse();

            if (!IsSame(expected, value))
            {
                throw new Exception("Values were not the same.");
            }
        }

        private static bool IsSame(object expected, object actual)
        {
            if (expected == null) return actual == null;
            if (expected is bool)
            {
                if (actual is bool) return (bool)expected == (bool)actual;
                return false;
            }
            if (expected is int)
            {
                if (actual is int) return (int)expected == (int)actual;
                return false;
            }
            if (expected is double)
            {
                if (actual is double)
                {
                    double diff = (double)expected - (double)actual;
                    return Math.Abs(diff) < 0.00000001;
                }
                return false;
            }
            if (expected is string)
            {
                if (actual is string)
                {
                    return (string)expected == (string)actual;
                }
                return false;
            }
            if (expected is object[])
            {
                return (actual is object[]) && CompareLists((object[])expected, (object[])actual);
            }
            if (expected is IDictionary<string, object>)
            {
                return (actual is IDictionary<string, object>) && CompareDictionaries((IDictionary<string, object>)expected, (IDictionary<string, object>)actual);
            }
            throw new Exception(); // missing value type.
        }

        private static bool CompareLists(object[] expected, object[] actual)
        {
            if (expected.Length != actual.Length) return false;
            for (int i = 0; i < expected.Length; ++i)
            {
                if (!IsSame(expected[i], actual[i])) return false;
            }
            return true;
        }

        private static bool CompareDictionaries(IDictionary<string, object> expected, IDictionary<string, object> actual)
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            foreach (string key in expected.Keys)
            {
                if (!actual.ContainsKey(key)) return false;
                if (!IsSame(expected[key], actual[key])) return false;
            }
            return true;
        }

        private static string ReadResource(string filename)
        {
            string path = "SimpleJson." + filename.Replace('/', '.');
            System.IO.Stream testStream = typeof(TestRunner).Assembly.GetManifestResourceStream(path);
            System.IO.StreamReader sr = new System.IO.StreamReader(testStream);
            return sr.ReadToEnd();
        }
    }
}
