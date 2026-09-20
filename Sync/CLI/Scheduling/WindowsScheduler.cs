using System.Diagnostics;
using Microsoft.Win32.TaskScheduler;
namespace Sync.CLI.Scheduling;
public class WindowsScheduler : IScheduler
{
    private const string TaskName = "SyncTask";
    public bool IsRunning
    {
        get
        {
            using var service = new TaskService();
            var task = service.FindTask(TaskName);
            return task != null && task.State == TaskState.Running;
        }
    }

    public void Schedule(CommandLineOptions options)
    {
        if(options.Interval == null)
        {
            throw new ArgumentException("Interval is required for scheduling.");
        }

        var exePath = Environment.ProcessPath 
        ?? throw new InvalidOperationException("Unable to determine the path of the current executable.");

        using var service = new TaskService();
        var task = service.NewTask();

        task.Settings.StartWhenAvailable = true;
        task.Settings.AllowDemandStart = true;

        task.Triggers.Add(
            new TimeTrigger
            {
                StartBoundary = DateTime.Now /*+ TimeSpan.FromSeconds(5)*/,
                Repetition = new RepetitionPattern(options.Interval.Value, TimeSpan.Zero),
                //Enabled = true,
            }
        );

        task.Actions.Add(
            new ExecAction(exePath,
            arguments: "--mode true", //loaded from config file
            workingDirectory: null)
            //TODO workingDirectory == null? check how the files work
        );

        service.RootFolder.RegisterTaskDefinition
        (
            TaskName, task,
            TaskCreation.CreateOrUpdate, null, null, TaskLogonType.InteractiveToken
        );
    }

    public void Stop()
    {
        using var service = new TaskService();
        service.RootFolder.DeleteTask(TaskName, false);
    }
}