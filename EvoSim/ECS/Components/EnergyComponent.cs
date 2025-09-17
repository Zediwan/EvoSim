using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

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
            Energy = Math.Min(Energy, MaxEnergy); // Ensure current energy does not exceed new max energy
        }
    }

    public EnergyComponent(float maxEnergy = 0, float energy = 0)
    {
        MaxEnergy = maxEnergy;
        Energy = energy;
    }
}