using MassTransit;
using Microsoft.Extensions.Options;
using WebClient.Extensions;
using WebClient.Options;

namespace WebClient;

public static class DependencyInjection
{
    public static IServiceCollection AddLocal(this IServiceCollection services) =>
        services.AddLocalization()
            .AddRequestLocalization(options =>
            {
                options.SetDefaultCulture("en-US")
                    .AddSupportedCultures("en-US", "ru-RU")
                    .AddSupportedUICultures("en-US", "ru-RU");
            });

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureOptions<RabbitMqOptions>(configuration).ConfigureOptions<ServerServiceHttpClientOptions>(configuration);

    public static IServiceCollection AddQueue(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        configuration.GetSection(nameof(RabbitMqOptions)).Bind(options);
        
      return  services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(options.Host, options.Port, options.VirtualHost, h =>
                {
                    h.Username(options.Username);
                    h.Password(options.Password);
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }

    public static IServiceCollection AddServerServiceClient(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ServerServiceHttpClientOptions();
        configuration.GetSection(nameof(ServerServiceHttpClientOptions)).Bind(options);

        return services.AddHttpClient<IServerServiceClient, ServerServiceHttpClient>(o =>
                o.BaseAddress = new Uri(options.BaseUrl))
            .Services;
    }
}