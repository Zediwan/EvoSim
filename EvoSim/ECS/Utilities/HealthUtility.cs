using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class HealthUtility
{
    /// <summary>
    /// Reduces the health of the specified entity by the given damage amount.
    /// </summary>
    /// <remarks>If the damage reduces the entity's health to zero or below, the entity is considered dead. 
    /// The method ensures that the health value does not drop below zero.</remarks>
    /// <param name="entity">The entity whose health will be reduced. The entity must have a <see cref="HealthComponent"/>.</param>
    /// <param name="amount">The amount of damage to apply. Must be a non-negative value.</param>
    public static void TakeDamage(Entity entity, float amount)
    {
        Debug.Assert(entity.HasComponent<HealthComponent>(), $"Entity {entity.Id} does not have a {nameof(HealthComponent)}.");
        Debug.Assert(amount >= 0, $"Damage amount ({amount}) cannot be negative.");

        if (!entity.HasComponent<HealthComponent>()) return;
        var health = entity.GetComponent<HealthComponent>();

        amount = Math.Max(amount, 0);

        Console.WriteLine($"Entity {entity.Id} took {amount} damage. Remaining Health: {health.Health - amount}");

        health.Health = Math.Max(health.Health - amount, 0);

        if (!health.IsAlive)
        {
            Console.WriteLine($"Entity {entity.Id} health depleted. Entity is now dead.");
        }
    }

    /// <summary>
    /// Restores health to the specified entity, up to its maximum health.
    /// </summary>
    /// <remarks>If the resulting health exceeds the entity's maximum health, it will be capped at the maximum
    /// health value.</remarks>
    /// <param name="entity">The entity to heal. The entity must have a <see cref="HealthComponent"/>.</param>
    /// <param name="amount">The amount of health to restore. Must be a non-negative value.</param>
    public static void Heal(Entity entity, float amount)
    {
        Debug.Assert(entity.HasComponent<HealthComponent>(), $"Entity {entity.Id} does not have a {nameof(HealthComponent)}.");
        Debug.Assert(amount >= 0, $"Heal amount ({amount}) cannot be negative.");

        if (!entity.HasComponent<HealthComponent>()) return;
        var health = entity.GetComponent<HealthComponent>();

        amount = Math.Max(amount, 0);
        health.Health = Math.Min(health.Health + amount, health.MaxHealth);

        Console.WriteLine($"Entity {entity.Id} healed by {amount}. Current Health: {health.Health}");
    }

    /// <summary>
    /// Determines whether the specified entity is considered dead.
    /// </summary>
    /// <param name="entity">The entity to evaluate. Must not be <see langword="null"/>.</param>
    /// <returns><see langword="true"/> if the entity does not have a <see cref="HealthComponent"/>  or if its health is less
    /// than or equal to zero; otherwise, <see langword="false"/>.</returns>
    public static bool IsDead(Entity entity)
    {
        Debug.Assert(entity.HasComponent<HealthComponent>(), $"Entity {entity.Id} does not have a {nameof(HealthComponent)}.");

        if (!entity.HasComponent<HealthComponent>()) return true;
        var health = entity.GetComponent<HealthComponent>();

        return health.Health <= 0;
    }
}