
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Aids {
    [TestClass]
    public class CopyTests {
        private class testClass1 {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime DoB { get; set; }
        }
        private class testClass2 {
            public string AdministratorId { get; set; }
            public string Name { get; set; }
            public DateTime DoB { get; set; }
        }

        [TestMethod] public void MemberTest() {
            var x = GetRandom.ObjectOf<testClass1>();
            var y = new testClass2();
            y = Copy.Members(x, y);
            Assert.AreEqual(x.Name, y.Name);
            Assert.AreEqual(x.DoB, y.DoB);
        }
    }
}
