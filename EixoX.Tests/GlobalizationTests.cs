using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EixoX.Globalization;

namespace EixoX.Tests
{
    [TestClass]
    public class GlobalizationTests
    {
        [TestMethod]
        public void GlobalizationLabelTest()
        {
            GlobalizationAspect exampleGlobalization = GlobalizationAspect<Example>.Instance;

            string item = exampleGlobalization["FirstName"].GetItem("grayName", "pt-BR");
            Assert.IsNotNull(item);
        }
    }
}
