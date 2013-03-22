using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EixoX.Interceptors;

namespace EixoX.Tests
{
     [TestClass]
    public class InterceptorTests
    {
         [TestMethod]
         public void CapitalizationTest()
         {
             string item = "rodrigo    fernandes .,po po po portela;";
             string exp = "Rodrigo    Fernandes .,Po Po Po Portela;";
             Assert.AreEqual<string>(exp, Capitalize.Intercept(item));
         }
    }
}
