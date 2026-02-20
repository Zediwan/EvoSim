using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record HealthComponent(
    float Health = 0,
    float MaxHealth = float.MaxValue
) : IComponent
{
    private float _health = Health;
    private float _maxHealth = MaxHealth;

    /// <summary>
    /// The current health level.
    /// </summary>
    /// <remarks>
    /// Values will be clamped between 0 and <see cref="MaxHealth"/>.
    /// </remarks>
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

    // TODO: Should a MaxHealth of 0 be treated as "no limit" instead of clamping to 0? Or should we use a nullable float for MaxHealth to represent "no limit" more explicitly?
    /// <summary>
    /// The maximum health level.
    /// </summary>
    /// <remarks>
    /// Negative values will be clamped to 0.
    /// </remarks>
    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(MaxHealth)} ({MaxHealth}) cannot be negative.");
            _maxHealth = Math.Max(0, value);
            Health = Health; // Re-clamp current health to new max
        }
    }

    /// <summary>
    /// Gets a value indicating whether the entity is alive.
    /// </summary>
    public bool IsAlive => Health > 0;
}