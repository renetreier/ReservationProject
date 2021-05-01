using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReservationProject.Aids;
using ReservationProject.Core;
using ReservationProject.Data.Common;

namespace ReservationProject.Infra.Common {
    public abstract class OrderedRepo<TEntity, TData> :FilteredRepo<TEntity, TData>
        where TData : BaseEntityData, IEntityData, new() {
        private string sortOrder;
        protected OrderedRepo(DbContext c = null, DbSet<TData> s = null) :base(c, s) { }
        public override string SortOrder {
            get => GetSortOrder();
            set => sortOrder = value;
        }
        protected internal virtual string GetSortOrder()
            => sortOrder?.Contains("_desc") ?? true ? RemoveDesc(sortOrder) : AddDesc(sortOrder);
        public override string CurrentSort => sortOrder;
        protected internal virtual string AddDesc(string s) => $"{s}_desc";
        protected internal virtual string RemoveDesc(string s)
            => s?.Replace("_desc", string.Empty) ?? string.Empty;
        protected internal override IQueryable<TData> CreateSql() => AddSorting(base.CreateSql());
        protected internal IQueryable<TData> AddSorting(IQueryable<TData> query) {
            var expression = CreateExpression();
            var r = expression is null ? query : AddOrderBy(query, expression);
            return r;
        }
        internal Expression<Func<TData, object>> CreateExpression() {
            var property = FindProperty();
            return property is null ? null : LambdaExpression(property);
        }
        internal static Expression<Func<TData, object>> LambdaExpression(PropertyInfo p) {
            var param = Expression.Parameter(typeof(TData), "x");
            var property = Expression.Property(param, p);
            var body = Expression.Convert(property, typeof(object));
            return Expression.Lambda<Func<TData, object>>(body, param);
        }
        internal PropertyInfo FindProperty() {
            var name = GetName();
            return typeof(TData).GetProperty(name);
        }
        internal string GetName() {
            if (string.IsNullOrEmpty(sortOrder)) return string.Empty;
            var s = RemoveDesc(sortOrder);
            return s;
        }
        internal IQueryable<TData> AddOrderBy(IQueryable<TData> query, Expression<Func<TData, object>> e) {
            if (query is null) return null;
            if (e is null) return query;
            return Safe.Run(() => IsDescending()
                ? query.OrderByDescending(e)
                : query.OrderBy(e), query);
        }
        internal bool IsDescending() =>
            !string.IsNullOrEmpty(sortOrder) && sortOrder.EndsWith("_desc");
    }
}