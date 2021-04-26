using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests {
    public abstract class AssemblyBaseTests: BaseTests  {
        protected AssemblyBaseTests(string assemblyName = null, 
            string testAssemblyName = null) {
            assembly = assemblyName ?? "Contoso";
            var head = assembly.GetHead();
            var tail = assembly.GetTail();
            testAssembly = testAssemblyName ?? $"{head}.Tests";
            testNamespace = $"{testAssembly}.{tail}";
        }
        [TestInitialize] public void CreateList() => list = new List<string>();
        [TestMethod] public void IsTested() => isAllTested(assembly);
        private static string isNotTested => "<{0}> is not tested";
        private static string noClassesInAssembly => "No classes in the assembly {0}";
        private static string noClassesInNamespace => "No classes in the namespace {0}";
        protected string testAssembly { get; }
        protected string testNamespace { get; }
        protected string assembly { get; }
        private static char genericsClass => '`';
        private static char internalClass => '+';
        private static string displayClass => "<>";
        private static string shell32Class => "Shell32.";
        private List<string> list;
        protected virtual string nameSpace(string name) => $"{assembly}.{name}";
        protected void isAllTested(string assemblyName, string namespaceName = null) {
            namespaceName ??= assemblyName;
            var l = getTypes(assemblyName);
            removeInterfaces(l);
            list = getNames(l);
            removeNotIn(list, namespaceName);
            if (list.Count == 0) notTested(noClassesInNamespace, namespaceName);
            removeSurrogates(list);
            removeTested();
            if (list.Count == 0) return;
            notTested(isNotTested, list[0]);
        }
        private static List<Type> getTypes(string assembly) {
            var l = GetSolution.TypesForAssembly(assembly);
            if (l.Count == 0) notTested(noClassesInAssembly, assembly);
            return l;
        }
        private static void removeInterfaces(IList<Type> types) {
            for (var i = types.Count; i > 0; i--) {
                var e = types[i - 1];
                if (!e.IsInterface) continue;
                types.Remove(e);
            }
        }
        private static List<string> getNames(IEnumerable<Type> l) => l.Select(o => o.FullName).ToList();
        private static void removeNotIn(List<string> l,  string namespaceName) {
            if (string.IsNullOrEmpty(namespaceName)) return;
            l.RemoveAll(o => !o.StartsWith(namespaceName + '.'));
        }
        private static void removeSurrogates(List<string> l) {
            l.RemoveAll(o => o.Contains(shell32Class));
            l.RemoveAll(o => o.Contains(internalClass));
            l.RemoveAll(o => o.Contains(displayClass));
            l.RemoveAll(o => o.Contains("<"));
            l.RemoveAll(o => o.Contains(">"));
            l.RemoveAll(o => o.Contains("Migrations"));
        }
        private void removeTested() {
            var tests = getTestClasses();
            for (var i = list.Count; i > 0; i--) {
                var className = list[i - 1];
                var testName = toTestName(className);
                var t = tests.Find(o => o.EndsWith(testName));
                if (t is null) continue;
                list.RemoveAt(i - 1);
            }
        }
        private List<string> getTestClasses() {
            var tests = GetSolution.TypeNamesForAssembly(testAssembly);
            removeNotIn(tests, testNamespace);
            removeSurrogates(tests);
            return tests.Select(removeGenericsChars).ToList();
        }
        private string toTestName(string className) {
            className = removeAssemblyName(className);
            className = removeGenericsChars(className);
            return className + "Tests";
        }
        private static string removeGenericsChars(string className) {
            var idx = className.IndexOf(genericsClass);
            if (idx > 0) className = className.Substring(0, idx);
            return className;
        }
        private string removeAssemblyName(string className) => className[assembly.Length..];
    }
}    
