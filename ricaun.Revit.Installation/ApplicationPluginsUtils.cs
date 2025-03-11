using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;

namespace ricaun.Revit.Installation
{
    /// <summary>
    /// ApplicationPluginsUtils
    /// <code>Based: https://github.com/ricaun-io/ricaun.Revit.Github/blob/master/ricaun.Revit.Github/Services/DownloadBundleService.cs</code>
    /// </summary>
    public class ApplicationPluginsUtils
    {
        #region const
        private const string CONST_BUNDLE = ".bundle";
        private const int MutexMillisecondsTimeout = 10000;
        private const string CONST_PACKAGE_CONTENTS = "PackageContents.xml";
        #endregion

        #region Delete
        /// <summary>
        /// DeleteBundle
        /// </summary>
        /// <param name="applicationPluginsFolder"></param>
        /// <param name="bundleName"></param>
        /// <exception cref="Exception"></exception>
        public static void DeleteBundle(string applicationPluginsFolder, string bundleName)
        {
            if (bundleName.EndsWith(CONST_BUNDLE) == false)
                throw new Exception(string.Format("BundleName {0} does not end with {0}", bundleName, CONST_BUNDLE));

            using (var mutex = new Mutex(false, bundleName))
            {
                mutex.WaitOne(MutexMillisecondsTimeout);
                DeleteDirectoryAndFiles(Path.Combine(applicationPluginsFolder, bundleName));
                mutex.ReleaseMutex();
            }

            void DeleteDirectoryAndFiles(string directory)
            {
                DirectoryInfo dir = new DirectoryInfo(directory);
                if (!dir.Exists) return;
                foreach (FileInfo fi in dir.GetFiles())
                {
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
                }
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    DeleteDirectoryAndFiles(di.FullName);
                    try
                    {
                        di.Delete();
                    }
                    catch { }
                }
                try
                {
                    dir.Delete();
                }
                catch { }
            }
        }
        #endregion

        #region Download
        /// <summary>
        /// Download and unzip Bundle
        /// </summary>
        /// <param name="applicationPluginsFolder">Folder of the ApplicationPlugins or the Application.bundle</param>
        /// <param name="address"></param>
        /// <param name="downloadFileException"></param>
        /// <param name="logFileConsole"></param>
        /// <returns></returns>
        public static bool DownloadBundle(string applicationPluginsFolder, string address, Action<Exception> downloadFileException = null, Action<string> logFileConsole = null)
        {
            if (!Directory.Exists(applicationPluginsFolder))
                Directory.CreateDirectory(applicationPluginsFolder);

            var fileName = Path.GetFileName(address);
            var zipPath = Path.Combine(applicationPluginsFolder, fileName);
            var result = false;

            var bundleName = Path.GetFileNameWithoutExtension(fileName);
            using (var mutex = new Mutex(false, bundleName))
            {
                mutex.WaitOne(MutexMillisecondsTimeout);
                using (var client = new WebClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
                    client.Headers[HttpRequestHeader.UserAgent] = nameof(ApplicationPluginsUtils);
                    try
                    {
                        client.DownloadFile(new Uri(address), zipPath);
                        ExtractBundleZipToDirectory(zipPath, applicationPluginsFolder, downloadFileException, logFileConsole);
                        result = true;
                    }
                    catch (Exception ex)
                    {
                        downloadFileException?.Invoke(ex);
                    }
                    if (File.Exists(zipPath)) File.Delete(zipPath);
                }
                mutex.ReleaseMutex();
            }

            return result;
        }
        #endregion

        #region BundleZip
        private static bool IsEntryPackageContents(ZipArchiveEntry entry)
        {
            return entry.Name.Equals(CONST_PACKAGE_CONTENTS, StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// ExtractToDirectory with overwrite enable
        /// </summary>
        /// <param name="archiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        /// <param name="extractFileException"></param>
        /// <param name="logFileConsole"></param>
        private static void ExtractBundleZipToDirectory(string archiveFileName, string destinationDirectoryName, Action<Exception> extractFileException = null, Action<string> logFileConsole = null)
        {
            if (Path.GetExtension(archiveFileName) != ".zip") return;

            // If destination does not have .bundle in the end
            if (destinationDirectoryName.EndsWith(CONST_BUNDLE) == false)
                destinationDirectoryName = Path.Combine(destinationDirectoryName, Path.GetFileNameWithoutExtension(archiveFileName));

            Console.WriteLine(destinationDirectoryName);

            using (var archive = ZipFile.OpenRead(archiveFileName))
            {
                string baseDirectory = string.Empty;

                // Check if first file is inside the bundle folder, to ignore when extract.
                var firstFile = archive.Entries.FirstOrDefault(IsEntryPackageContents);
                if (firstFile is not null)
                {
                    var firstDirectory = Path.GetDirectoryName(firstFile.FullName);
                    if (firstDirectory.EndsWith(CONST_BUNDLE, StringComparison.InvariantCultureIgnoreCase))
                        baseDirectory = firstDirectory;
                }

                foreach (var file in archive.Entries.OrderBy(IsEntryPackageContents))
                {
                    var fileFullName = file.FullName.Substring(baseDirectory.Length).TrimStart('/').TrimStart('\\');

                    var completeFileName = Path.Combine(destinationDirectoryName, fileFullName);
                    var directory = Path.GetDirectoryName(completeFileName);

                    Debug.WriteLine($"{fileFullName} |\t {baseDirectory} |\t {completeFileName}");

                    logFileConsole?.Invoke($"{fileFullName} |\t {baseDirectory} |\t {completeFileName}");

                    if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
                        Directory.CreateDirectory(directory);

                    if (file.Name != "")
                    {
                        try
                        {
                            file.ExtractToFile(completeFileName, true);
                        }
                        catch (Exception ex)
                        {
                            if (extractFileException is null) throw;
                            extractFileException.Invoke(ex);
                        }
                    }
                }
            }
        }

        internal static string GetBaseDirectory(string fullPath)
        {
            if (Path.IsPathRooted(fullPath))
                return Path.GetPathRoot(fullPath);

            var baseDirectory = Path.GetDirectoryName(fullPath);
            while (!string.IsNullOrEmpty(Path.GetDirectoryName(baseDirectory)))
            {
                baseDirectory = Path.GetDirectoryName(baseDirectory);
            }
            return baseDirectory;
        }

        #endregion
    }
}