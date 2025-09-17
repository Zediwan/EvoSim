using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

/// <summary>
/// Provides utility methods for applying velocity to entities and ensuring their positions remain within the boundaries
/// of a defined world.
/// </summary>
/// <remarks>This class contains methods to update an entity's position based on its velocity and to handle
/// wraparound behavior at the edges of the world. It is designed for use in systems where entities have position and
/// velocity components, such as in 2D game worlds or simulations.</remarks>
public static class VelocityUtility
{
    /// <summary>
    /// Updates the position of the specified entity by applying its velocity, with wraparound behavior at the world
    /// boundaries.
    /// </summary>
    /// <remarks>This method adjusts the entity's position based on its velocity and ensures that the position
    /// wraps around when it exceeds the world boundaries. If the entity does not have the required components or if the
    /// world dimensions are invalid, the method will return without making any changes.</remarks>
    /// <param name="entity">The entity whose position will be updated. The entity must have both a <see cref="PositionComponent"/> and a
    /// <see cref="VelocityComponent"/>.</param>
    /// <param name="worldWidth">The width of the world. Must be greater than zero.</param>
    /// <param name="worldHeight">The height of the world. Must be greater than zero.</param>
    public static void ApplyVelocityToPosition(Entity entity, int worldWidth, int worldHeight)
    {
        Debug.Assert(entity.HasComponent<PositionComponent>(), $"Entity {entity.Id} does not have a {nameof(PositionComponent)}.");
        Debug.Assert(entity.HasComponent<VelocityComponent>(), $"Entity {entity.Id} does not have a {nameof(VelocityComponent)}.");
        Debug.Assert(worldWidth > 0, $"World width ({worldWidth}) must be greater than zero.");
        Debug.Assert(worldHeight > 0, $"World height ({worldHeight}) must be greater than zero.");
        
        if (worldWidth <= 0) return;
        if (worldHeight <= 0) return;
        if (!entity.HasComponent<PositionComponent>()) return;
        if (!entity.HasComponent<VelocityComponent>()) return;

        var positionComponent = entity.GetComponent<PositionComponent>();
        var velocityComponent = entity.GetComponent<VelocityComponent>();

        // TODO: add max velocity limit based on entity traits

        positionComponent.X += (int) velocityComponent.DX;
        positionComponent.Y += (int) velocityComponent.DY;

        CalculateWraparoundPosition(positionComponent, worldWidth, worldHeight);
    }

    /// <summary>
    /// Adjusts the position of an entity to ensure it wraps around within the boundaries of the world.
    /// </summary>
    /// <remarks>This method ensures that the position wraps around horizontally and vertically when it
    /// exceeds the world boundaries. For example, if the position exceeds the maximum width, it will reappear at the
    /// beginning of the width range.</remarks>
    /// <param name="positionComponent">The position component representing the entity's current coordinates. The X and Y values will be modified to
    /// remain within the range [0, <paramref name="worldWidth"/> - 1] for X and [0, <paramref name="worldHeight"/> - 1]
    /// for Y.</param>
    /// <param name="worldWidth">The width of the world. Must be greater than zero.</param>
    /// <param name="worldHeight">The height of the world. Must be greater than zero.</param>
    private static void CalculateWraparoundPosition(PositionComponent positionComponent, int worldWidth, int worldHeight)
    {
        Debug.Assert(worldWidth > 0, $"World width ({worldWidth}) must be greater than zero.");
        Debug.Assert(worldHeight > 0, $"World height ({worldHeight}) must be greater than zero.");

        positionComponent.X = ((positionComponent.X % worldWidth) + worldWidth) % worldWidth;
        positionComponent.Y = ((positionComponent.Y % worldHeight) + worldHeight) % worldHeight;
    }
}
