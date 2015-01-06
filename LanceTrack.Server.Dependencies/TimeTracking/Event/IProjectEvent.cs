namespace LanceTrack.Server.Dependencies.TimeTracking.Event
{
    public interface IProjectEvent
    {
        int Id { get; }

        int ProjectId { get; }
    }
}