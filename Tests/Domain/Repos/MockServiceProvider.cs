using System;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationProject.Tests.Domain.Repos
{
    public class MockServiceProvider :IServiceProvider {
        private readonly dynamic service;
        public MockServiceProvider(dynamic x) => service = x;
        public dynamic GetService(Type serviceType)
            => serviceType == typeof(IServiceScopeFactory) ? returnScopeFactory() : returnService();
        private dynamic returnService() => service;
        private dynamic returnScopeFactory() => new MockServiceScopeFactory(this);
    }
}