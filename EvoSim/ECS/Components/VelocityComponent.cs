using EvoSim.ECS.Core;
using System.Diagnostics;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a velocity component that defines the horizontal and vertical displacement values, as well as the maximum
/// velocity constraints for an object.
/// </summary>
/// <remarks>This component provides properties to manage and calculate velocity-related values, including the
/// total velocity and its squared magnitude. The <see cref="TotalVelocitySquared"/> property is recommended for
/// performance-sensitive comparisons, as it avoids the computational cost of calculating a square root.</remarks>
public class VelocityComponent : IComponent
{
    /// <summary>
    /// Gets or sets the horizontal displacement value.
    /// </summary>
    public float VX { get; set; }
    /// <summary>
    /// Gets or sets the vertical displacement value.
    /// </summary>
    public float VY { get; set; }

    /// <summary>
    /// Gets the squared magnitude of the total velocity, calculated as the sum of the squares of the X and Y velocity
    /// components.
    /// </summary>
    public float TotalVelocitySquared => VX * VX + VY * VY;

    /// <summary>
    /// Gets the total velocity as a single scalar value.
    /// </summary>
    /// <remarks>
    /// Rather use <see cref="TotalVelocitySquared"/> when comparing velocities, to avoid the computational cost of a square root operation.
    /// </remarks>
    public float TotalVelocity => (float)Math.Sqrt(TotalVelocitySquared);

    /// <summary>
    /// Gets a value indicating whether the object has a non-zero velocity.
    /// </summary>
    public bool HasVelocity => VX != 0 || VY != 0; // TODO: consider a small epsilon for floating-point precision
}


