using Labixa.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Outsourcing.Service;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;

public class ServiceConfig
{
    public static void Register(HttpConfiguration config)
    {
        var services = new ServiceCollection();
        services.AddScoped<INganLuongService, NganLuongService>();

        var resolver = new DependencyResolver(services.BuildServiceProvider());
        config.DependencyResolver = resolver;
    }

    public class DependencyResolver : IDependencyResolver
    {
        private readonly IServiceProvider provider;
        private readonly IServiceScope scope;

        public DependencyResolver(ServiceProvider provider) => this.provider = provider;

        internal DependencyResolver(IServiceScope scope)
        {
            this.provider = scope.ServiceProvider;
            this.scope = scope;
        }

        public IDependencyScope BeginScope() =>
            new DependencyResolver(provider.CreateScope());

        public object GetService(Type serviceType) => provider.GetService(serviceType);
        public IEnumerable<object> GetServices(Type type) => provider.GetServices(type);
        public void Dispose() => scope?.Dispose();
    }

}