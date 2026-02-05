using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;
using System.Diagnostics;

namespace EvoSim.ECS.Utilities;

/// <summary>
/// Provides utility methods for applying velocity to entities in a simulation.
/// </summary>
/// <remarks>This class contains methods for updating the position of entities based on their velocity. It assumes
/// that entities are composed of components, such as <see cref="PositionComponent"/> and <see
/// cref="VelocityComponent"/>, which are required for certain operations.</remarks>
public static class VelocityUtility
{
    /// <summary>
    /// Updates the position of the specified entity based on its velocity.
    /// </summary>
    /// <remarks>This method applies the velocity of the entity to its position. If the entity does not have both a
    /// <see cref="PositionComponent"/> and a <see cref="VelocityComponent"/>, the method will return without making any
    /// changes.</remarks>
    /// <param name="entity">The entity whose position will be updated. The entity must have both a <see cref="PositionComponent"/> and a <see
    /// cref="VelocityComponent"/>.</param>
    public static void ApplyVelocityToPosition(Entity entity)
    {
        Debug.Assert(entity.HasComponent<PositionComponent>(),
            $"Entity {entity.Id} does not have a {nameof(PositionComponent)}.");
        Debug.Assert(entity.HasComponent<VelocityComponent>(),
            $"Entity {entity.Id} does not have a {nameof(VelocityComponent)}.");

        if (!entity.HasComponent<PositionComponent>()) return;
        if (!entity.HasComponent<VelocityComponent>()) return;

        ApplyVelocityToPosition(entity.GetComponent<PositionComponent>(), entity.GetComponent<VelocityComponent>());
    }

    /// <summary>
    /// Updates the position of an entity based on its velocity.
    /// </summary>
    /// <remarks>If the velocity is zero, the position remains unchanged. The method applies the velocity as
    /// integer values to the position.</remarks>
    /// <param name="positionComponent">The position component representing the current coordinates of the entity.</param>
    /// <param name="velocityComponent">The velocity component representing the movement vector of the entity.</param>
    public static void ApplyVelocityToPosition(PositionComponent positionComponent, VelocityComponent velocityComponent)
    {
        if (velocityComponent.TotalVelocitySquared == 0) return; // No movement if velocity is zero

        // TODO: add max velocity limit based on entity traits

        positionComponent.X += (int)velocityComponent.VX;
        positionComponent.Y += (int)velocityComponent.VY;
    }
}
