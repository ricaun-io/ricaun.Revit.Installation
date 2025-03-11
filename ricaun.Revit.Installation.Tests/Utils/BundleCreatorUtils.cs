using System;
using System.IO;
using System.IO.Compression;

namespace ricaun.Revit.Installation.Tests.Utils
{
    public static class BundleCreatorUtils
    {
        public static string CreateBundleZip(string projectName, bool includeBundleDirectory = true, bool includeContents = true)
        {
            var bundleFileName = $"{projectName}.bundle";
            var zipFileName = $"{bundleFileName}.zip";
            var tempPath = Path.Combine(Path.GetTempPath(), "BundleCreatorUtils");

            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);

            Directory.CreateDirectory(tempPath);

            var zipFilePath = Path.Combine(tempPath, zipFileName);
            var sourceBundlePath = Path.Combine(tempPath, bundleFileName);
            Directory.CreateDirectory(sourceBundlePath);

            var packageContentsFile = Path.Combine(sourceBundlePath, "PackageContents.xml");
            File.WriteAllText(packageContentsFile, string.Empty);

            if (includeContents)
            {
                var contentsFolder = Path.Combine(sourceBundlePath, "Contents");
                Directory.CreateDirectory(contentsFolder);
                var contentFile = Path.Combine(contentsFolder, "File.xml");
                File.WriteAllText(contentFile, string.Empty);
            }

            ZipFile.CreateFromDirectory(sourceBundlePath, zipFilePath, CompressionLevel.NoCompression, includeBundleDirectory);

            return zipFilePath;
        }
    }
}
