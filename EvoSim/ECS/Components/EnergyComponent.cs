using EvoSim.ECS.Core;
using System.Diagnostics;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a component that manages the energy level of an entity, including its current and maximum energy values.
/// </summary>
/// <remarks>The <see cref="EnergyComponent"/> class provides functionality to track and constrain the energy
/// level of an entity. The energy level is always clamped between 0 and the maximum energy level (<see
/// cref="MaxEnergy"/>). Modifying the maximum energy level may adjust the current energy level to ensure it does not
/// exceed the new maximum.</remarks>
public class EnergyComponent : IComponent
{
    private float _energy;

    /// <summary>
    /// Gets or sets the current energy level of the entity.
    /// </summary>
    public float Energy
    {
        get => _energy;
        set
        {
            Debug.Assert(value <= MaxEnergy, $"Energy ({value}) cannot exceed MaxEnergy ({MaxEnergy}).");
            Debug.Assert(value >= 0, $"Energy ({value}) cannot be negative.");

            _energy = Math.Clamp(value, 0, MaxEnergy);
        }
    }
    private float _maxEnergy;

    /// <summary>
    /// Gets or sets the maximum energy level.
    /// </summary>
    /// <remarks>Setting this property to a value less than the current energy level will reduce the current
    /// energy level to match the new maximum.</remarks>
    public float MaxEnergy
    {
        get => _maxEnergy;
        set
        {
            Debug.Assert(value >= 0, $"MaxEnergy ({MaxEnergy}) cannot be negative.");

            _maxEnergy = Math.Max(0, value);
            // TODO: define if setting max Energy should clamp current energy or not.
            Energy = Math.Min(Energy, MaxEnergy); // Ensure current energy does not exceed new max energy
        }
    }

    public EnergyComponent(float maxEnergy = 0, float energy = 0)
    {
        MaxEnergy = maxEnergy;
        Energy = energy;
    }
}