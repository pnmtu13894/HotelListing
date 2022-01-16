using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Create logger
            // Log.Logger = new LoggerConfiguration()
            //     .WriteTo.File(path: "/Users/tupham/RiderProjects/HotelListing/Logs/log-.txt",
            //         outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
            //         rollingInterval: RollingInterval.Day,
            //         restrictedToMinimumLevel: LogEventLevel.Information
            //     ).CreateLogger();
            string connectionStr = Configuration.GetConnectionString("HotelListingDB");

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(connectionStr, new MSSqlServerSinkOptions() { TableName = "Log" }, null, null,
                    LogEventLevel.Information, null, null, null)
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .CreateLogger();

            try
            {
                Log.Information("App Starting");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal("Application is failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json")
            .Build();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}