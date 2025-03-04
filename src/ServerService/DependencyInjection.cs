using MassTransit;
using Microsoft.Extensions.Options;
using ServerService.Extensions;
using ServerService.Options;
using ServerService.Redis;
using StackExchange.Redis;

namespace ServerService;

public static class DependencyInjection
{
    public static IServiceCollection AddRedis(this IServiceCollection services) =>
        services.AddSingleton<IConnectionMultiplexer>(
            sp => ConnectionMultiplexer.Connect(sp.GetRequiredService<IOptions<RedisOptions>>().Value
                .ConnectionString));

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration) =>
        services.ConfigureOptions<RedisOptions>(configuration)
            .ConfigureOptions<EmailOptions>(configuration);

    public static IServiceCollection AddQueue(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new RabbitMqOptions();
        configuration.GetSection(nameof(RabbitMqOptions)).Bind(options);

        return services.AddMassTransit(x =>
        {
            x.AddConsumer<EmailConsumer>();

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

    public static IServiceCollection AddServices(this IServiceCollection services) =>
        services.AddTransient<ICashService, CashService>().AddTransient<IMailSender, MailSender>()
            .AddTransient<ISmtpClientFabric, SmtpClientFabric>();
}