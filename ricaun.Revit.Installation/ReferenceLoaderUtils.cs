using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ricaun.Revit.Installation
{
    /// <summary>
    /// ReferenceLoaderUtils
    /// </summary>
    public static class ReferenceLoaderUtils
    {
#if NETFRAMEWORK
        /// <summary>
        /// Get references of the <paramref name="assemblyPath"/> using a diferent AppDomain
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        public static AssemblyName[] GetReferencedAssemblies(string assemblyPath)
        {
            var settings = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
            };
            var childDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null, settings);

            var loader = childDomain.CreateInstanceAndUnwrap<ReferenceLoader>();

            //This operation is executed in the new AppDomain
            var assemblyNames = loader.LoadReferences(assemblyPath);

            AppDomain.Unload(childDomain);

            return assemblyNames;
        }

        private static T CreateInstanceAndUnwrap<T>(this AppDomain domain, params object[] args) where T : MarshalByRefObject
        {
            var handle = Activator.CreateInstance(domain,
                       typeof(T).Assembly.FullName,
                       typeof(T).FullName,
                       false, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, args, CultureInfo.CurrentCulture, new object[0]);

            return (T)handle.Unwrap();
        }

        /// <summary>
        /// ReferenceLoader
        /// <code>
        /// https://stackoverflow.com/questions/225330/how-to-load-a-net-assembly-for-reflection-operations-and-subsequently-unload-it/37970043#37970043
        /// </code>
        /// </summary>
        public class ReferenceLoader : MarshalByRefObject
        {
            public AssemblyName[] LoadReferences(string assemblyPath)
            {
                var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
                var assemblyNames = assembly.GetReferencedAssemblies().ToArray();
                return assemblyNames;
            }
        }
#else
        public static AssemblyName[] GetReferencedAssemblies(string assemblyPath)
        {
            //var assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
            //var domain = AppDomain.CurrentDomain;
            //var assembly = domain.ReflectionOnlyGetAssemblies()
            //    .FirstOrDefault(e => e.GetName().Name == assemblyName.Name);

            //assembly = assembly ?? Assembly.ReflectionOnlyLoad(File.ReadAllBytes(assemblyPath));
            //return assembly.GetReferencedAssemblies();

            var assembly = Assembly.Load(File.ReadAllBytes(assemblyPath));
            return assembly.GetReferencedAssemblies();
        }
#endif
    }
}