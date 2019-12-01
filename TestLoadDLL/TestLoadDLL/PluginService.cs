using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;


namespace TestLoadDLL
{

    public class PluginService

    {

        // put entire UnloadableAssemblyLoadContext in a method to avoid caller
        // holding on to the reference
        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ExecuteAssembly(HostedPlugin plugin, string input)
        {
            var context = new CollectibleAssemblyContext(@"C:\Users\ChengJhe\Desktop\TestLoadDLL\TestLoadDLL\bin\Debug\netcoreapp3.0\Plugins\Newtonsoft.Json.dll");
            var assemblyPath = Path.Combine(plugin.FilePath);
            using (var fs = new FileStream(assemblyPath, FileMode.Open, FileAccess.Read))
            {
                var assembly = context.LoadFromStream(fs);

                var type = assembly.GetType("TestJob.Job1");
                var executeMethod = type.GetMethod("GetName");

                var instance = Activator.CreateInstance(type);

              Console.WriteLine(executeMethod.Invoke(instance, new object[] { input }).ToString());
            }

            context.Unload();

        }


        public void RunPlugin(HostedPlugin plugin, string input)
        {
            ExecuteAssembly(plugin, input);

           RunGarbageCollection();
        }


        private static void RunGarbageCollection()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (System.Exception)
            {
                //sometimes GC.Collet/WaitForPendingFinalizers crashes, just ignore for this blog post
            }
        }


    }
}
