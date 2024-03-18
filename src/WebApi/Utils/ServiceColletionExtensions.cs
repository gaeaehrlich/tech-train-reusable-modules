using System.Reflection;
using Webapi.ApiValidators;
using Webapi.Contract;

namespace Webapi.Utils
{
    public static class ServiceColletionExtensions
    {
        public static void AddValidators(this IServiceCollection services, string[] configuredDisabledValidators)
        {
            typeof(Program).Assembly.DefinedTypes
                .Where(x => x.GetInterfaces().Contains(typeof(IApiDescriptionValidaor)) && !x.IsAbstract && !configuredDisabledValidators.Contains(x.Name))
                .ToList()
                .ForEach(type => services.AddSingleton(typeof(IApiDescriptionValidaor), type));
        }

        public static void AddErrorListener(this IServiceCollection services, IErrorListener listener)
        {
            services.AddSingleton(listener);
        }
    }
}
