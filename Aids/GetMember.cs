using System;
using System.Linq.Expressions;

namespace ReservationProject.Aids
{
    public static class GetMember {
        public static string Name<TClass>(Expression<Func<TClass, object>> e)
            => Safe.Run(() => Name(e?.Body), string.Empty);

        public static string Name<TClass, TResult>(Expression<Func<TClass, TResult>> e)
            => Safe.Run(() => Name(e?.Body), string.Empty);
        
        public static string Name<TClass>(Expression<Action<TClass>> e)
            => Safe.Run(() => Name(e?.Body), string.Empty);

        public static string Name(Expression ex) => ex switch {
            MemberExpression member => Name(member), 
            MethodCallExpression method => Name(method),
            UnaryExpression operand => Name(operand),
            _=> string.Empty
        };
        private static string Name(MemberExpression e) => e?.Member.Name ?? string.Empty;
        private static string Name(MethodCallExpression e) => e?.Method.Name ?? string.Empty;
        private static string Name(UnaryExpression e) => e?.Operand switch {
                MemberExpression member => Name(member),
                MethodCallExpression method => Name(method),
                _ => string.Empty
            };
    }
}
