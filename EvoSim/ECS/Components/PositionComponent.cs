using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class PositionComponent : IComponent
{
    /// <summary>
    /// Represents the X-coordinate value in a two-dimensional space.
    /// </summary>
    public int X;
    /// <summary>
    /// Represents the Y-coordinate of a point or object in a two-dimensional space.
    /// </summary>
    public int Y;
}
