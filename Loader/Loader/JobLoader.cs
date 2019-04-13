using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Loader
{
    public class JobProp
    {
        public string Path { get; set; }
        public string JobName { get; set; }
        public string TypeName { get; set; }
        public string MethodName { get; set; }
        public object[] Params { get; set; }
    }
    public class JobLoader
    {
        public static void Execute(JobProp prop)
        {

            //AppDomainSetup appDomainSetup = new AppDomainSetup
            //{
            //    ApplicationName = prop.JobName,
            //    ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
            //    //ShadowCopyDirectories = AppDomain.CurrentDomain.BaseDirectory,
            //    //ShadowCopyFiles = true.ToString().ToLower(),
            //    //DynamicBase = AppDomain.CurrentDomain.BaseDirectory,
            //    //PrivateBinPath = AppDomain.CurrentDomain.BaseDirectory,
            //    //PrivateBinPathProbe = AppDomain.CurrentDomain.BaseDirectory,
            //    //CachePath = AppDomain.CurrentDomain.BaseDirectory
            //};
            string pluginDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}dll";
            AppDomainSetup appDomainSetup = new AppDomainSetup();
                         appDomainSetup.ApplicationName = "RoutineReportDynamicDLL";
                         appDomainSetup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;
                         appDomainSetup.PrivateBinPath = pluginDirectory;
                       appDomainSetup.CachePath = Path.Combine(pluginDirectory, "cache/");
                     appDomainSetup.ShadowCopyFiles = "true";
                     appDomainSetup.ShadowCopyDirectories = pluginDirectory;
          

            AppDomain appDomain = AppDomain.CreateDomain(prop.JobName,new Evidence( AppDomain.CurrentDomain.Evidence), appDomainSetup,
    new PermissionSet(PermissionState.Unrestricted));
 
            Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            Console.WriteLine(appDomain.BaseDirectory);
            Console.WriteLine(appDomain.RelativeSearchPath);
            Console.WriteLine(appDomain.ShadowCopyFiles);
        
            AssemblyLoader assemblyLoader = (AssemblyLoader)appDomain.CreateInstanceAndUnwrap(typeof(AssemblyLoader).Assembly.FullName, typeof(AssemblyLoader).FullName);
            Assembly assembly = assemblyLoader.GetAssembly(prop.Path);
            Type assemblyType = assembly.GetType(prop.TypeName);
            MethodInfo assemblyMethod = assemblyType.GetMethod(prop.MethodName);
            var assemblyInstance = Activator.CreateInstance(assemblyType);
            Console.WriteLine(assemblyMethod.Invoke(assemblyInstance, prop.Params));

            AppDomain.Unload(appDomain);
            assemblyLoader = null;
            appDomain = null;
            for (int i=0; i<GC.MaxGeneration;i++) {
                GC.Collect(i);
                GC.WaitForPendingFinalizers();

            }
            GC.Collect(0);
        }
    }
    public class AssemblyLoader : MarshalByRefObject
    {
        public Assembly GetAssembly(string assemblyPath)
        {
            Assembly assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            return assembly;

        }
    }
}
