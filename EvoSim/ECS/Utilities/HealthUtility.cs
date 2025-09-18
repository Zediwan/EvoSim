using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class HealthUtility
{
    /// <summary>
    /// Reduces the health of the specified entity by the given amount.
    /// </summary>
    /// <remarks>If the entity does not have a <see cref="HealthComponent"/>, the method will return without
    /// applying any damage.</remarks>
    /// <param name="entity">The entity whose health will be reduced. The entity must have a <see cref="HealthComponent"/>.</param>
    /// <param name="amount">The amount of damage to apply to the entity's health. Must be a non-negative value.</param>
    public static void TakeDamage(Entity entity, float amount)
    {
        Debug.Assert(entity.HasComponent<HealthComponent>(), $"Entity {entity.Id} does not have a {nameof(HealthComponent)}.");

        if (!entity.HasComponent<HealthComponent>()) return;
        TakeDamage(entity.GetComponent<HealthComponent>(), amount);
    }

    /// <summary>
    /// Reduces the health of the specified <see cref="HealthComponent"/> by the given damage amount.
    /// </summary>
    /// <remarks>If the specified <paramref name="healthComponent"/> is already dead, the method will return
    /// without applying any damage. The health value is clamped to ensure it does not drop below zero. If the health
    /// reaches zero, the entity is considered dead.</remarks>
    /// <param name="healthComponent">The <see cref="HealthComponent"/> representing the entity whose health will be reduced. Must be alive prior to
    /// calling this method.</param>
    /// <param name="amount">The amount of damage to apply. Must be a non-negative value.</param>
    public static void TakeDamage(HealthComponent healthComponent, float amount)
    {
        Debug.Assert(healthComponent.IsAlive, $"An already dead Component is being damaged (Health: {healthComponent.Health})");
        if (!healthComponent.IsAlive) return;

        Debug.Assert(amount >= 0, $"Damage amount ({amount}) cannot be negative.");
        amount = Math.Max(amount, 0);

        Console.WriteLine($"Entity took {amount} damage. Remaining Health: {healthComponent.Health - amount}");

        healthComponent.Health = Math.Max(healthComponent.Health - amount, 0);

        if (!healthComponent.IsAlive)
            Console.WriteLine($"Entity health depleted. Entity is now dead.");
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