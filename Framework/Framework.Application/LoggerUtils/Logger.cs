using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Sinks.MongoDB;


namespace Framework.Application.LoggerUtils
{
    public static class Logger
    {
        public static void ConfigureLogger()
        {
            var customTheme = new AnsiConsoleTheme(new Dictionary<ConsoleThemeStyle, string>
            {
                [ConsoleThemeStyle.Text] = "\u001b[37m",
                [ConsoleThemeStyle.SecondaryText] = "\u001b[90m",
                [ConsoleThemeStyle.TertiaryText] = "\u001b[36m",
                [ConsoleThemeStyle.Invalid] = "\u001b[31m",
                [ConsoleThemeStyle.Null] = "\u001b[35m",
                [ConsoleThemeStyle.Name] = "\u001b[33m",
                [ConsoleThemeStyle.String] = "\u001b[32m",
                [ConsoleThemeStyle.Number] = "\u001b[34m",
                [ConsoleThemeStyle.Boolean] = "\u001b[36m",
                [ConsoleThemeStyle.Scalar] = "\u001b[37m",
                [ConsoleThemeStyle.LevelVerbose] = "\u001b[37m",
                [ConsoleThemeStyle.LevelDebug] = "\u001b[36m",
                [ConsoleThemeStyle.LevelInformation] = "\u001b[32m",
                [ConsoleThemeStyle.LevelWarning] = "\u001b[33m",
                [ConsoleThemeStyle.LevelError] = "\u001b[31m",
                [ConsoleThemeStyle.LevelFatal] = "\u001b[41m\u001b[30m"
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.Console(theme: customTheme,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    new CompactJsonFormatter(),
                    "logs/log-.json",
                    rollingInterval: Serilog.RollingInterval.Day,  // Explicitly specify Serilog.RollingInterval
                    retainedFileCountLimit: 7
                )
                .WriteTo.MongoDB("mongodb://localhost:27017/logs", collectionName: "logEvents")
                .CreateLogger();
        }

        public static void LogDebug(string message) => Log.Debug(message);
        public static void LogInfo(string message) => Log.Information(message);
        public static void LogWarning(string message) => Log.Warning(message);
        public static void LogError(string message, Exception ex = null) => Log.Error(ex, message);
        public static void LogFatal(string message, Exception ex = null) => Log.Fatal(ex, message);
    }
}
