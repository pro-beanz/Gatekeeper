namespace Gatekeeper.Utility
{
    public static class Helper
    {
        public static bool YoungerThan(DateTimeOffset dateTime, TimeSpan timeSpan) =>
            DateTimeOffset.Now - dateTime < timeSpan;
    }
}
