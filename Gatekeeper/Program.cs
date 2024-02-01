using Discord.WebSocket;
using Gatekeeper.Data;
using Gatekeeper.Services;

var app = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
    .ConfigureServices(services =>
    {
                // Backend
        services.AddDbContext<MemberDbContext>(optionsLifetime: ServiceLifetime.Singleton)
                .AddDbContextFactory<MemberDbContext>()
                .AddSingleton<RepositoryService>()
                .AddSingleton<GuildAffiliationService>()
                .AddSingleton<MemberService>()
                
                // Auth
                .AddSingleton<AuthService>()
                .AddHostedService<ExpiredCodeCleanerService>()

                // Discord
                .AddSingleton<DiscordSocketClient>()
                .AddHostedService<DiscordManagementService>();
    }).Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<MemberDbContext>().Database.EnsureCreatedAsync();
}

app.Run();