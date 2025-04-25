using EvoSim.ECS.Systems;
using Microsoft.Extensions.Logging;
using System.Windows;
using EvoSim.ECS.Core;

namespace EvoSim;
public partial class App : Application
{
    public static EcsEngine EcsEngine { get; private set; }
    public static ILoggerFactory LoggerFactory { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Setup Logging
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            object value = builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });

        // Setup ECS World
        EcsEngine = new EcsEngine();

        var energySystem = new EnergySystem(LoggerFactory.CreateLogger<EnergySystem>());
        var healthSystem = new HealthSystem(LoggerFactory.CreateLogger<HealthSystem>());

        EcsEngine.AddSystem(energySystem);
        EcsEngine.AddSystem(healthSystem);

        // Optionally start background simulation loop here, or control from MainWindow
    }
}
