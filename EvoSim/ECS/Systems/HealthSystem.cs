using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Systems;

public class HealthSystem : ISystem
{
    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        var entitiesToRemove = (
            from entity in ecsEngine.GetEntitiesWith<HealthComponent>()
            let health = entity.GetComponent<HealthComponent>()
            where !health.IsAlive
            select entity
        ).ToList();

        foreach (var entity in entitiesToRemove)
        {
            HandleDeath(entity, ecsEngine);
        }
    }

    private void HandleDeath(Entity entity, EcsEngine ecsEngine)
    {
        Console.WriteLine($"Entity {entity.Id} has died. Removing from world.");
        ecsEngine.RemoveEntity(entity);
    }
}