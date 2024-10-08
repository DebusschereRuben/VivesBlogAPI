using Microsoft.Extensions.DependencyInjection;

namespace VivesBlog.SDK.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, string apiUrl)
        {
            services.AddHttpClient("VivesBlogApi", options =>
            {
                options.BaseAddress = new Uri(apiUrl);
            });

            services.AddScoped<ArticleSDK>();
            services.AddScoped<PersonSDK>();

            return services;
        }
    }
}
