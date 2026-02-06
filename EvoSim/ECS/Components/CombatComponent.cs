using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a component that provides combat-related attributes.
/// </summary>
public class CombatComponent : IComponent
{
    private float _attack;

    /// <summary>
    /// Attack power of the entity. This value must be non-negative, and any attempt to set it to a negative value will be clamped to zero.
    /// </summary>
    public float Attack
    {
        get => _attack;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(Attack)} ({value}) must be non-negative.");
            _attack = Math.Max(value, 0);
        }
    }

    private float _defense;
    
    /// <summary>
    /// Defense power of the entity. This value must be non-negative, and any attempt to set it to a negative value will be clamped to zero.
    /// </summary>
    public float Defense
    {
        get => _defense;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(Defense)} ({value}) must be non-negative.");
            _defense = Math.Max(value, 0);
        }
    }

}