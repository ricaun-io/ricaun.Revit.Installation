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
        public void InstalledRevit_Test_Count()
        {
            foreach (var installedRevit in RevitInstallationUtils.InstalledRevit)
            {
                var count = RevitInstallationUtils.InstalledRevit.Count(e => e.Version == installedRevit.Version);
                Console.WriteLine($"{installedRevit} : {count}");
                Assert.That(count, Is.EqualTo(1));
            }
        }

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
        public void InstalledRevit_Test_Show_NoCompornent()
        {
            foreach (var installedRevit in RevitInstallationUtils.GetRevitInstallations())
            {
                Console.WriteLine(installedRevit);
                foreach (var process in installedRevit.GetProcesses())
                {
                    Console.WriteLine($"\t {process} {process.Id}");
                }
            }
        }

        [Test]
        [Explicit]
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

                    Thread.Sleep(5000);
                }
            }
        }

        [Test]
        [Explicit]
        public void InstalledRevit_Test_StartWithJornal()
        {
            var InstalledRevits = RevitInstallationUtils.InstalledRevit;
            var installedRevit = InstalledRevits.LastOrDefault();
            if (installedRevit is not null)
            {
                if (installedRevit.TryGetProcess(out Process process) == false)
                {
                    string workingDirectory = Path.Combine(Path.GetTempPath(), "_RevitInstallation_Test_");
                    Directory.CreateDirectory(workingDirectory);
                    Console.WriteLine($"{installedRevit}: StartWithJornal {workingDirectory}");
                    var forceToExited = false;
                    process = installedRevit.StartWithJornal(workingDirectory);
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

                    Thread.Sleep(5000);
                }
            }
        }
    }
}