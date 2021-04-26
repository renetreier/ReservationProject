using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests
{
    public abstract class ClassTests<TClass, TBaseClass> 
        :AbstractClassTests<TClass, TBaseClass> 
        where TClass : class, new()
        where TBaseClass : class {
        [TestMethod] public override void IsAbstractTest() => isFalse(type.IsAbstract);
        protected override TClass getObject() => GetRandom.ObjectOf<TClass>();
        [TestMethod]
        public virtual void CanCreate()
            => Assert.IsInstanceOfType(new TClass(), typeof(TClass));
    }
}