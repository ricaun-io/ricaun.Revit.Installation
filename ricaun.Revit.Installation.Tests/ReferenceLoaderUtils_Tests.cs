using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace ricaun.Revit.Installation.Tests
{
    public class ReferenceLoaderUtils_Tests
    {
        [Test]
        public void ReferenceLoaderUtils_Tests_GetReferences()
        {
            var assemblyFile = Assembly.GetExecutingAssembly().Location;
            var names = ReferenceLoaderUtils.GetReferencedAssemblies(assemblyFile);
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
            Assert.IsNotNull(names.FirstOrDefault(e => e.Name.StartsWith("nunit.framework")));
            Assert.IsNotNull(names.FirstOrDefault(e => e.Name.StartsWith("ricaun.Revit.Installation")));
        }

        [Test]
        public void ReferenceLoaderUtils_Tests_GetReferencesRepeat()
        {
            var assemblyFile = Assembly.GetExecutingAssembly().Location;
            for (int i = 0; i < 5; i++)
            {
                var names = ReferenceLoaderUtils.GetReferencedAssemblies(assemblyFile);
                Assert.IsNotNull(names.FirstOrDefault(e => e.Name.StartsWith("nunit.framework")));
                Assert.IsNotNull(names.FirstOrDefault(e => e.Name.StartsWith("ricaun.Revit.Installation")));
            }
            Assert.Zero(AppDomain.CurrentDomain.ReflectionOnlyGetAssemblies().Length);
        }
    }
}