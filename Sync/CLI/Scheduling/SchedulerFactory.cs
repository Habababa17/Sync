namespace Sync.CLI.Scheduling;

public static class SchedulerFactory
{
    public static IScheduler Create() => 
#if WINDOWS
        new WindowsScheduler();
#else
        throw new PlatformNotSupportedException("Only Windows is supported at the moment.");
#endif
}