using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe((i) =>
            //{
                JobLoader.Execute(
                     new JobProp
                     {
                         JobName = "Test",
                         Path = $"{System.Environment.CurrentDirectory}\\dll\\Job.dll",
                         MethodName = "Get",
                         TypeName = "Job.Test"
                     });

            // });

            Console.WriteLine("OK");
            Console.ReadKey();
        }
    }
}
