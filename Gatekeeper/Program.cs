using Discord.WebSocket;
using Gatekeeper.Data;
using Gatekeeper.Services;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(config => config.AddEnvironmentVariables())
    .ConfigureServices(services =>
    {
        // Backend
        services.AddDbContext<MemberDbContext>();
        services.AddSingleton<RepositoryService>();
        services.AddSingleton<GuildAffiliationService>();
        services.AddSingleton<MemberService>();

        // Auth
        services.AddSingleton<AuthService>();
        services.AddHostedService<ExpiredCodeCleanerService>();

        // Discord
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton<DiscordManagementService>();
    }).Build();
    
using (var scope = host.Services.CreateScope())
    await scope.ServiceProvider.GetRequiredService<MemberDbContext>().Database.EnsureCreatedAsync();

host.Run();