using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace ricaun.Revit.Installation
{
    /// <summary>
    /// RevitInstallationUtils
    /// </summary>
    public static class RevitInstallationUtils
    {
        /// <summary>
        /// RevitInstallation[] - InstalledRevit
        /// </summary>
        public static RevitInstallation[] InstalledRevit
        {
            get
            {
                return _installedRevit ?? (_installedRevit = GetRevitInstallationCodes().ToArray());
            }
        }

        #region private
        private static RevitInstallation[] _installedRevit;
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        private static extern uint MsiEnumClients(string szComponent, uint iProductIndex, string lpProductBuf);
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        private static extern int MsiGetProductInfo(string product, string property, string valueBuf, out int len);
        private static string MsiGetProductInfo(string product, string property)
        {
            int len = 250;
            string valueBuf = new string(' ', len);
            MsiGetProductInfo(product, property, valueBuf, out len);
            return valueBuf.Substring(0, len);
        }
        private static IEnumerable<string> GetInstalledRevitProductCodes()
        {
            List<string> productCodes = new List<string>();
            string code = new string(' ', 38);
            string component = "{DF7D485F-B8BA-448E-A444-E6FB1C258912}";
            uint num = 1u;
            if (MsiEnumClients(component, 0u, code) != 0)
            {
                return productCodes;
            }

            uint iProductIndex;
            do
            {
                productCodes.Add(string.Copy(code));
                iProductIndex = num;
                num++;
            }
            while (MsiEnumClients(component, iProductIndex, code) == 0);
            return productCodes;
        }
        private static IEnumerable<RevitInstallation> GetRevitInstallationCodes()
        {
            var regex = new Regex("^(\\{{0,1}(7346B4A[0-9a-fA-F])-(?<Majorversion>([0-9a-fA-F]){2})(?<Subversion>([0-9a-fA-F]){2})-(?<Discipline>([0-9a-fA-F]){2})(?<Platform>([0-9a-fA-F]){1})[0-9a-fA-F]-(?<Language>([0-9a-fA-F]){4})-705C0D862004\\}{0,1})$");
            return GetInstalledRevitProductCodes().Select(code =>
            {
                Match match = regex.Match(code);
                if (!match.Success)
                    return null;

                var version = int.Parse(match.Result("${Majorversion}")) + 2000;
                var installLocation = MsiGetProductInfo(code, "InstallLocation");

                return new RevitInstallation(installLocation, version);
            }).OfType<RevitInstallation>().OrderBy(e => e.Version);
        }
        #endregion
    }
}
