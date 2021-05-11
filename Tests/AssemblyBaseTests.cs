using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReservationProject.Aids;

namespace ReservationProject.Tests {
    public abstract class AssemblyBaseTests: BaseTests  
    {
        protected AssemblyBaseTests(string assemblyName = null, 
            string testAssemblyName = null) 
        {
            Assembly = assemblyName ?? "ReservationProject";
            var head = Assembly.GetHead();
            var tail = Assembly.GetTail();
            TestAssembly = testAssemblyName ?? $"{head}.Tests";
            TestNamespace = $"{TestAssembly}.{tail}";
        }
        [TestInitialize] public void CreateList() => _list = new List<string>();
        [TestMethod] public void IsTested() => IsAllTested(Assembly);
        private static string IsNotTested => "<{0}> is not tested";
        private static string NoClassesInAssembly => "No classes in the assembly {0}";
        private static string NoClassesInNamespace => "No classes in the namespace {0}";
        protected string TestAssembly { get; }
        protected string TestNamespace { get; }
        protected string Assembly { get; }
        private static char GenericsClass => '`';
        private static char InternalClass => '+';
        private static string DisplayClass => "<>";
        private static string Shell32Class => "Shell32.";
        private List<string> _list;
        protected virtual string NameSpace(string name) => $"{Assembly}.{name}";
        protected void IsAllTested(string assemblyName, string namespaceName = null) 
        {
            namespaceName ??= assemblyName;
            var l = GetTypes(assemblyName);
            RemoveInterfaces(l);
            _list = GetNames(l);
            RemoveNotIn(_list, namespaceName);
            if (_list.Count == 0) NotTested(NoClassesInNamespace, namespaceName);
            RemoveSurrogates(_list);
            RemoveTested();
            if (_list.Count == 0) return;
            NotTested(IsNotTested, _list[0]);
        }
        private static List<Type> GetTypes(string assembly)
        {
            var l = GetSolution.TypesForAssembly(assembly);
            if (l.Count == 0) NotTested(NoClassesInAssembly, assembly);
            return l;
        }
        private static void RemoveInterfaces(IList<Type> types)
        {
            for (var i = types.Count; i > 0; i--) {
                var e = types[i - 1];
                if (!e.IsInterface) continue;
                types.Remove(e);
            }
        }
        private static List<string> GetNames(IEnumerable<Type> l) => l.Select(o => o.FullName).ToList();
        private static void RemoveNotIn(List<string> l,  string namespaceName) 
        {
            if (string.IsNullOrEmpty(namespaceName)) return;
            l.RemoveAll(o => !o.StartsWith(namespaceName + '.'));
        }
        private static void RemoveSurrogates(List<string> l)
        {
            l.RemoveAll(o => o.Contains(Shell32Class));
            l.RemoveAll(o => o.Contains(InternalClass));
            l.RemoveAll(o => o.Contains(DisplayClass));
            l.RemoveAll(o => o.Contains("<"));
            l.RemoveAll(o => o.Contains(">"));
            l.RemoveAll(o => o.Contains("Migrations"));
        }
        private void RemoveTested() 
        {
            var tests = GetTestClasses();
            for (var i = _list.Count; i > 0; i--) 
            {
                var className = _list[i - 1];
                var testName = ToTestName(className);
                var t = tests.Find(o => o.EndsWith(testName));
                if (t is null) continue;
                _list.RemoveAt(i - 1);
            }
        }
        private List<string> GetTestClasses()
        {
            var tests = GetSolution.TypeNamesForAssembly(TestAssembly);
            RemoveNotIn(tests, TestNamespace);
            RemoveSurrogates(tests);
            return tests.Select(RemoveGenericsChars).ToList();
        }
        private string ToTestName(string className)
        {
            className = RemoveAssemblyName(className);
            className = RemoveGenericsChars(className);
            return className + "Tests";
        }
        private static string RemoveGenericsChars(string className)
        {
            var idx = className.IndexOf(GenericsClass);
            if (idx > 0) className = className.Substring(0, idx);
            return className;
        }
        private string RemoveAssemblyName(string className) => className[Assembly.Length..];
    }
}    
