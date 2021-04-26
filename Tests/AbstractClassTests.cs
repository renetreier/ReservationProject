using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests {
    public abstract class AbstractClassTests<TClass, TBaseClass>: StaticClassTests
    where TClass: class 
    where TBaseClass: class {
        protected TClass obj;
        [TestInitialize] public virtual void TestInitialize() {
            type = typeof(TClass);
            obj = getObject();
        }
        protected virtual TClass getObject() => null;
        [TestMethod] public virtual void BaseClassTest() => 
            areEqual(typeof(TBaseClass), typeof(TClass).BaseType); 
        [TestMethod] public override void IsStaticTest() => isFalse(type.IsAbstract && type.IsClass && type.IsSealed);
        [TestMethod] public virtual void IsSealedTest() => isFalse(type.IsSealed);
        [TestMethod] public virtual void IsAbstractTest() => isTrue(type.IsAbstract);
        [TestMethod] public virtual void IsClassTest() => isTrue(type.IsClass);
        protected static void lazyTest<TResult>(Func<bool> isValueCreated, Func<TResult> getValue, bool valueIsNull = true) {
            isFalse(isValueCreated());
            var d = getValue();
            isTrue(isValueCreated());
            if (valueIsNull) isNull(d); else isNotNull(d);
        }
        protected override T getPropertyValue<T>(bool canWrite = false) {
            var propertyInfo = isProperty<T>(canWrite);
            var o = (T) propertyInfo.GetValue(obj);
            return o;
        }
        protected override void setPropertyValue<T>(PropertyInfo p, T newValue)  
            => p.SetValue(obj, newValue);

        protected override dynamic getCurrentValues() {
            var o = getObject();
            Copy.Members(obj, o);
            return o;
        }
    }
}
