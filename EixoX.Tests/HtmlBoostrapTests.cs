using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EixoX.Html;

namespace EixoX.Tests
{
    [TestClass]
    public class HtmlBoostrapTests
    {
        [TestMethod]
        public void TestPerson()
        {
            BoostrapPresenter<TestPerson> presenter = BoostrapPresenter<TestPerson>.GetInstance(1033);
            TestPerson person  = new Tests.TestPerson(){
                FirstName = "Rodrigo",
                LastName = "Portela"
            };

            presenter.Render(Console.Out, person, true);
        }
    }
}
