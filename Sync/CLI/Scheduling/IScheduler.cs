namespace Sync.CLI.Scheduling;

public interface IScheduler
{
    void Schedule(CommandLineOptions options);
    void Stop();
    bool IsRunning { get; }
}