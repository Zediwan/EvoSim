using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

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
    /// Updates the position of the specified entity by applying its velocity.
    /// </summary>
    /// <remarks>This method adjusts the position of the entity based on its velocity. If the entity does not
    /// have the required components, the method will return without making any changes.</remarks>
    /// <param name="entity">The entity whose position will be updated. The entity must have both a <see cref="PositionComponent"/> and a
    /// <see cref="VelocityComponent"/>.</param>
    public static void ApplyVelocityToPosition(Entity entity)
    {
        Debug.Assert(entity.HasComponent<PositionComponent>(), $"Entity {entity.Id} does not have a {nameof(PositionComponent)}.");
        Debug.Assert(entity.HasComponent<VelocityComponent>(), $"Entity {entity.Id} does not have a {nameof(VelocityComponent)}.");
        
        if (!entity.HasComponent<PositionComponent>()) return;
        if (!entity.HasComponent<VelocityComponent>()) return;

        var positionComponent = entity.GetComponent<PositionComponent>();
        var velocityComponent = entity.GetComponent<VelocityComponent>();

        // TODO: add max velocity limit based on entity traits

        positionComponent.X += (int) velocityComponent.DX;
        positionComponent.Y += (int) velocityComponent.DY;
    }
}
