
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Aids {
    [TestClass]
    public class CreateNewTests {
        internal class testClassStr {
            public testClassStr(string s) { strField = s;}
            protected internal readonly string strField;
        }
        internal class testClassInt {
            public testClassInt(int i) { intField = i; }
            protected internal readonly int intField;
        }
        [TestMethod] public void InstanceTest() {
            testCreate<testClassStr>();
            testCreate<testClassInt>();
            testCreate<CreateNewTests>();
        }
        [DataRow(typeof(testClassStr))]
        [DataRow(typeof(testClassInt))]
        [DataRow(typeof(CreateNewTests))]
        [DataTestMethod]
        public void InstanceTestBtType(Type t) => testCreate(t);
        private static void testCreate(Type t) {
            var o = CreateNew.Instance(t);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, t);
        }
        private static void testCreate<T>() {
            var o = CreateNew.Instance<T>();
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(T));
        }
    }
}
