using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ReservationProject.Aids {
    public static class GetClass {

        private static string get => "get_";
        private static string set => "set_";
        private static string add => "add_";
        private static string remove => "remove_";
        private static string ctor => ".ctor";
        private static string value => "value__";
        private static string test => "+TestClass";
        public static string Namespace(Type type) => type is null ? string.Empty : type.Namespace;
        public static List<MemberInfo> Members(Type type,
            BindingFlags f = PublicFlagsFor.All,
            bool clean = true) {
            if (type is null) return new List<MemberInfo>();
            var l = type.GetMembers(f).ToList();
            if (clean) removeSurrogates(l);
            return l;
        }
        public static List<PropertyInfo> Properties(Type type,
            BindingFlags f = PublicFlagsFor.All)
            => type?.GetProperties(f).ToList() ?? new List<PropertyInfo>();
        public static PropertyInfo Property<T>(string name)
            => Safe.Run(() => typeof(T).GetProperty(name), (PropertyInfo)null);
        public static PropertyInfo Property<T>(Expression<Func<T, object>> e) {
            var n = GetMember.Name(e);
            return Safe.Run(() => typeof(T).GetProperty(n), (PropertyInfo)null);
        }
        private static void removeSurrogates(IList<MemberInfo> l) {
            for (var i = l.Count; i > 0; i--) {
                var m = l[i - 1];
                if (!isSurrogate(m)) continue;
                l.RemoveAt(i - 1);
            }
        }
        private static bool isSurrogate(MemberInfo m) {
            var n = m.Name;
            if (string.IsNullOrEmpty(n)) return false;
            if (n.Contains(get)) return true;
            if (n.Contains(set)) return true;
            if (n.Contains(add)) return true;
            if (n.Contains(remove)) return true;
            if (n.Contains(value)) return true;
            return n.Contains(test) || n.Contains(ctor);
        }
        public static List<object> ReadWritePropertyValues(object obj) {
            var l = new List<object>();
            if (obj is null) return l;
            foreach (var p in Properties(obj.GetType())) {
                if (!p.CanWrite) continue;
                addValue(p, obj, l);
            }
            return l;
        }
        private static void addValue(PropertyInfo p, object o, List<object> l) {
            var indexer = p.GetIndexParameters();
            if (indexer.Length == 0) l.Add(p.GetValue(o));
            else {
                var i = 0;
                while (true) {
                    try { l.Add(p.GetValue(o, new object[] { i++ })); } catch {
                        l.Add(i);
                        return;
                    }
                }
            }
        }
    }
}





