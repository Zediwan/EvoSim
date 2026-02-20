using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public record PositionComponent(
    int X = 0,
    int Y = 0
) : IComponent
{
    /// <summary>
    /// X Position
    /// </summary>
    public int X { get; set; } = X;

    /// <summary>
    /// Y Position
    /// </summary>
    public int Y { get; set; } = Y;
}