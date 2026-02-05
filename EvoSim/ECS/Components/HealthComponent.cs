using EvoSim.ECS.Core;
using System.Diagnostics;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a health component for an entity, managing its current health, maximum health, and alive status.
/// </summary>
/// <remarks>This component enforces constraints on health values, ensuring that the current health cannot exceed
/// the maximum health or fall below zero. The <see cref="IsAlive"/> property provides a quick way to determine if the
/// entity is still alive.</remarks>
public class HealthComponent : IComponent
{
    private float _health;
    /// <summary>
    /// Gets or sets the current health value of the entity.
    /// </summary>
    /// <remarks>The health value cannot exceed <see cref="MaxHealth"/> or be less than 0.  Any value set
    /// outside this range will be automatically clamped.</remarks>
    public float Health
    {
        get => _health;
        set
        {
            Debug.Assert(value <= MaxHealth, $"{nameof(Health)} ({value}) cannot exceed {nameof(MaxHealth)} ({MaxHealth}).");
            Debug.Assert(value >= 0, $"{nameof(Health)} ({value}) cannot be negative.");

            _health = Math.Clamp(value, 0, MaxHealth);
        }
    }
    private float _maxHealth;

    /// <summary>
    /// Gets or sets the maximum health value for the entity.
    /// </summary>
    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(MaxHealth)} ({MaxHealth}) cannot be negative.");

            _maxHealth = Math.Max(0, value);
            // TODO: define if setting max Health should clamp current health or not.
            Health = Math.Min(Health, MaxHealth); // Ensure current health does not exceed new max health
        }
    }

    /// <summary>
    /// Gets a value indicating whether the entity is alive.
    /// </summary>
    public bool IsAlive => Health > 0;

    public HealthComponent(float maxHealth = 0, float health = 0)
    {
        MaxHealth = maxHealth;
        Health = health;
    }
}