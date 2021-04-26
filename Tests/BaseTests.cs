using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests
{
    public abstract class BaseTests {
        private static System.Reflection.BindingFlags allFlags => PublicFlagsFor.All | NonPublicFlagsFor.All;
        protected static void areEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreEqual(e, a);
        protected static void areEqual<TExpected, TActual>(TExpected e, TActual a, string s) => Assert.AreEqual(e, a, s);
        protected static void areNotEqual<TExpected, TActual>(TExpected e, TActual a) => Assert.AreNotEqual(e, a);
        protected static void exception<T>(Action a) where T : Exception => Assert.ThrowsException<T>(a);
        protected static void isNull(object o, string msg = null) => Assert.IsNull(o, msg ?? string.Empty);
        protected static void isNotNull(object o) => Assert.IsNotNull(o);
        protected static void isNotNull(object o, string message) => Assert.IsNotNull(o, message);
        protected static void isInstanceOfType<TType>(object o) => isInstanceOfType(o, typeof(TType));
        protected static void isInstanceOfType(object o, Type t) => Assert.IsInstanceOfType(o, t);
        protected static void isFalse(bool b) => Assert.IsFalse(b);
        protected static void isTrue(bool b, string s = null) {
            if (s is null) Assert.IsTrue(b);
            else Assert.IsTrue(b, s);
        }
        protected static void fail(string message) => Assert.Fail(message);
        protected static void notTested(string message) => Assert.Inconclusive(message);
        protected static void notTested(string message, params object[] parameters)
            => Assert.Inconclusive(message, parameters);
        protected static void isReadOnly(object o, string propertyName, object expected) {
            var actual = isReadOnly(o, propertyName);
            areEqual(expected, actual);
        }
        protected static object isReadOnly(object o, string propertyName) {
            var p = o.GetType().GetProperty(propertyName, allFlags);
            isNotNull(p);
            isFalse(p?.CanWrite ?? true);
            isTrue(p?.CanRead ?? false);
            return p?.GetValue(o);
        }
        protected static string getPropertyAfter(string methodName) {
            var stack = new StackTrace();
            int i = methodFrameIdx(stack, methodName);
            return nextPropertyName(stack, i);
        }
        private static string nextPropertyName(StackTrace s, int i) {
            for (i += 1; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n is "MoveNext" or "Start") continue;
                return n?.Replace("Test", string.Empty);
            }
            return string.Empty;
        }
        private static int methodFrameIdx(StackTrace s, string methodName) {
            int i;
            for (i = 0; i < s.FrameCount - 1; i++) {
                var n = s.GetFrame(i)?.GetMethod()?.Name;
                if (n == methodName) break;
            }
            return i;
        }
        protected static void equalProperties(object x, object y, params string[] except) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                if (except.Contains(name)) continue;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                isNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                areEqual(expected, actual, $"For property'{name}'.");
            }
        }
        protected static void notEqualProperties(object x, object y) {
            foreach (var property in x.GetType().GetProperties(PublicFlagsFor.Instance)) {
                var name = property.Name;
                var p = y.GetType().GetProperty(name, PublicFlagsFor.Instance);
                isNotNull(p, $"No property with name '{name}' found.");
                var expected = property.GetValue(x);
                var actual = p?.GetValue(y);
                if (expected != actual) return;
            }
            fail("All properties are same");
        }
        protected static void htmlContains(IReadOnlyList<object> actual, IReadOnlyList<string> expected) {
            isInstanceOfType(actual, typeof(List<object>));
            areEqual(expected.Count, actual.Count);
            for (var i = 0; i < actual.Count; i++) {
                var a = actual[i].ToString();
                var e = expected[i];
                isTrue(a?.Contains(e) ?? false, $"{e} != {a}");
            }
        }
    }
}