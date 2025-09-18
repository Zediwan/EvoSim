using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a component that defines acceleration properties for an object in a 2D space.
/// </summary>
/// <remarks>This component provides properties to manage and calculate acceleration values, including individual 
/// acceleration components along the X and Y axes, as well as the total acceleration magnitude.</remarks>
public class AccelerationComponent : IComponent
{
    /// <summary>
    /// Gets or sets the maximum acceleration value for the object.
    /// </summary>
    public float MaxAcceleration { get; set; }
    /// <summary>
    /// Gets or sets the X-Axis acceleration of the object's position in a 2D space.
    /// </summary>
    public float AX { get; set; }
    /// <summary>
    /// Gets or sets the Y-Axis acceleration of the object's position in a 2D space.
    /// </summary>
    public float AY { get; set; }

    /// <summary>
    /// Gets the squared magnitude of the total acceleration vector.
    /// </summary>
    public float TotalAccelerationSquared => AX * AX + AY * AY;

    /// <summary>
    /// Gets the total acceleration as a single scalar value.
    /// </summary>
    /// <remarks>
    /// Rather use <see cref="TotalAccelerationSquared"/> when comparing accelerations, to avoid the computational cost of a square root operation.
    /// </remarks>
    public float TotalAcceleration => (float)Math.Sqrt(TotalAccelerationSquared);
}

