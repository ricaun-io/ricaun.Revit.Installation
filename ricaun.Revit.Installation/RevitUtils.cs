using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ricaun.Revit.Installation
{
    /// <summary>
    /// RevitUtils
    /// </summary>
    public static class RevitUtils
    {
        private const string RevitAPIName = "RevitAPI";
        private const string RevitAddInFolder = "\\Autodesk\\Revit\\AddIns\\";
        private const string RevitApplicationPluginsFolder = "\\Autodesk\\ApplicationPlugins\\";

        #region Revit Folder
        /// <summary>
        /// Get AllUsers AddIn Folder using <paramref name="revitVersion"/>
        /// </summary>
        /// <param name="revitVersion"></param>
        /// <returns></returns>
        public static string GetAllUsersAddInFolder(int revitVersion)
        {
            return GetAllUsersAddInFolder() + revitVersion;
        }
        /// <summary>
        /// Get AllUsers AddIn Folder
        /// </summary>
        /// <returns></returns>
        public static string GetAllUsersAddInFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + RevitAddInFolder;
        }
        /// <summary>
        /// Get CurrentUser AddIn Folder using <paramref name="revitVersion"/>
        /// </summary>
        /// <param name="revitVersion"></param>
        /// <returns></returns>
        public static string GetCurrentUserAddInFolder(int revitVersion)
        {
            return GetCurrentUserAddInFolder() + revitVersion;
        }
        /// <summary>
        /// Get CurrentUser AddIn Folder
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserAddInFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + RevitAddInFolder;
        }
        /// <summary>
        /// Get AllUsers ApplicationPlugins Folder
        /// </summary>
        /// <returns></returns>
        public static string GetAllUsersApplicationPluginsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + RevitApplicationPluginsFolder;
        }
        /// <summary>
        /// Get CurrentUser ApplicationPlugins Folder
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentUserApplicationPluginsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + RevitApplicationPluginsFolder;
        }
        #endregion

        #region Revit Version File
        /// <summary>
        /// TryGetRevitVersion using the RevitAPI ReferencedAssemblies
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="revitVersion"></param>
        /// <returns></returns>
        public static bool TryGetRevitVersion(string assemblyFile, out int revitVersion)
        {
            if (File.Exists(assemblyFile) == false)
                throw new FileNotFoundException();

            revitVersion = 0;

            var assemblyTest = TryReflectionOnlyLoad(assemblyFile);

            var revit = assemblyTest.GetReferencedAssemblies()
                .FirstOrDefault(e => e.Name.StartsWith(RevitAPIName));

            if (revit == null) return false;

            var version = revit.Version.Major;
            if (version < 2000) version += 2000;

            revitVersion = version;

            return true;
        }

        private static Assembly TryReflectionOnlyLoad(string assemblyFile)
        {
            var assemblyName = AssemblyName.GetAssemblyName(assemblyFile);
            var domain = AppDomain.CurrentDomain;
            var assembly = domain.ReflectionOnlyGetAssemblies()
                .FirstOrDefault(e => e.GetName().Name == assemblyName.Name);

            assembly = assembly ?? Assembly.ReflectionOnlyLoad(File.ReadAllBytes(assemblyFile));
            return assembly;
        }
        #endregion
    }
}