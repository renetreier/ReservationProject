using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ReservationProject.Aids {
    public static class GetClass {

        private static string Get => "get_";
        private static string Set => "set_";
        private static string Add => "add_";
        private static string Remove => "remove_";
        private static string Ctor => ".ctor";
        private static string Value => "value__";
        private static string Test => "+TestClass";
        public static string Namespace(Type type) => type is null ? string.Empty : type.Namespace;
        public static List<MemberInfo> Members(Type type,
            BindingFlags f = PublicFlagsFor.All,
            bool clean = true) {
            if (type is null) return new List<MemberInfo>();
            var l = type.GetMembers(f).ToList();
            if (clean) RemoveSurrogates(l);
            return l;
        }
        public static List<PropertyInfo> Properties(Type type,
            BindingFlags f = PublicFlagsFor.All)
            => type?.GetProperties(f).ToList() ?? new List<PropertyInfo>();
        public static PropertyInfo Property<T>(string name)
            => Safe.Run(() => typeof(T).GetProperty(name), null);
        public static PropertyInfo Property<T>(Expression<Func<T, object>> e) {
            var n = GetMember.Name(e);
            return Safe.Run(() => typeof(T).GetProperty(n), null);
        }
        private static void RemoveSurrogates(IList<MemberInfo> l) {
            for (var i = l.Count; i > 0; i--) {
                var m = l[i - 1];
                if (!IsSurrogate(m)) continue;
                l.RemoveAt(i - 1);
            }
        }
        private static bool IsSurrogate(MemberInfo m) {
            var n = m.Name;
            if (string.IsNullOrEmpty(n)) return false;
            if (n.Contains(Get)) return true;
            if (n.Contains(Set)) return true;
            if (n.Contains(Add)) return true;
            if (n.Contains(Remove)) return true;
            if (n.Contains(Value)) return true;
            return n.Contains(Test) || n.Contains(Ctor);
        }
        public static List<object> ReadWritePropertyValues(object obj) {
            var l = new List<object>();
            if (obj is null) return l;
            foreach (var p in Properties(obj.GetType()).Where(p => p.CanWrite))
            {
                AddValue(p, obj, l);
            }
            return l;
        }
        private static void AddValue(PropertyInfo p, object o, List<object> l) {
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





