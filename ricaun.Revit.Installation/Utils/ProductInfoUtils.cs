using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ricaun.Revit.Installation.Utils
{
    /// <summary>
    /// ProductInfoUtils
    /// </summary>
    public class ProductInfoUtils
    {
        #region Msi private
        // Source Info: https://learn.microsoft.com/en-us/windows/win32/api/msi/nf-msi-msigetproductinfoa
        // Source Info: https://github.com/tpn/winsdk-10/blob/master/Include/10.0.10240.0/um/Msi.h

        private const int MAX_FEATURE_CHARS = 38;   // maximum chars in feature name (same as string GUID)
        private const int MAX_PRODUCT_INFO_CHARS = 250;

        internal const string INSTALLPROPERTY_INSTALLLOCATION = "InstallLocation";
        internal const string INSTALLPROPERTY_INSTALLSOURCE = "InstallSource";
        internal const string INSTALLPROPERTY_PUBLISHER = "Publisher";
        internal const string INSTALLPROPERTY_VERSIONMAJOR = "VersionMajor";
        internal const string INSTALLPROPERTY_VERSIONSTRING = "VersionString";
        internal const string INSTALLPROPERTY_PRODUCTNAME = "ProductName";

        [DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MsiEnumProducts(int iProductIndex, string lpProductBuf);
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        private static extern int MsiEnumClients(string szComponent, int iProductIndex, string lpProductBuf);
        [DllImport("msi.dll", CharSet = CharSet.Unicode)]
        private static extern int MsiGetProductInfo(string product, string property, string valueBuf, out int len);
        #endregion
        internal static IEnumerable<string> GetProductCodes()
        {
            int i = 0;
            string productCode = new string(' ', MAX_FEATURE_CHARS);
            while (MsiEnumProducts(i++, productCode) == 0)
            {
                yield return new string(productCode.ToCharArray());
            }
        }
        internal static IEnumerable<string> GetProductCodes(string szComponent)
        {
            int i = 0;
            string productCode = new string(' ', MAX_FEATURE_CHARS);
            while (MsiEnumClients(szComponent, i++, productCode) == 0)
            {
                yield return new string(productCode.ToCharArray());
            }
        }
        internal static string GetProductInfo(string productCode, [CallerMemberName] string property = null)
        {
            int len = MAX_PRODUCT_INFO_CHARS;
            string valueBuf = new string(' ', len);
            MsiGetProductInfo(productCode, property, valueBuf, out len);
            return valueBuf.Substring(0, len);
        }

        internal static ProductInfo ToProductInfo(string productCode)
        {
            return new ProductInfo(productCode);
        }

        /// <summary>
        /// GetProductInfos using msi.dll
        /// </summary>
        /// <param name="szComponent"></param>
        /// <returns></returns>
        public static IEnumerable<ProductInfo> GetProductInfos(string szComponent = null)
        {
            if (szComponent is null)
                return GetProductCodes().Select(ToProductInfo);

            return GetProductCodes(szComponent).Select(ToProductInfo);
        }
    }
}
