using Serilog;

namespace WebLogger.Blazor.Core.Managers
{
    public class WebLoggerSettingsManager
    {

        Serilog.ILogger _logger;

        public WebLoggerSettingsManager(Serilog.ILogger logger)
        {
            _logger = logger;
        }


    }
}
