using NUnit.Framework;
using ricaun.Revit.Installation.Utils;
using System;
using System.Linq;

namespace ricaun.Revit.Installation.Tests
{
    public class ProductInfoUtils_Tests
    {
        [Test]
        public void Test_GetProductCodes()
        {
            var productCodes = ProductInfoUtils.GetProductCodes();
            Assert.IsTrue(productCodes.Any());
        }

        [Test]
        public void Test_GetProductInfos_IsNotEqual()
        {
            var productInfos = ProductInfoUtils.GetProductInfos().OrderBy(e=>e.ToString());

            var productInfos1 = productInfos.FirstOrDefault();
            var productInfos2 = productInfos.Skip(1).FirstOrDefault();

            Assert.AreNotEqual(productInfos1.ToString(), productInfos2.ToString());
        }

        [Test]
        public void GetProductInfo_Show()
        {
            var productInfos = ProductInfoUtils.GetProductInfos();
            foreach (var productInfo in productInfos.ToArray().Take(1))
            {
                ShowProperties(productInfo);
                Console.WriteLine();
            }
        }

        [TestCase("Autodesk")]
        public void GetProductInfo_Publisher(string publisher)
        {
            var productInfos = ProductInfoUtils.GetProductInfos();
            foreach (var productInfo in productInfos.Where(e => e.Publisher == publisher))
            {
                Console.WriteLine(productInfo);
            }
        }

        [TestCase("Autodesk", "Revit")]
        [TestCase("Autodesk", "AutoCAD")]
        public void GetProductInfo_PublisherProductName(string publisher, string productNameStarts)
        {
            var productInfos = ProductInfoUtils.GetProductInfos();
            foreach (var productInfo in productInfos.Where(e => e.Publisher == publisher && e.ProductName.StartsWith(productNameStarts)))
            {
                Console.WriteLine($"{productInfo}");
                ShowProperties(productInfo);
                //Console.WriteLine();
            }
        }

        [TestCase("705C0D862004")] // Revit
        [TestCase("CF3F3A09B77D")] // AutoCAD
        public void GetProductInfo_ProductCode(string productCodeContains)
        {
            var productInfos = ProductInfoUtils.GetProductInfos();
            foreach (var productInfo in productInfos.Where(e => e.ProductCode.Contains(productCodeContains)).OrderBy(e => e.ProductName))
            {
                Console.WriteLine(productInfo);
            }
        }

        [TestCase("{DF7D485F-B8BA-448E-A444-E6FB1C258912}")]
        [TestCase("{1C685B70-BF48-4E33-BCB8-32E56CF31A2C}")]
        public void GetProductInfo_Component(string szComponent)
        {
            var productInfos = ProductInfoUtils.GetProductInfos(szComponent);
            foreach (var productInfo in productInfos.OrderBy(e=>e.ProductName))
            {
                Console.WriteLine(productInfo);
            }
        }

        private static void ShowProperties(object value)
        {
            foreach (var propertyInfo in value.GetType().GetProperties())
            {
                Console.WriteLine($"\t{propertyInfo.Name}:\t{propertyInfo.GetValue(value)}");
            }
        }

        [Test]
        public void Internal_GetProductInfo()
        {
            var productCodes = ProductInfoUtils.GetProductCodes();
            foreach (var productCode in productCodes)
            {
                var productName = ProductInfoUtils.GetProductInfo(productCode, ProductInfoUtils.INSTALLPROPERTY_PRODUCTNAME);
                Console.WriteLine($"{productCode}\t{productName}");
            }
        }
    }
}