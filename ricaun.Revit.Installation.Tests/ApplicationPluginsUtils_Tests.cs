using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ricaun.Revit.Installation.Tests
{
    public class ApplicationPluginsUtils_Tests
    {
        [Test]
        public async Task ApplicationPluginsUtils_Test_Download_Async()
        {
            var projectName = "RevitAddin.DA.Tester";
            var bundleUrl = $@"https://github.com/ricaun-io/{projectName}/releases/latest/download/{projectName}.bundle.zip";

            var applicationPluginsFolder = RevitUtils.GetCurrentUserApplicationPluginsFolder();
            var bundleName = Path.GetFileNameWithoutExtension(bundleUrl);

            Console.WriteLine($"DownloadBundle: {bundleName}");

            var tasks = new List<Task>();
            for (int i = 0; i < 5; i++)
            {
                var task = Task.Run(async () =>
                {
                    await Task.Delay(0);
                    ApplicationPluginsUtils.DownloadBundle(applicationPluginsFolder, bundleUrl, (e) =>
                    {
                        Console.WriteLine(e);
                        Assert.Fail(e.Message);
                    }, (log) => {
                        //Console.WriteLine(log);
                    });
                });
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }

            [Test]
        public void ApplicationPluginsUtils_Test_Download()
        {
            var projectName = "RevitAddin.DA.Tester";
            var bundleUrl = $@"https://github.com/ricaun-io/{projectName}/releases/latest/download/{projectName}.bundle.zip";

            var applicationPluginsFolder = RevitUtils.GetCurrentUserApplicationPluginsFolder();
            var bundleName = Path.GetFileNameWithoutExtension(bundleUrl);

            Console.WriteLine($"DownloadBundle: {bundleName}");

            ApplicationPluginsUtils.DownloadBundle(applicationPluginsFolder, bundleUrl, (e) =>
            {
                Console.WriteLine(e);
                Assert.Fail(e.Message);
            }, (log) => {
                Console.WriteLine(log);
            });

            Console.WriteLine($"Bundle Exists: {Directory.Exists(Path.Combine(applicationPluginsFolder, bundleName))}");
            Assert.IsTrue(Directory.Exists(Path.Combine(applicationPluginsFolder, bundleName)));

            Thread.Sleep(1000);

            ApplicationPluginsUtils.DeleteBundle(applicationPluginsFolder, bundleName);
            Console.WriteLine($"Bundle Exists: {Directory.Exists(Path.Combine(applicationPluginsFolder, bundleName))}");
        }
    }
}