using System.Collections.Generic;
using System.Linq;
using ricaun.Revit.Installation.Utils;

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
                return _installedRevit ?? (_installedRevit = GetRevitInstallations());
            }
        }

        /// <summary>
        /// Get RevitInstallations using a <paramref name="componentGuid"/> or all RevitInstallations
        /// </summary>
        /// <param name="componentGuid"></param>
        /// <returns></returns>
        public static RevitInstallation[] GetRevitInstallations(string componentGuid = null)
        {
            return GetRevitInstallationCodes(componentGuid).ToArray();
        }

        #region private
        private static RevitInstallation[] _installedRevit;

        private static bool IsRevit(ProductInfo productInfo)
        {
            return productInfo.ProductName.StartsWith("Revit") && productInfo.ProductCode.EndsWith("705C0D862004}");
        }
        private static RevitInstallation ToRevitInstallation(ProductInfo productInfo)
        {
            var version = 2000 + int.Parse(productInfo.VersionMajor);
            return new RevitInstallation(productInfo.InstallLocation, version);
        }
        private static IEnumerable<RevitInstallation> GetRevitInstallationCodes(string componentGuid = null)
        {
            var productInfos = ProductInfoUtils.GetProductInfos(componentGuid);
            return productInfos
                .Where(IsRevit)
                .Select(ToRevitInstallation)
                .OrderBy(e => e.Version);
        }
        #endregion
    }
}
