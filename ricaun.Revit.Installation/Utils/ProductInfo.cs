namespace ricaun.Revit.Installation.Utils
{
    /// <summary>
    /// ProductInfo
    /// </summary>
    public class ProductInfo
    {
        private readonly string productCode;
        /// <summary>
        /// ProductInfo
        /// </summary>
        /// <param name="productCode"></param>
        public ProductInfo(string productCode)
        {
            this.productCode = productCode;
        }
        /// <summary>
        /// ProductCode
        /// </summary>
        public string ProductCode => productCode;
        //public string PackageCode => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// ProductName
        /// </summary>
        public string ProductName => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// InstallLocation
        /// </summary>
        public string InstallLocation => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// InstallSource
        /// </summary>
        public string InstallSource => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// Publisher
        /// </summary>
        public string Publisher => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// VersionMajor
        /// </summary>
        public string VersionMajor => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// VersionString
        /// </summary>
        public string VersionString => ProductInfoUtils.GetProductInfo(ProductCode);
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{ProductCode}\t{ProductName}";
        }
    }
}
