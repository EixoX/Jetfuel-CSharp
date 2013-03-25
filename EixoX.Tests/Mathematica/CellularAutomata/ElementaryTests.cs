using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EixoX.Mathematica.CellularAutomata;

namespace EixoX.Tests.Mathematica.CellularAutomata
{
    [TestClass]
    public class ElementaryTests
    {
        [TestMethod]
        public void ElementaryRuleTest()
        {

            ElementaryRule rule30 = new ElementaryRule(30);

            Assert.AreEqual(false, rule30.Next(false, false, false), "Failed for bit 0");
            Assert.AreEqual(true, rule30.Next(false, false, true), "Failed for bit 1");
            Assert.AreEqual(true, rule30.Next(false, true, false), "Failed for bit 2");
            Assert.AreEqual(true, rule30.Next(false, true, true), "Failed for bit 3");
            Assert.AreEqual(true, rule30.Next(true, false, false), "Failed for bit 4");
            Assert.AreEqual(false, rule30.Next(true, false, true), "Failed for bit 5");
            Assert.AreEqual(false, rule30.Next(true, true, false), "Failed for bit 6");
            Assert.AreEqual(false, rule30.Next(true, true, true), "Failed for bit 7");
        }

        [TestMethod]
        public void ElementaryIntLatticeTest()
        {
            ElementaryIntLattice lattice = new ElementaryIntLattice(30);

            Assert.AreEqual(false, lattice[0], "Failed for bit 0");
            Assert.AreEqual(true, lattice[1], "Failed for bit 1");
            Assert.AreEqual(true, lattice[2], "Failed for bit 2");
            Assert.AreEqual(true, lattice[3], "Failed for bit 3");
            Assert.AreEqual(true, lattice[4], "Failed for bit 4");
            Assert.AreEqual(false, lattice[5], "Failed for bit 5");
            Assert.AreEqual(false, lattice[6], "Failed for bit 6");
            Assert.AreEqual(false, lattice[7], "Failed for bit 7");
            Assert.AreEqual(false, lattice[8], "Failed for bit 8");
        }
    }
}
