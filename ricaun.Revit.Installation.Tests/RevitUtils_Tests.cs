using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace ricaun.Revit.Installation.Tests
{
    public class RevitUtils_Tests
    {
        [Test]
        public void RevitUtils_Test_CurrentUser()
        {
            Console.WriteLine(RevitUtils.GetCurrentUserApplicationPluginsFolder());
            Console.WriteLine(RevitUtils.GetCurrentUserAddInFolder());
            Console.WriteLine(RevitUtils.GetCurrentUserAddInFolder(2021));
        }

        [Test]
        public void RevitUtils_Test_AllUsers()
        {
            Console.WriteLine(RevitUtils.GetAllUsersApplicationPluginsFolder());
            Console.WriteLine(RevitUtils.GetAllUsersAddInFolder());
            Console.WriteLine(RevitUtils.GetAllUsersAddInFolder(2021));
        }

        [Test]
        public void RevitUtils_Test_GetReferences()
        {
            var assemblyFile = Assembly.GetExecutingAssembly().Location;
            Assert.IsFalse(RevitUtils.TryGetRevitVersion(assemblyFile, out int revitVersion));
            Assert.That(revitVersion, Is.EqualTo(0));
        }

        [TestCase("Files/2017/RevitAddin1.dll", 2017)]
        [TestCase("Files/2018/RevitAddin1.dll", 2018)]
        [TestCase("Files/2019/RevitAddin1.dll", 2019)]
        [TestCase("Files/2020/RevitAddin1.dll", 2020)]
        [TestCase("Files/2021/RevitAddin1.dll", 2021)]
        [TestCase("Files/2022/RevitAddin1.dll", 2022)]
        [TestCase("Files/2025/RevitAddin24.dll", 2025)]
        public void RevitUtils_Test_GetReferencesRevit(string filePath, int expectedRevitVersion)
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var assemblyFile = Path.Combine(directory,filePath);
            Console.WriteLine(filePath);
            Assert.IsTrue(RevitUtils.TryGetRevitVersion(assemblyFile, out int revitVersion));
            Console.WriteLine(revitVersion);
            Assert.That(revitVersion, Is.EqualTo(expectedRevitVersion));
        }
    }
}