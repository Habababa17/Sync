namespace Sync.CLI;
public sealed record CommandLineOptions(
    string SourceFolder,
    string TargetFolder,
    TimeSpan? Interval,
    string LogFilePath)
    {
        public bool isFromScheduler { get; init; }
    }