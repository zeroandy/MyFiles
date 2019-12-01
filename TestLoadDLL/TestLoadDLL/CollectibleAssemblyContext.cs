using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;

namespace TestLoadDLL
{
    public class CollectibleAssemblyContext : AssemblyLoadContext
    {
        //public CollectibleAssemblyContext() : base(isCollectible: true)
        //{
        //}

        //protected override Assembly Load(AssemblyName assemblyName)
        //{
        //    return null;
        //}


        private AssemblyDependencyResolver _resolver;

        public CollectibleAssemblyContext(string mainAssemblyToLoadPath) : base(isCollectible: true)
        {
            _resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath);
        }

        protected override Assembly Load(AssemblyName name)
        {
            string assemblyPath = _resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }
    }
}
