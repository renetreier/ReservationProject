using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests {
    public abstract class StaticClassTests: BaseTests {
        private const string notSpecified = "Class is not specified";
        private List<string> members { get; set; }
        protected Type type;
        protected string typeName => getName();
        private string getName() {
            var s = type.Name;
            var index = s.IndexOf("`", StringComparison.Ordinal);
            if (index > -1) s = s.Substring(0, index);
            return s;
        }
        [TestMethod] public virtual void IsStaticTest() 
            => isTrue(type.IsAbstract && type.IsClass && type.IsSealed);
        [TestMethod] public virtual void IsTested() {
            if (type == null) notTested(notSpecified);
            var m = GetClass.Members(type, PublicFlagsFor.Declared);
            members = m.Select(e => e.Name).ToList();
            removeTested();
            if (members.Count == 0) return;
            notTested("<{0}> is not tested", members[0]);
        }
        [TestMethod] public virtual void IsSpecifiedClassTested() {
            if (type == null) Assert.Inconclusive(notSpecified);
            var className = GetType().Name;
            isTrue(className.StartsWith(typeName));
        }
        private void removeTested() {
            var tests = GetType().GetMembers().Select(e => e.Name).ToList();
            for (var i = members.Count; i > 0; i--) {
                var m = members[i - 1] + "Test";
                var isTested = tests.Find(o => o == m);
                if (string.IsNullOrEmpty(isTested)) continue;
                members.RemoveAt(i - 1);
            }
        }
        protected PropertyInfo isProperty<T>(bool canWrite = true) {
            var name = getPropertyName();
            var propertyInfo = type.GetProperty(name);
            Assert.IsNotNull(propertyInfo, "Not found");
            Assert.AreEqual(typeof(T), propertyInfo.PropertyType, "Wrong type");
            Assert.AreEqual(true, propertyInfo.CanRead, "Cant read");
            Assert.AreEqual(canWrite, propertyInfo.CanWrite, "CanWrite is wrong");
            return propertyInfo;
        }
        protected void isReadWriteProperty<T>() {
            var propertyInfo = isProperty<T>();
            var actual = getPropertyValue<T>(true);
            var expected = getValue(actual);
            var current = getCurrentValues();
            setPropertyValue(propertyInfo, expected);
            actual = getPropertyValue<T>(true);
            areEqual(expected, actual);
            arePropertiesEqual(current, getCurrentValues(), propertyInfo.Name);
        }
        private static T getValue<T>(T value) {
            var v = (T)GetRandom.ValueOf<T>();
            while (value.Equals(v)) {
                v = (T)GetRandom.ValueOf<T>();
            }
            return v;
        }
        protected virtual void setPropertyValue<T>(PropertyInfo p, T newValue) { }
        protected virtual dynamic getCurrentValues() => null;
        protected void isReadOnlyProperty<T>() => isProperty<T>(false);
        protected void isReadOnlyProperty<T>(T expected) {
            var actual = getPropertyValue<T>();
            areEqual(expected, actual);
        }
        protected virtual T getPropertyValue<T>(bool canWrite = false) => default;  

        private readonly string[] notPropertyNames = { nameof(getPropertyName), 
            nameof(isReadOnlyProperty) , nameof(isReadWriteProperty), nameof(isProperty)
            , nameof(getPropertyValue), nameof(getCurrentValues), 
            nameof(setPropertyValue), nameof(getValue)};

        protected string getPropertyName() {
            var stack = new StackTrace();
            for (var idx = 0; idx < stack.FrameCount; idx ++) {
                var n = stack.GetFrame(idx)?.GetMethod()?.Name;
                if (notPropertyNames.Contains(n)) continue;
                return n?.Replace("Test", string.Empty);
            }
            return string.Empty;
        }

        protected static void arePropertiesNotEqual<T>(T expected, T actual, params string[] exceptProperties) {
            foreach (var p in typeof(T).GetProperties()) {
                var expectedValue = p.GetValue(expected);
                var actualValue = p.GetValue(actual);
                if (exceptProperties.Contains(p.Name)) areEqual(expectedValue, actualValue);
                else areNotEqual(expectedValue, actualValue);
            }
        }
        protected static void arePropertiesEqual<T>(T expected, T actual, params string[] exceptProperties) {
            foreach (var p in typeof(T).GetProperties()) {
                var expectedValue = p.GetValue(expected);
                var actualValue = p.GetValue(actual);
                if (exceptProperties.Contains(p.Name)) areNotEqual(expectedValue, actualValue);
                else areEqual(expectedValue, actualValue);
            }
        }
    }
}
