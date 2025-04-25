using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;
using Microsoft.Extensions.Logging;

namespace EvoSim.ECS.Utilities;

public static class HealthUtility
{
    public static void TakeDamage(Entity entity, int amount, ILogger logger = null)
    {
        if (!entity.HasComponent<HealthComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have HealthComponent.");

        var health = entity.GetComponent<HealthComponent>();
        health.Health -= amount;

        logger?.LogInformation("Entity {EntityId} took {Damage} damage. Remaining Health: {Health}",
            entity.Id, amount, health.Health);

        if (health.Health <= 0)
        {
            health.Health = 0;
            logger?.LogWarning("Entity {EntityId} health depleted. Entity is now dead.", entity.Id);
        }
    }

    public static void Heal(Entity entity, int amount, ILogger logger = null)
    {
        if (!entity.HasComponent<HealthComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have HealthComponent.");

        var health = entity.GetComponent<HealthComponent>();
        health.Health = Math.Min(health.Health + amount, health.MaxHealth);

        logger?.LogInformation("Entity {EntityId} healed by {Amount}. Current Health: {Health}",
            entity.Id, amount, health.Health);
    }

    public static bool IsDead(Entity entity)
    {
        if (!entity.HasComponent<HealthComponent>())
            return true;

        var health = entity.GetComponent<HealthComponent>();
        return health.Health <= 0;
    }
}