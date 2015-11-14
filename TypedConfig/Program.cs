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
        private static readonly IDictionary<string, string> ConfigValuesFromAnySource =
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
            Func<string, string> settingGetter = s => ConfigValuesFromAnySource[s];
            var settings = new []
            {
                new GeneratedTypedConcreteConfig(settingGetter),
                TypedConfigProxy<ITypedConcreteConfig>.Create(settingGetter)
            };

            foreach (var setting in settings)
            {
                Console.WriteLine("Starting " + setting.GetType().Name);
                Console.WriteLine();
                var start = MeasureAccessTime(setting, 5).TotalMilliseconds;
                Console.WriteLine("Started in {0} milliseconds",start);
                Console.WriteLine();
                var work = MeasureAccessTime(setting, 10000000).TotalMilliseconds;
                Console.WriteLine("Finished work in {0} milliseconds",work);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static TimeSpan MeasureAccessTime(ITypedConcreteConfig config, int count)
        {
            var watch = new Stopwatch();
            watch.Start();
            foreach (var num in Enumerable.Range(0,count))
            {
                var a = config.Balance;
                var b = config.CustomerMail;
                var c = config.FirstName;
                var d = config.LastName;
                var e = config.MiddleName;
                var f = config.Subscription;
                var g = config.MounthlyFee;
            }
            watch.Stop();
            return watch.Elapsed;
        }
    }
}
