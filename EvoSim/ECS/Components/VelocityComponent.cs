using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record VelocityComponent(
    float VX = 0,
    float VY = 0,
    float MaxVelocity = float.MaxValue
) : IComponent
{
    private float _maxVelocity = MaxVelocity;

    /// <summary>
    /// The X-Axis velocity
    /// </summary>
    public float VX { get; set; } = VX;

    /// <summary>
    /// The Y-Axis velocity
    /// </summary>
    public float VY { get; set; } = VY;

    // TODO: Should setting this value clamp the Velocity right away?
    // TODO: Should a MaxVelocity of 0 be treated as "no limit" instead of clamping to 0? Or should we use a nullable float for MaxVelocity to represent "no limit" more explicitly?
    /// <summary>
    /// The maximum allowed velocity magnitude
    /// </summary>
    /// <remarks>
    /// Negative values will be clamped to 0.
    /// </remarks>
    public float MaxVelocity
    {
        get => _maxVelocity;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(MaxVelocity)} ({value}) must be non-negative.");
            _maxVelocity = Math.Max(0, value);
        }
    }

    /// <summary>
    /// Gets the squared magnitude of the velocity vector.
    /// </summary>
    public float TotalVelocitySquared => VX * VX + VY * VY;

    /// <summary>
    /// Gets the total velocity as a single scalar value.
    /// </summary>
    /// <remarks>
    /// Rather use <see cref="TotalVelocitySquared"/> when comparing velocities, to avoid the computational cost of a square root operation.
    /// </remarks>
    public float TotalVelocity => (float)Math.Sqrt(TotalVelocitySquared);

    // TODO: consider a small epsilon for floating-point precision
    /// <summary>
    /// Gets a value indicating whether the object has a non-zero velocity.
    /// </summary>
    public bool HasVelocity => VX != 0 || VY != 0;
}