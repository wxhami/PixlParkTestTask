namespace WebClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureOptions<TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TOptions : class
    {
        services.AddOptions<TOptions>()
            .Bind(configuration.GetSection(typeof(TOptions).Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}