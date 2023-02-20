using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace ricaun.Revit.Installation.Tests
{
    public class RevitInstallationUtils_Tests
    {
        [Test]
        public void InstalledRevit_Test_Show()
        {
            foreach (var installedRevit in RevitInstallationUtils.InstalledRevit)
            {
                Console.WriteLine(installedRevit);
                foreach (var process in installedRevit.GetProcesses())
                {
                    Console.WriteLine($"\t {process} {process.Id}");
                }
            }
        }

        [Test]
        public void InstalledRevit_Test_Start()
        {
            var InstalledRevits = RevitInstallationUtils.InstalledRevit;
            var installedRevit = InstalledRevits.LastOrDefault();
            //foreach (var installedRevit in InstalledRevits)
            if (installedRevit is not null)
            {
                if (installedRevit.TryGetProcess(out Process process) == false)
                {
                    Console.WriteLine($"{installedRevit}: Start");
                    var forceToExited = false;
                    process = installedRevit.Start();
                    process.ErrorDataReceived += (s, e) =>
                    {
                        Console.WriteLine(e.Data);
                        forceToExited = true;
                    };
                    process.Exited += (s, e) =>
                    {
                        forceToExited = true;
                    };

                    for (int i = 0; i < 60; i++)
                    {
                        Console.WriteLine($"{installedRevit}: Wait {i}");
                        Thread.Sleep(1000);
                        if (forceToExited) break;
                    }

                    if (!process.HasExited)
                    {
                        process.Kill();
                        Console.WriteLine($"{installedRevit}: Kill");
                    }

                    //break;
                }
            }
        }
    }
}