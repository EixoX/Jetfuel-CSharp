using System;
using EixoX.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EixoX.Tests
{
    [TestClass]
    public class GlobalizationTests
    {
        [TestMethod]
        public void GlobalizationLabelTest()
        {
            GlobalizationAspect exampleGlobalization = GlobalizationAspect<Example>.Instance;

            string item = exampleGlobalization["FirstName"].GetTerm("label", "pt-BR");
            Assert.IsNotNull(item);
        }
    }
}
