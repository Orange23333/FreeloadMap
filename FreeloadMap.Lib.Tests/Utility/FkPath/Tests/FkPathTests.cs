using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using FreeloadMap.Lib.Utility;

namespace FreeloadMap.Lib.Tests.Utility.FkPath.Tests
{
    [TestFixture(Author = "Orange233",
        Category = "FreeloadMap.Lib.Utility.FkPath",
        Description = "测试FreeloadMap.Lib.Utility.FkPath类。",
        TestName = "FreeloadMap.Lib.Utility.FkPath",
        TestOf = typeof(FreeloadMap.Lib.Utility.FkPath))]
    public class FkPathTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test(Author = "Orange233",
            Description = "测试Combine的childPath参数为Windows绝对路径时的情况。")]
        public void Combine_1()
        {
            Assert.Pass();
        }
    }
}
