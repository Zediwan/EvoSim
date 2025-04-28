using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class HealthUtility
{
    public static void TakeDamage(Entity entity, float amount)
    {
        if (!entity.HasComponent<HealthComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have HealthComponent.");

        var health = entity.GetComponent<HealthComponent>();
        health.Health -= amount;

        Console.WriteLine($"Entity {entity.Id} took {amount} damage. Remaining Health: {health.Health}");

        if (health.Health <= 0)
        {
            health.Health = 0;
            Console.WriteLine($"Entity {entity.Id} health depleted. Entity is now dead.");
        }
    }

    public static void Heal(Entity entity, float amount)
    {
        if (!entity.HasComponent<HealthComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have HealthComponent.");

        var health = entity.GetComponent<HealthComponent>();
        health.Health = Math.Min(health.Health + amount, health.MaxHealth);

        Console.WriteLine($"Entity {entity.Id} healed by {amount}. Current Health: {health.Health}");
    }

    public static bool IsDead(Entity entity)
    {
        if (!entity.HasComponent<HealthComponent>())
            return true;

        var health = entity.GetComponent<HealthComponent>();
        return health.Health <= 0;
    }
}