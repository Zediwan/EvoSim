using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

/// <summary>
/// Represents a velocity component with horizontal and vertical displacement values.
/// </summary>
/// <remarks>This class provides properties to define the horizontal (<see cref="DX"/>) and vertical (<see
/// cref="DY"/>)  components of velocity, as well as calculated properties for the total velocity magnitude  (<see
/// cref="TotalVelocity"/>) and its squared value (<see cref="TotalVelocitySquared"/>).</remarks>
public class VelocityComponent : IComponent
{
    /// <summary>
    /// Gets or sets the maximum velocity, in units per second, that an object can achieve.
    /// </summary>
    public float MaxVelocity { get; set; }
    /// <summary>
    /// Gets or sets the horizontal displacement value.
    /// </summary>
    public float DX { get; set; }
    /// <summary>
    /// Gets or sets the vertical displacement value.
    /// </summary>
    public float DY { get; set; }

    /// <summary>
    /// Gets the squared magnitude of the total velocity, calculated as the sum of the squares of the X and Y velocity
    /// components.
    /// </summary>
    public float TotalVelocitySquared => DX * DX + DY * DY;

    /// <summary>
    /// Gets the total velocity as a single scalar value.
    /// </summary>
    /// <remarks>
    /// Rather use <see cref="TotalVelocitySquared"/> when comparing velocities, to avoid the computational cost of a square root operation.
    /// </remarks>
    public float TotalVelocity => (float)Math.Sqrt(TotalVelocitySquared);
}


