using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Core.Utilities.IoC;

namespace TaskManager.Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
