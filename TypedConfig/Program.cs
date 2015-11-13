using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypedConfig
{
    class Program
    {
        private IDictionary<string, string> configValuesFromAnySource =
            new Dictionary<string, string>()
            {
                {"FirstName", "test first name"},
                {"LastName", "test last name"},
                {"MiddleName", "configured middle name"},
                {"MounthlyFee", "1232.34"},
                {"Balance", "120.0"},
                {"CustomerMail", "customer@try.net"},
                {"Subscription", "Small"}
            };

        static void Main(string[] args)
        {



        }

        private static TimeSpan MeasureAccessTime(ITypedConcreteConfig config, int count)
        {
            var watch = new Stopwatch();
            watch.Start();
            foreach (var num in Enumerable.Range(0,count))
            {
                var a = config.Balance;
                var a = config.CustomerMail;
                var a = config.FirstName;
                var a = config.LastName;
                var a = config.MiddleName;
                var a = config.Subscription;
                var a = config.MounthlyFee;

            }

        }
    }
}
