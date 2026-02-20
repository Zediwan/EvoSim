using System.Diagnostics;
using EvoSim.ECS.Components;

namespace EvoSim.ECS.Utilities;

public static class HealthUtility
{
    /// <summary>
    /// Reduces the health of the given HealthComponent by the specified amount.
    /// </summary>
    /// <param name="healthComponent">The HealthComponent to apply damage to.</param>
    /// <param name="amount">The amount of damage to apply.</param>
    public static void TakeDamage(HealthComponent healthComponent, float amount)
    {
        Debug.Assert(healthComponent.IsAlive, $"An already dead Component is being damaged (Health: {healthComponent.Health})");
        if (!healthComponent.IsAlive) return;

        Debug.Assert(amount >= 0, $"Damage amount ({amount}) cannot be negative.");
        if (amount <= 0) return;

        healthComponent.Health -= amount;
    }

    /// <summary>
    /// Restores the health of the given HealthComponent by the specified amount, up to its maximum health. This method includes assertions to ensure that healing is only applied to alive components and that the heal amount is non-negative.
    /// </summary>
    /// <param name="healthComponent">The HealthComponent to heal.</param>
    /// <param name="amount">The amount of health to restore.</param>
    public static void Heal(HealthComponent healthComponent, float amount)
    {
        Debug.Assert(healthComponent.IsAlive, $"Trying to heal an already dead health Component. (Health: {healthComponent.Health})");
        if (!healthComponent.IsAlive) return;

        Debug.Assert(amount >= 0, $"Heal amount ({amount}) cannot be negative.");
        if (amount <= 0) return;

        healthComponent.Health += amount;
    }
}