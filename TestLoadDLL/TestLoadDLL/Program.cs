
using System;
using System.Reactive.Linq;

namespace TestLoadDLL
{
    class Program
    {
        static void Main(string[] args)
        {
            PluginService pluginService = new PluginService();

            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe((i) =>
            {
                pluginService.RunPlugin(new HostedPlugin { Name = "TestJob.dll", FilePath = @"C:\Users\ChengJhe\Desktop\TestLoadDLL\TestLoadDLL\bin\Debug\netcoreapp3.0\Plugins\TestJob.dll" }, "test");
            });

            Console.ReadKey();
        }
    }
}
