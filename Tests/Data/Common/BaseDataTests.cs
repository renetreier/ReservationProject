
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Tests.Data.Common
{
    [TestClass]
    public class BaseDataTests : AbstractClassTests<BaseData, UniqueItem>
    {
        private class TestClass : BaseData { }
        protected override BaseData GetObject() => GetRandom.ObjectOf<TestClass>();
        [TestMethod] public void RowVersionTest() => IsReadWriteProperty<byte[]>();



    }
}
