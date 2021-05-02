using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests
{
    public abstract class BaseTests {
        private static System.Reflection.BindingFlags AllFlags => PublicFlagsFor.All | NonPublicFlagsFor.All;
        protected static void AreEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreEqual(e, a);
        protected static void AreEqual<TExpected, TActual>(TExpected e, TActual a, string s) => Assert.AreEqual(e, a, s);
        protected static void AreNotEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreNotEqual(e, a);
        protected static void Exception<T>(Action a) where T : Exception => Assert.ThrowsException<T>(a);
        protected static void IsNull(object o, string msg = null) => Assert.IsNull(o, msg ?? string.Empty);
        protected static void IsNotNull(object o) => Assert.IsNotNull(o);
        protected static void IsNotNull(object o, string message) => Assert.IsNotNull(o, message);
        protected static void IsInstanceOfType<TType>(object o) => IsInstanceOfType(o, typeof(TType));
        protected static void IsInstanceOfType(object o, Type t) => Assert.IsInstanceOfType(o, t);
        protected static void IsFalse(bool b) => Assert.IsFalse(b);
        protected static void IsTrue(bool b, string s = null) {
            if (s is null) Assert.IsTrue(b);
            else Assert.IsTrue(b, s);
        }
        protected static void Fail(string message) => Assert.Fail(message);
        protected static void NotTested(string message) => Assert.Inconclusive(message);
        protected static void NotTested(string message, params object[] parameters)
            => Assert.Inconclusive(message, parameters);
        protected static void IsReadOnly(object o, string propertyName, object expected) {
            var actual = IsReadOnly(o, propertyName);
            AreEqual(expected, actual);
        }
        protected static object IsReadOnly(object o, string propertyName) {
            var p = o.GetType().GetProperty(propertyName, AllFlags);
            IsNotNull(p);
            IsFalse(p?.CanWrite ?? true);
            IsTrue(p?.CanRead ?? false);
            return p?.GetValue(o);
        }
        protected static string GetPropertyAfter(string methodName) {
            var stack = new StackTrace();
            int i = MethodFrameIdx(stack, methodName);
            return NextPropertyName(stack, i);
        }
        private static string NextPropertyName(StackTrace s, int i) {
            for (i += 1; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n is "MoveNext" or "Start") continue;
                return n?.Replace("Test", string.Empty);
            }
            return string.Empty;
        }
        private static int MethodFrameIdx(StackTrace s, string methodName) {
            int i;
            for (i = 0; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n == methodName) break;
            }
            return i;
        }
        protected static void EqualProperties(object x, object y, params string[] except) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                if (except.Contains(name)) continue;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                IsNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                AreEqual(expected, actual, $"For property'{name}'.");
            }
        }
        protected static void NotEqualProperties(object x, object y) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                IsNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                if (expected != actual) return;
            }
            Fail("All properties are same");
        }
        protected static void HtmlContains(IReadOnlyList<object> actual, IReadOnlyList<string> expected) {
            IsInstanceOfType(actual, typeof(List<object>));
            AreEqual(expected.Count, actual.Count);
            for (var i = 0; i < actual.Count; i++) {
                var a = actual[i].ToString();
                var e = expected[i];
                IsTrue(a?.Contains(e) ?? false, $"{e} != {a}");
            }
        }
    }
}