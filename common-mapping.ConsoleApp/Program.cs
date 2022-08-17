using common_mapping;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

Console.ResetColor();
ILogger logger = new Logger(Microsoft.Extensions.Logging.LogLevel.Debug, true, true);
try
{
    bool inProgress = true;
    var setup = LoadSettings();
    var options = new Options(setup);

    Mapping map = new Mapping(options, logger);
    Types types = new Types(map);
    Links links = new Links(map);
    Items items = new Items(map);
    Dictionary<ConsoleKey, string> commands = new Dictionary<ConsoleKey, string>();
    commands.Add(ConsoleKey.D1, "MapTypes");
    commands.Add(ConsoleKey.D2, "MapLinks");
    commands.Add(ConsoleKey.D3, "MapItems");
    commands.Add(ConsoleKey.Escape, "Exit");
    while (inProgress)
    {
        switch (Helper.ReadAction(commands))
        {
            case ConsoleKey.D1: types.Main(); break;
            case ConsoleKey.D2: links.Main(); break;
            case ConsoleKey.D3: items.Main(); break;
            default: inProgress = false; break;
        }
    }
}
catch (Exception ex)
{
    logger.LogCritical(ex, ex.Message);
}
logger.LogInformation("App stop");
Console.ReadKey();

common_mapping.Settings.Setup LoadSettings()
{
    var setup = new common_mapping.Settings.Setup();
    JsonSerializerOptions options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Converters = { new JsonStringEnumConverter() }
    };
    using (StreamReader reader = new StreamReader("appsettings.json"))
    {
        var json = reader.ReadToEnd();
        setup = JsonSerializer.Deserialize<common_mapping.Settings.Setup>(json, options);
    }
    return setup;
}