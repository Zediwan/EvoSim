using EvoSim.ECS.Components;
using System.Windows.Documents;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;
using Microsoft.Extensions.Logging;

namespace EvoSim.ECS.Systems;

public class EnergySystem : ISystem
{
    private readonly ILogger<EnergySystem> _logger;
    private readonly int _drainRate;

    public EnergySystem(ILogger<EnergySystem> logger, int drainRate = 1)
    {
        _logger = logger;
        _drainRate = drainRate;
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<EnergyComponent>())
        {
            EnergyUtility.UseEnergy(entity, (int)(_drainRate * deltaTime), _logger);
        }
    }
}
