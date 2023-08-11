using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SampleOnlineMall.Core;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.DataAccess;
using SampleOnlineMall.FrontEnd.Blazor;
using SampleOnlineMall.Service;
using Serilog;
using Serilog.Events;

namespace SampleOnlineMall.FrontEnd.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            var _app = new SampleOnlineMallFrontEndBlazorApp();
            string logFilePath = System.IO.Path.Combine(_app.LogsDirectory, Functions.GetNextFreeFileName(_app.LogsDirectory, "SampleMallBlazorFrontend", "txt"));
            
            Serilog.ILogger _logger = new LoggerConfiguration()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
                       .Enrich.FromLogContext()
                       .WriteTo.BrowserConsole()
                       .WriteTo.File(logFilePath)
                       .CreateLogger();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => _logger);

            var webRepoOptions = new WebApiAsyncRepositoryOptions()
                                    .SetLogger(_logger)
                                    .SetBaseAddress("https://mallassortapi01.t109.tech/")
                                    .SetGetAllHostPath("getall/")
                                    .SetSearchHostPath("search");

            builder.Services.AddScoped(typeof(IAsyncRepository<CommodityItemFrontend>), (x) => new WebApiAsyncRepository<CommodityItemFrontend>(webRepoOptions));
            await builder.Build().RunAsync();
        }
    }
}