
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Xml;

namespace e_Folio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime now = DateTime.Now;
            string month = now.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.File(
                   $"D:\\LogFiles\\EFolio\\{now.Date.Year}\\{month}\\{now.Date.Day}.txt",
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
