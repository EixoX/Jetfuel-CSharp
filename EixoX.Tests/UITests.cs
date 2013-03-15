using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EixoX.Html;
using System.IO;
using System.Diagnostics;

namespace EixoX.Tests
{
    [TestClass]
    public class UITests
    {
        [TestMethod]
        public void UITest()
        {
            TestPerson model = new TestPerson() 
            {
                FirstName = "Guilherme"
            };

            HtmlPresenter<TestPerson> presenter = HtmlPresenter<TestPerson>.GetInstance(1033);
            StringBuilder sb = new StringBuilder();

            presenter.Render(new StringWriter(sb), model, true);

            Trace.WriteLine(sb);
        }
    }
}