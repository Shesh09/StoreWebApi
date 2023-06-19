using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Tests
{
    public class ProductConsts
    {
        public enum Categories
        {
            Condiments = 2,
            Confections = 3,
            Dairy,
            Grains,
            Meat
        }

        public const string ProductName = "Test Product Name 1";
        public const string ProductName2 = "Test Product Name 2";
        public const string ProductName3 = "Test Product Name 3";
        public static int TestProduct = 3;
        public static int TestProduct3 = 1;
        public static int TestProduct2 = 2;
        public static int TestSupplierId = 4;
        public static int TestSupplierId2 = 3;
        public static int TestSupplierId3 = 2;
        public static decimal TestUnitPrice = 100.23M;
    }
}
