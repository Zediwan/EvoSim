using EvoSim.ECS.Components;
using System.Diagnostics;

namespace EvoSim.ECS.Utilities;

/// <summary>
/// Provides utility methods for manipulating and adjusting object positions within a bounded world.
/// </summary>
/// <remarks>This class contains methods to handle position-related calculations, such as ensuring objects remain
/// within the boundaries of a rectangular world by applying wraparound effects.</remarks>
public class PositionUtility
{
    /// <summary>
    /// Adjusts the position of an object to ensure it wraps around within the boundaries of a rectangular world.
    /// </summary>
    /// <remarks>This method ensures that the object's position remains within the bounds of the world by
    /// applying a wraparound effect.  If the position exceeds the world dimensions, it is wrapped to the opposite
    /// side.</remarks>
    /// <param name="positionComponent">The position component representing the object's current coordinates. The X and Y values will be modified to fit
    /// within the specified world dimensions.</param>
    /// <param name="worldWidth">The width of the world. Must be greater than zero.</param>
    /// <param name="worldHeight">The height of the world. Must be greater than zero.</param>
    public static void CalculateWraparoundPosition(PositionComponent positionComponent, int worldWidth, int worldHeight)
    {
        Debug.Assert(worldWidth > 0, $"World width ({worldWidth}) must be greater than zero.");
        Debug.Assert(worldHeight > 0, $"World height ({worldHeight}) must be greater than zero.");

        if (worldWidth <= 0) return;
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
