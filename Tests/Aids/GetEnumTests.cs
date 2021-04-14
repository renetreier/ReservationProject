using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Aids {
    [TestClass]
    public class GetEnumTests {
        private enum testEnum {
            Undefined = 0,
            First = 1,
            Second = 123
        }
        [TestMethod] public void CountTest() 
            => Assert.AreEqual(3, GetEnum.Count<testEnum>());
        [TestMethod] public void CountTestByType()
            => Assert.AreEqual(3, GetEnum.Count(typeof(testEnum)));
 
        [TestMethod] public void ValueByIndexTest()
            => Assert.AreEqual(testEnum.First, GetEnum.ValueByIndex<testEnum>(1));
        [TestMethod] public void ValueByIndexTestByType()
            => Assert.AreEqual(testEnum.Second, GetEnum.ValueByIndex(typeof(testEnum), 2));
        
        [TestMethod] public void ValueByValueTest()
            => Assert.AreEqual(testEnum.First, GetEnum.ValueByValue<testEnum>(1));
        [TestMethod] public void ValueByValueTestByType()
            => Assert.AreEqual(testEnum.Second, GetEnum.ValueByValue(typeof(testEnum), 123));

        [TestMethod] public void CountTestWrongType()
            => Assert.AreEqual(-1, GetEnum.Count<string>());
        [TestMethod] public void CountTestByTypeWrongType()
            => Assert.AreEqual(-1, GetEnum.Count(typeof(int)));

        [TestMethod] public void ValueByIndexTestWrongIndex()
            => Assert.AreEqual(testEnum.Undefined, GetEnum.ValueByIndex<testEnum>(100));
        [TestMethod] public void ValueByIndexTestByTypeWrongIndex()
            => Assert.AreEqual(testEnum.Undefined, GetEnum.ValueByIndex(typeof(testEnum), 100));

        [TestMethod] public void ValueByValueTestWrongIndex()
            => Assert.AreEqual(testEnum.Undefined, GetEnum.ValueByValue<testEnum>(100));
        [TestMethod] public void ValueByValueTestByTypeWrongIndex()
            => Assert.AreEqual(testEnum.Undefined, GetEnum.ValueByValue(typeof(testEnum), 100));

        [TestMethod] public void ValueByIndexTestWrongType()
            => Assert.AreEqual(null, GetEnum.ValueByIndex<string>(100));
        [TestMethod] public void ValueByIndexTestByTypeWrongType()
            => Assert.AreEqual(null, GetEnum.ValueByIndex(typeof(string), 100));

        [TestMethod] public void ValueByValueTestWrongType()
            => Assert.AreEqual(0, GetEnum.ValueByValue<int>(100));
        [TestMethod] public void ValueByValueTestByTypeWrongType()
            => Assert.AreEqual(0, GetEnum.ValueByValue(typeof(int), 100));

    }
}
