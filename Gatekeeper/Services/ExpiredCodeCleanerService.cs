namespace Gatekeeper.Services
{
    public class ExpiredCodeCleanerService : IHostedService
    {
        private readonly RepositoryService _repo;
        private Timer? _timer;

        public ExpiredCodeCleanerService(RepositoryService repositoryService)
        {
            _repo = repositoryService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(RunCleanup, AuthService.VALID_TIME, TimeSpan.Zero, Timeout.InfiniteTimeSpan);
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_timer != null)
                await _timer.DisposeAsync();
            _timer = null;
        }

        private void RunCleanup(object? state) =>
            RemoveExpiredAuthCodesAsync().GetAwaiter().GetResult();

        private async Task RemoveExpiredAuthCodesAsync() =>
            await _repo.DeleteAuthCodeRangeAsync(await _repo.GetExpiredAuthCodesAsync());
    }
}
