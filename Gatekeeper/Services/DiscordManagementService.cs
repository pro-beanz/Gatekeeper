using Discord;
using Discord.Net;
using Discord.WebSocket;

namespace Gatekeeper.Services
{
    public class DiscordManagementService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;
        private readonly ILogger<DiscordManagementService> _logger;

        public DiscordManagementService(DiscordSocketClient client, IConfiguration config, ILogger<DiscordManagementService> logger)
        {
            _client = client;
            _config = config;
            _logger = logger;

            // Combined logger setup implementation from https://github.com/foxbot/patek/blob/master/src/Patek/Services/LogService.cs
            // and https://github.com/Aux/Discord.Net-Example/blob/3.10/src/Utility/LogHelper.cs
            _client.Log += msg =>
            {
                _logger.Log((LogLevel)(Math.Abs(((int)msg.Severity) - 5)), msg.ToString());
                return Task.CompletedTask;
            };

            _client.Disconnected += async ex =>
            {
                if (ex.GetType().Equals(typeof(HttpException)))
                {
                    _logger.LogCritical("Invalid Discord application token! Set the DISCORD_SECRET environment variable.");
                    await _client.StopAsync();
                }
            };
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.LoginAsync(TokenType.Bot, _config["DISCORD_SECRET"]);
            await _client.StartAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.LogoutAsync();
            await _client.StopAsync();
        }
    }
}
