using System.Diagnostics;
using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record AccelerationComponent(
    float AX = 0,
    float AY = 0,
    float MaxAcceleration = float.MaxValue
) : IComponent
{
    private float _maxAcceleration = MaxAcceleration;

    /// <summary>
    /// The X-Axis acceleration
    /// </summary>
    public float AX { get; set; } = AX;

    /// <summary>
    /// The Y-Axis acceleration
    /// </summary>
    public float AY { get; set; } = AY;

    // TODO: Should setting this value clamp the Acceleration right away?
    // TODO: Should a MaxAcceleration of 0 be treated as "no limit" instead of clamping to 0? Or should we use a nullable float for MaxAcceleration to represent "no limit" more explicitly?
    /// <summary>
    /// The maximum allowed acceleration magnitude.
    /// </summary>
    /// <remarks>
    /// Negative values will be clamped to 0.
    /// </remarks>
    public float MaxAcceleration
    {
        get => _maxAcceleration;
        set
        {
            Debug.Assert(value >= 0, $"{nameof(MaxAcceleration)} ({value}) must be non-negative.");
            _maxAcceleration = Math.Max(0, value);
        }
    }

    /// <summary>
    /// Gets the squared magnitude of the acceleration vector.
    /// </summary>
    public float TotalAccelerationSquared => AX * AX + AY * AY;

    /// <summary>
    /// Gets the total acceleration as a single scalar value.
    /// </summary>
    /// <remarks>
    /// Rather use <see cref="TotalAccelerationSquared"/> when comparing accelerations, to avoid the computational cost of a square root operation.
    /// </remarks>
    public float TotalAcceleration => (float)Math.Sqrt(TotalAccelerationSquared);

    // TODO: consider a small epsilon for floating-point precision
    /// <summary>
    /// Gets a value indicating whether the object has a non-zero acceleration.
    /// </summary>
    public bool HasAcceleration => AX != 0 || AY != 0;
}