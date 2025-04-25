using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;
using Microsoft.Extensions.Logging;

namespace EvoSim.ECS.Systems;

public class HealthSystem : ISystem
{
    private readonly ILogger<HealthSystem> _logger;

    public HealthSystem(ILogger<HealthSystem> logger)
    {
        _logger = logger;
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<HealthComponent>())
        {
            var health = entity.GetComponent<HealthComponent>();

            if (!health.IsAlive)
            {
                HandleDeath(entity, ecsEngine);
            }
        }
    }

    private void HandleDeath(Entity entity, EcsEngine ecsEngine)
    {
        _logger.LogWarning("Entity {EntityId} has died. Removing from world.", entity.Id);
        ecsEngine.RemoveEntity(entity);
    }
}
