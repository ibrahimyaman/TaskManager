using Microsoft.Extensions.DependencyInjection;

namespace TaskManager.Core.Utilities.IoC
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
