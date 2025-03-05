using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ricaun.Revit.Installation.Utils
{
    internal static class FinalPathNameExtension
    {
        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        private const string DLL_NAME = "Kernel32.dll";
        private const uint FILE_READ_EA = 0x0008;
        private const uint FILE_FLAG_BACKUP_SEMANTICS = 0x2000000;

        [DllImport(DLL_NAME, SetLastError = true, CharSet = CharSet.Auto)]
        private static extern uint GetFinalPathNameByHandle(
            IntPtr hFile,
            [MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpszFilePath,
            uint cchFilePath,
            uint dwFlags);

        [DllImport(DLL_NAME, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport(DLL_NAME, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(
                [MarshalAs(UnmanagedType.LPTStr)] string filename,
                [MarshalAs(UnmanagedType.U4)] uint access,
                [MarshalAs(UnmanagedType.U4)] FileShare share,
                IntPtr securityAttributes, // optional SECURITY_ATTRIBUTES struct or IntPtr.Zero
                [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
                [MarshalAs(UnmanagedType.U4)] uint flagsAndAttributes,
                IntPtr templateFile);

        public static string GetFinalPathName(string path, bool removeLongPathPrefix = true)
        {
            const int bufferSize = 1024;

            var fileHandle = CreateFile(path,
                FILE_READ_EA,
                FileShare.ReadWrite | FileShare.Delete,
                IntPtr.Zero,
                FileMode.Open,
                FILE_FLAG_BACKUP_SEMANTICS,
                IntPtr.Zero);

            if (fileHandle == INVALID_HANDLE_VALUE)
                throw new FileNotFoundException("File not found.", path);

            try
            {
                var fileStringBuilder = new StringBuilder(bufferSize);
                var res = GetFinalPathNameByHandle(fileHandle, fileStringBuilder, bufferSize, 0);

                if (res == 0)
                    throw new FileNotFoundException("FinalPathNameByHandle fail.", path);

                var finalPath = fileStringBuilder.ToString();

                if (removeLongPathPrefix)
                {
                    // Remove the \\?\ prefix for the long path
                    finalPath = finalPath.StartsWith(@"\\?\") ? finalPath.Substring(4) : finalPath;
                }

                return finalPath;
            }
            finally
            {
                CloseHandle(fileHandle);
            }
        }
    }
}
