using EvoSim.ECS.Components;

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
    /// Updates the position of an entity based on its velocity.
    /// </summary>
    /// <remarks>If the velocity is zero, the position remains unchanged. The method applies the velocity as
    /// integer values to the position.</remarks>
    /// <param name="positionComponent">The position component representing the current coordinates of the entity.</param>
    /// <param name="velocityComponent">The velocity component representing the movement vector of the entity.</param>
    /// <param name="deltaTime">Time that has passed since the last update.</param>
    public static void ApplyVelocityToPosition(PositionComponent positionComponent, VelocityComponent velocityComponent,
        float deltaTime)
    {
        if (!velocityComponent.HasVelocity) return; // No movement if velocity is zero

        // TODO: add max velocity limit based on entity traits

        positionComponent.X += (int)(velocityComponent.VX * deltaTime);
        positionComponent.Y += (int)(velocityComponent.VY * deltaTime);
    }
}