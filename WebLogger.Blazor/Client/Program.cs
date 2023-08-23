using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SampleOnlineMall.Core.Managers;
using SampleOnlineMall.Core.Mappers;
using SampleOnlineMall.Core;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.DataAccess;
using SampleOnlineMall.Service;
using Serilog.Events;
using Serilog;
using WebLogger.Blazor;
using WebLogger.Blazor.Core.App;
using SampleOnlineMall.WebLogger.Blazor.Core;
using WebLogger.Blazor.Core.Models;
using SampleOnlineMall.DataAccess;

namespace WebLogger.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            var _app = new SampleOnlineMallWebLoggerBlazorApp();
            string logFilePath = System.IO.Path.Combine(_app.LogsDirectory, Functions.GetNextFreeFileName(_app.LogsDirectory, "SampleMallBlazorFrontend", "txt"));

            Serilog.ILogger _logger = new LoggerConfiguration()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                       .Enrich.FromLogContext()
                       .WriteTo.BrowserConsole()
                       .WriteTo.File(logFilePath)
                       .CreateLogger();

            _logger.Information("P1");

            _logger.Information("P2");

            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => _logger);


            builder.Services.AddScoped(typeof(SampleOnlineMallWebLoggerBlazorApp), typeof(SampleOnlineMallWebLoggerBlazorApp));
            builder.Services.AddScoped(typeof(ComponentHub), typeof(ComponentHub));
            builder.Services.AddScoped(typeof(IAsyncRepository<WebLoggerSettings>), typeof(EfAsyncRepository<WebLoggerSettings>));

            _logger.Information("Blazor P3");

            var host = builder.Build();
            
            await host.RunAsync();
        }
    }
}