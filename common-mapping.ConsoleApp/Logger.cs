using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common_mapping
{
    internal class Logger : Microsoft.Extensions.Logging.ILogger
    {
        //private readonly Func<LoggerConfiguration> _getCurrentConfig;
        Serilog.ILogger logger;
        public Logger(LogLevel minimumLevel, bool console, bool file)
        {
            var template = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";
            Serilog.Formatting.ITextFormatter formatter = new Serilog.Formatting.Display.MessageTemplateTextFormatter(template);
            
            var config = new LoggerConfiguration();
            switch (minimumLevel)
            {
                case LogLevel.Trace: config.MinimumLevel.Verbose(); break;
                case LogLevel.Debug: config.MinimumLevel.Debug(); break;
                case LogLevel.Information: config.MinimumLevel.Information(); break;
                case LogLevel.Warning: config.MinimumLevel.Warning(); break;
                case LogLevel.Error: config.MinimumLevel.Error(); break;
                case LogLevel.Critical: config.MinimumLevel.Fatal(); break;
                case LogLevel.None: config.MinimumLevel.Verbose(); break;
            }
            if (console)
                config.WriteTo.Console(formatter: formatter);
            if (file)
                config.WriteTo.File(formatter: formatter, path: "./logs/log-.txt", rollingInterval: RollingInterval.Day);
            logger = config.CreateLogger();
        }

        public IDisposable BeginScope<TState>(TState state) => default!;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            string message = $"{formatter(state, exception)}";
            switch (logLevel)
            {
                case LogLevel.Trace: logger.Verbose(message); break;
                case LogLevel.Debug: logger.Debug(message); break;
                case LogLevel.Information: logger.Information(message); break;
                case LogLevel.Warning: logger.Warning(message); break;
                case LogLevel.Error: logger.Error(message); break;
                case LogLevel.Critical: logger.Fatal(message); break;
                case LogLevel.None: logger.Verbose(message); break;
            }
        }
    }
}
