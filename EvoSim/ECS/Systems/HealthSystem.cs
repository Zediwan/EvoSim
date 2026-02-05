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

    /// <summary>
    /// Handles the death of an entity by removing it from the ECS engine.
    /// </summary>
    /// <remarks>This method logs the death of the entity and removes it from the ECS engine.  Ensure that
    /// both <paramref name="entity"/> and <paramref name="ecsEngine"/> are valid and initialized before calling this
    /// method.</remarks>
    /// <param name="entity">The entity that has died. Must not be <see langword="null"/>.</param>
    /// <param name="ecsEngine">The ECS engine responsible for managing entities. Must not be <see langword="null"/>.</param>
    private void HandleDeath(Entity entity, EcsEngine ecsEngine)
    {
        Console.WriteLine($"Entity {entity.Id} has died. Removing from world.");
        ecsEngine.RemoveEntity(entity);
    }
}