using System.Text.Json;

namespace Sync.CLI;

public static class OptionsStore
{
    private static readonly string ConfigFilePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "SyncApp",
        "config.json"
    );

    public static void Save(CommandLineOptions options)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath)!);
        File.WriteAllText(ConfigFilePath, JsonSerializer.Serialize(options, JsonContext.Default.CommandLineOptions));
    }

    public static CommandLineOptions? Load()
    {
        if (!File.Exists(ConfigFilePath))
        {
            return null;
        }

        var json = File.ReadAllText(ConfigFilePath);
        return JsonSerializer.Deserialize(json, JsonContext.Default.CommandLineOptions);
    }
}