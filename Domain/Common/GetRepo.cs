using System;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationProject.Domain.Common {
    public sealed class GetRepo {
    
        public readonly IServiceProvider _provider;
        public static IServiceProvider instance;
        public GetRepo() : this(null) { }
        public GetRepo(IServiceProvider p = null) => _provider = p ?? instance;
        public static void SetProvider(IServiceProvider p) => instance = p;
        public dynamic Instance(Type t)
        {
            var p = _provider;
            var c = p?.CreateScope();
            var s = c?.ServiceProvider;
            var r = s?.GetService(t);
            return r;
        }
        public dynamic Instance<TService>() => Instance(typeof(TService));

    }
}
