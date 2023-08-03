using SampleOnlineMall.Core;
using SampleOnlineMall.Core.Appilcation;
using SampleOnlineMall.Core.Managers;
using SampleOnlineMall.DataAccess.Abstract;
using SampleOnlineMall.DataAccess.DataAccess;
using SampleOnlineMall.Service;
using Serilog;

namespace SampleOnlineMall
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.WebHost.ConfigureKestrel(options => options.ListenLocalhost(6000));
            var configuration = new ConfigurationBuilder().Build();

            builder.Configuration.AddConfiguration(configuration);

            var _app = new SampleOnlineMallAssortmentApiApp();

            string logFilePath = System.IO.Path.Combine(_app.LogsDirectory, Functions.GetNextFreeFileName(_app.LogsDirectory, "AssortmentApiLogs", "txt"));

            Serilog.ILogger _logger = new LoggerConfiguration()
                  .WriteTo.File(logFilePath)
                  .CreateLogger();


            _logger.Information("P1");

            // Add services to the container
            builder.Services.AddSingleton(typeof(SampleOnlineMallAssortmentApiApp), (x) => _app);
            builder.Services.AddScoped(typeof(CommodityItemManager));
            builder.Services.AddSingleton(typeof(Serilog.ILogger), (x) => _logger);
            builder.Services.AddSingleton(typeof(IAsyncRepository<CommodityItem>), typeof(InMemoryAsyncRepository<CommodityItem>));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            _logger.Information("P2");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            
            app.UseSwagger();
            app.UseSwaggerUI();


            if (app.Environment.IsDevelopment())
            {
            }
            
            _logger.Information("P3");

            try
            {
                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapControllers();
                _logger.Information("P33");
                app.Run();
            }
            catch (Exception ex)
            {
                _logger.Information($"Error: {ex.Message}");
            }


            _logger.Information("P4");
        }
    }
}