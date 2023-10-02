using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

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

            DeleteDirectoryAndFiles(Path.Combine(applicationPluginsFolder, bundleName));

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
        /// <returns></returns>
        public static bool DownloadBundle(string applicationPluginsFolder, string address, Action<Exception> downloadFileException = null)
        {
            var task = Task.Run(async () =>
            {
                return await DownloadBundleAsync(applicationPluginsFolder, address, downloadFileException);
            });
            return task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Download and unzip Bundle Async
        /// </summary>
        /// <param name="applicationPluginsFolder">Folder of the ApplicationPlugins or the Application.bundle</param>
        /// <param name="address"></param>
        /// <param name="downloadFileException"></param>
        /// <returns></returns>
        public static async Task<bool> DownloadBundleAsync(string applicationPluginsFolder, string address, Action<Exception> downloadFileException = null)
        {
            if (!Directory.Exists(applicationPluginsFolder))
                Directory.CreateDirectory(applicationPluginsFolder);

            var fileName = Path.GetFileName(address);
            var zipPath = Path.Combine(applicationPluginsFolder, fileName);
            var result = false;

            using (var client = new WebClient())
            {
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;
                client.Headers[HttpRequestHeader.UserAgent] = nameof(ApplicationPluginsUtils);
                try
                {
                    await client.DownloadFileTaskAsync(new Uri(address), zipPath);
                    ExtractBundleZipToDirectory(zipPath, applicationPluginsFolder);
                    result = true;
                }
                catch (Exception ex)
                {
                    downloadFileException?.Invoke(ex);
                }
                if (File.Exists(zipPath)) File.Delete(zipPath);
            }

            return result;
        }
        #endregion

        #region BundleZip
        /// <summary>
        /// ExtractToDirectory with overwrite enable
        /// </summary>
        /// <param name="archiveFileName"></param>
        /// <param name="destinationDirectoryName"></param>
        private static void ExtractBundleZipToDirectory(string archiveFileName, string destinationDirectoryName)
        {
            if (Path.GetExtension(archiveFileName) != ".zip") return;

            // If destination does not have .bundle in the end
            if (destinationDirectoryName.EndsWith(CONST_BUNDLE) == false)
                destinationDirectoryName = Path.Combine(destinationDirectoryName, Path.GetFileNameWithoutExtension(archiveFileName));

            using (var archive = ZipFile.OpenRead(archiveFileName))
            {
                string baseDirectory = null;
                foreach (var file in archive.Entries)
                {
                    if (baseDirectory == null)
                        baseDirectory = Path.GetDirectoryName(file.FullName);
                    if (baseDirectory.EndsWith(CONST_BUNDLE) == false)
                        baseDirectory = "";

                    var fileFullName = file.FullName.Substring(baseDirectory.Length).TrimStart('/');

                    var completeFileName = Path.Combine(destinationDirectoryName, fileFullName);
                    var directory = Path.GetDirectoryName(completeFileName);

                    Debug.WriteLine($"{fileFullName} |\t {baseDirectory} |\t {completeFileName}");

                    if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
                        Directory.CreateDirectory(directory);

                    if (file.Name != "")
                        file.ExtractToFile(completeFileName, true);
                }
            }

        }
        #endregion
    }
}