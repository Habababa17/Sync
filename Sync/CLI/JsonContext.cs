using System.Text.Json.Serialization;

namespace Sync.CLI;
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(CommandLineOptions))]
internal partial class JsonContext : JsonSerializerContext { /*magic ;)*/ }