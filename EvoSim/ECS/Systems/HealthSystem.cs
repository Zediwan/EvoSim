using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class HealthSystem : ISystem
{

    public HealthSystem()
    {

    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        var entitiesToRemove = new List<Entity>();

        foreach (var entity in ecsEngine.GetEntitiesWith<HealthComponent>())
        {
            var health = entity.GetComponent<HealthComponent>();

            if (!health.IsAlive)
            {
                entitiesToRemove.Add(entity);
            }
        }

        foreach (var entity in entitiesToRemove)
        {
            HandleDeath(entity, ecsEngine);
        }
    }

    private void HandleDeath(Entity entity, EcsEngine ecsEngine)
    {
        Console.WriteLine("Entity {EntityId} has died. Removing from world.", entity.Id);
        ecsEngine.RemoveEntity(entity);
    }
}
