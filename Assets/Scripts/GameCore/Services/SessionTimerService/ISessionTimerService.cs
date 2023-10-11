namespace GameCore.Services.SessionTimerService
{
    public interface ISessionTimerService
    {
        int MinutesFromStart { get; }
        int SecondsFromStart { get; }
    }
}