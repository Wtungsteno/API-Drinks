using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace A_WebAPILoGiud
{
    public static class ProgramExtensions
    {
        public static IServiceCollection AddPlatformHttpClientFactory(this IServiceCollection services)
        {
            AsyncRetryPolicy<HttpResponseMessage> retryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync([
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4),
                    ]);
            return services
                .AddHttpClient("ExternalApi")
                .AddPolicyHandler(retryPolicy)
                .Services;
        }
    }
}
