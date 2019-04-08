using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;

namespace eFolio.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);

            string path = $"{Environment.CurrentDirectory}\\EFolio\\LogFiles\\" +
                          $"{now.Date.Year}\\{month}\\{now.Date.ToShortDateString()}.txt";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                   path,
                   fileSizeLimitBytes: 1_000_000,
                   rollOnFileSizeLimit: true,
                   shared: true,
                   flushToDiskInterval: TimeSpan.FromSeconds(1))
                .CreateLogger();
            try
            {
                Log.Information("Starting web host");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseSerilog()
                   .UseStartup<Startup>();
    }
}
