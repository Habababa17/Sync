using Sync.CLI;
using Sync.CLI.Scheduling;

var options = CliParser.Parse(args);
if(options.isFromScheduler)
{
    Console.WriteLine("Running from scheduler.");
}
Console.WriteLine($"Source:  {options.SourceFolder}");
Console.WriteLine($"Target:  {options.TargetFolder}");
Console.WriteLine($"Log:     {options.LogFilePath}");
Console.WriteLine($"Interval: {options.Interval}");


// run as a human to schedule the interval
if (!options.isFromScheduler && options.Interval is not null)
{
    // -i passed => save options, schedule, exit
    OptionsStore.Save(options);
    SchedulerFactory.Create().Schedule(options);
    Console.WriteLine($"Scheduled to run every {options.Interval}.");
    return;
}


// run from scheduler or manually => run one sync and exit 
var stored = OptionsStore.Load()
    ?? throw new InvalidOperationException("No stored options. Run with -i first.");
//RunSynchronization(stored);