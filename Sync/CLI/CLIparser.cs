using System.CommandLine;

namespace Sync.CLI;
public static class CliParser
{
    private static readonly Argument<string?> SourceFolderArgument = new(
        "source")
    {
        Description = "Path of the source folder to synchronize from.",
        DefaultValueFactory = _ => null
    };
    private static readonly Argument<string?> TargetFolderArgument = new(
        "target")
    {
        Description = "Path of the target folder to synchronize to. (the backup)",
        DefaultValueFactory = _ => null
    };

    private static readonly Argument<string?> LogFileArgument = new(
        "log")
    {
        Description = "Path of the log file. If the file does not exist, it will be created. If it exists, new logs will be appended to it.",
        DefaultValueFactory = _ => null
    };

    private static readonly Option<double?> Interval = new(
        "--interval",
        "-i")
    {
        Description = "Synchronization interval, in minutes. Without it program runs only once.",
        DefaultValueFactory = _ => null
    };
    // private static readonly Option<bool> ScheduleMode = new(
    //     "--mode",
    //     "-m")
    // {
    //     Description = "If set, it tells the program that it is being run by a scheduler, //maybe user will be able to call without.",
    //     DefaultValueFactory = _ => false
    // };


    public static CommandLineOptions Parse(string[] args)
    {
        var rootCommand = new RootCommand("Folder synchronization tool.")
        {
            SourceFolderArgument,
            TargetFolderArgument,
            LogFileArgument,
            Interval,
            //ScheduleMode
        };

        var result = rootCommand.Parse(args);

        if (result.Errors.Count > 0)
        {
            var message = string.Join(Environment.NewLine,
                result.Errors.Select(e => e.Message));
            throw new ArgumentException($"Invalid command line arguments:{Environment.NewLine}{message}");
        }
        
        //TODO add usage

        // var isScheduled = result.GetValue(ScheduleMode);
        // if(isScheduled)
        // {
        //     var storedOptions = OptionsStore.Load()
        //         ?? throw new InvalidOperationException("No stored options. Run with -i first to schedule or unintended usage.");
        //     return storedOptions;
        // }

        var source = result.GetValue(SourceFolderArgument);
            //?? throw new ArgumentException("Source folder is required.");
        var target = result.GetValue(TargetFolderArgument);
            //?? throw new ArgumentException("Target folder is required.");
        var logFile = result.GetValue(LogFileArgument);
            //?? throw new ArgumentException("Log file path is required.");
        var intervalMinutes = result.GetValue(Interval);
        TimeSpan? interval = intervalMinutes is null ? null : TimeSpan.FromMinutes(intervalMinutes.Value);


        // Zero args => run by the scheduler (or a lazy human) => load stored options.
        if (source is null && target is null && logFile is null)
        {
            return OptionsStore.Load() switch
            {   
                // Set the flag to know later that it was run by scheduler
                { } options => options with { isFromScheduler = true },
                null => throw new InvalidOperationException(
                    "No stored configuration found. Run once with arguments and -i to schedule.")
            };
        }
        
        if (source is null || target is null || logFile is null)
            throw new ArgumentException("Provide all three paths (source, target, log), or none at all.");

        if (!Directory.Exists(source))
            throw new ArgumentException($"Source folder does not exist: {source}");
        if (!Directory.Exists(target))
            throw new ArgumentException($"Target folder does not exist: {target}");

        return new CommandLineOptions(source, target, interval, logFile);
    }
}