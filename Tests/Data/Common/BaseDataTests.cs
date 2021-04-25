using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests.Data.Common
{
    public class BaseDataTests<TClass>
        where TClass : new()
    {
        protected TClass Obj;

        [TestInitialize] public void TestInitialize() => Obj = GetRandom.ObjectOf<TClass>();
        [TestMethod]
        public void CanCreate()
            => Assert.IsInstanceOfType(new TClass(), typeof(TClass));
        //private void isProperty<T>(string name, bool canWrite = true)
        //{
        //    var propertyInfo = obj.GetType().GetProperty(name);
        //    Assert.IsNotNull(propertyInfo, "Not found");
        //    Assert.AreEqual(typeof(T), propertyInfo.PropertyType, "Wrong type");
        //    Assert.AreEqual(true, propertyInfo.CanRead, "Cant read");
        //    Assert.AreEqual(canWrite, propertyInfo.CanWrite, "CanWrite is wrong");
        //}
        //protected void isReadWriteProperty<T>(string name)
        //{
        //    isProperty<T>(name);
        //}
        //protected void isReadOnlyProperty<T>(string name)
        //{
        //    isProperty<T>(name, false);
        //}
    }
}
