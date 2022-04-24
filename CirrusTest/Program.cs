// See https://aka.ms/new-console-template for more information

using Cirrus;
using Cirrus.Extensions;
using Cirrus.Models;
using Cirrus.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Serilog;

namespace CirrusTest;

public class Program
{
    public static async Task Main()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateBootstrapLogger();
            
        Log.Information("Starting application");
        using var host = CreateHostBuilder().Build();
            
        Log.Information("Starting to collect data from weather service");
        var token = new CancellationToken();

        await host.StartAsync();
        await Task.Delay(-1);
            
        Log.Information("Exiting application...");
        Log.CloseAndFlush();
    }
    
    private static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile("connections.json", optional: false, reloadOnChange: true)
                        .Build();
                })
                .ConfigureServices((context, collection) =>
                {
                    collection
                        .Configure<CirrusConfig>(context.Configuration)
                        .AddOptions()
                        .AddCirrusServices(cirrusConfig =>
                        {
                            var cirrusConfigApiKeys =
                                context.Configuration.GetValue<string>(nameof(CirrusConfig.ApiKeys));
                            Log.Information("Cirrus ApiKeys: {@Keys}", cirrusConfigApiKeys);

                            cirrusConfig.ApiKeys = new List<string> { cirrusConfigApiKeys };

                            var cirrusConfigApplicationKey =
                                context.Configuration.GetValue<string>(nameof(CirrusConfig.ApplicationKey));
                            Log.Information("Cirrus ApplicationKey: {@Keys}", cirrusConfigApplicationKey);

                            cirrusConfig.ApplicationKey = cirrusConfigApplicationKey;

                            var cirrusConfigMacAddress =
                                context.Configuration.GetValue<string>(nameof(CirrusConfig.MacAddress));
                            Log.Information("Cirrus MacAddress: {@Keys}", cirrusConfigMacAddress);

                            cirrusConfig.MacAddress = cirrusConfigMacAddress;
                        })
                        //.AddHostedService<RealtimeApi>()
                        .AddHostedService<HistoricData>();
                })
                .ConfigureLogging((context, builder) => builder.AddSerilog(dispose: true))
                .UseSerilog((context, provider, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                });
    }

public class RealtimeApi : BackgroundService
    {
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<RealtimeApi> _logger;

        public RealtimeApi(IServiceScopeFactory factory, ILogger<RealtimeApi>? logger = null)
        {
            _factory = factory;
            _logger = logger ?? NullLogger<RealtimeApi>.Instance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting background service");
            
            var scope = _factory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICirrusRealtime>();

            service.OnSubscribe += (sender, token) =>
                _logger.LogInformation("Subscribe date: {@Date}", token.UserDevice);
            
            service.OnDataReceived += (sender, token) =>
                _logger.LogInformation("Data received: {@Date}", token.Device);
            
            await service.OpenConnection(stoppingToken);
        }
    }
    
    public class HistoricData : BackgroundService
    {
        private readonly IServiceScopeFactory _factory;
        private readonly ILogger<HistoricData> _logger;

        public HistoricData(IServiceScopeFactory factory, ILogger<HistoricData>? logger = null)
        {
            _factory = factory;
            _logger = logger ?? NullLogger<HistoricData>.Instance;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting background service");
            
            var scope = _factory.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<ICirrusWrapper>();
            
            var startDate = DateTimeOffset.Now.AddDays(-30);
            var endDate = DateTimeOffset.Now;

            var data = service.FetchDeviceHistory<TestModel>(startDate, endDate);

            await foreach (var day in data)
            {
                _logger.LogInformation("{@Day}", day.First());
            }
        }
    }