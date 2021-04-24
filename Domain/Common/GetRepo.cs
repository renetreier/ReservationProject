using System;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationProject.Domain.Common {
    public sealed class GetRepo {
        private readonly IServiceProvider _provider;
        private static IServiceProvider instance;
        public GetRepo(IServiceProvider p = null) => _provider = p ?? instance;
        public static void SetProvider(IServiceProvider p) => instance = p;
        public dynamic Instance(Type t) => 
            _provider?.CreateScope()?.ServiceProvider?.GetService(t);
        public dynamic Instance<TService>() => Instance(typeof(TService));

    }
}
