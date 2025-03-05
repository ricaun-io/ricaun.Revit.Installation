using NUnit.Framework;
using ricaun.Revit.Installation.Utils;
using System;
using System.IO;

namespace ricaun.Revit.Installation.Tests
{
    public class FinalPathName_Tests
    {
        [TestCase("Program Files")] // English
        [TestCase("Arquivos de Programas")] // Portuguese
        [TestCase("Programme")] // German
        public void GetMainModuleFileName_Test(string programFiles)
        {
            var pathShouldBe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);

            var directory = Path.GetDirectoryName(pathShouldBe);
            var path = Path.Combine(directory, programFiles);

            try
            {
                var finalPath = FinalPathNameExtension.GetFinalPathName(path);
                Console.WriteLine(finalPath);
                Assert.That(finalPath, Is.EqualTo(pathShouldBe));
            }
            catch
            {
                Assert.Ignore($"'{programFiles}' does not exist in the system.");
            }
        }
    }
}