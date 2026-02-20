using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record EnergyComponent(
    float Energy = 0,
    float MaxEnergy = float.MaxValue
) : IComponent
{
    private float _energy = Energy;
    private float _maxEnergy = MaxEnergy;

    /// <summary>
    /// Current energy level.
    /// </summary>
    /// <remarks>
    /// Values will be clamped between 0 and <see cref="MaxEnergy"/>.
    /// </remarks>
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

    // TODO: Should setting this value clamp Energy right away?
    // TODO: Should a MaxEnergy of 0 be treated as "no limit" instead of clamping to 0? Or should we use a nullable float for MaxEnergy to represent "no limit" more explicitly?
    /// <summary>
    /// The maximum energy level.
    /// </summary>
    /// <remarks>
    /// Negative values will be clamped to 0.
    /// </remarks>
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
}