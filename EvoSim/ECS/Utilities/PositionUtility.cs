using System.Diagnostics;
using EvoSim.ECS.Components;

namespace EvoSim.ECS.Utilities;

public static class PositionUtility
{
    /// <summary>
    /// Adjusts the position of an object to ensure it wraps around within the bounds of a defined world.
    /// </summary>
    /// <remarks>If the object's position exceeds the boundaries of the world, it is wrapped around to the
    /// opposite side. This ensures the position remains within the range [0, <paramref
    /// name="worldWidth"/>) for the X-coordinate and [0, <paramref name="worldHeight"/>) for the
    /// Y-coordinate.</remarks>
    /// <param name="positionComponent">The position component representing the object's current coordinates.</param>
    /// <param name="worldWidth">The width of the world. Must be greater than zero.</param>
    /// <param name="worldHeight">The height of the world. Must be greater than zero.</param>
    public static void ApplyWraparound(PositionComponent positionComponent, int worldWidth, int worldHeight)
    {
        Debug.Assert(worldWidth > 0, $"World width ({worldWidth}) must be greater than zero.");
        if (worldWidth <= 0) return;

        Debug.Assert(worldHeight > 0, $"World height ({worldHeight}) must be greater than zero.");
        if (worldHeight <= 0) return;

        // Early exit if already within bounds
        if (positionComponent.X >= 0 && positionComponent.X < worldWidth &&
            positionComponent.Y >= 0 && positionComponent.Y < worldHeight)
        {
            return;
        }

        positionComponent.X = ((positionComponent.X % worldWidth) + worldWidth) % worldWidth;
        positionComponent.Y = ((positionComponent.Y % worldHeight) + worldHeight) % worldHeight;
    }
}