using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

/// <summary>
/// Provides utility methods for applying acceleration and updating velocity for entities with acceleration and velocity
/// components.
/// </summary>
/// <remarks>This class contains static methods to manipulate the acceleration and velocity of entities in a
/// simulation.  The methods assume that the entities have the required components, such as <see
/// cref="AccelerationComponent"/>  and <see cref="VelocityComponent"/>, and perform no operation if the necessary
/// components are missing or if  invalid parameters are provided. These utilities are designed to be used in
/// physics-based simulations or  similar systems where acceleration and velocity updates are required.</remarks>
public static class AccelerationUtility
{
    /// <summary>
    /// Applies acceleration to the specified entity over a given time interval.
    /// </summary>
    /// <remarks>This method updates the acceleration values of the entity's <see
    /// cref="AccelerationComponent"/> based on the provided acceleration and time interval. If <paramref
    /// name="deltaTime"/> is zero or the entity does not have an <see cref="AccelerationComponent"/>, the method exits
    /// without making changes.</remarks>
    /// <param name="entity">The entity to which the acceleration will be applied. The entity must have an <see
    /// cref="AccelerationComponent"/>.</param>
    /// <param name="accelerationX">The acceleration to apply along the X-axis, in units per second squared.</param>
    /// <param name="accelerationY">The acceleration to apply along the Y-axis, in units per second squared.</param>
    /// <param name="deltaTime">The time interval, in seconds, over which the acceleration is applied. Must be non-negative.</param>
    public static void ApplyAcceleration(Entity entity, float accelerationX, float accelerationY, float deltaTime)
    {
        Debug.Assert(entity.HasComponent<AccelerationComponent>(), $"Entity {entity.Id} does not have a {nameof(AccelerationComponent)}.");
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");

        if (deltaTime <= 0) return;
        if (!entity.HasComponent<AccelerationComponent>()) return;
        var accelerationComponent = entity.GetComponent<AccelerationComponent>();

        // TODO: add max acceleration limit based on entity traits

        accelerationComponent.AX += accelerationX * deltaTime;
        accelerationComponent.AY += accelerationY * deltaTime;
    }

    /// <summary>
    /// Updates the velocity of the specified entity by applying its acceleration over the given time interval.
    /// </summary>
    /// <remarks>This method modifies the velocity of the entity based on its acceleration and the specified
    /// time interval.  If the entity lacks the required components or if <paramref name="deltaTime"/> is zero or
    /// negative, the method performs no operation. Additionally, energy is consumed based on the total acceleration
    /// applied and the time interval.</remarks>
    /// <param name="entity">The entity whose velocity will be updated. The entity must have both <see cref="AccelerationComponent"/> and
    /// <see cref="VelocityComponent"/>.</param>
    /// <param name="deltaTime">The time interval, in seconds, over which the acceleration is applied. Must be non-negative.</param>
    public static void ApplyAccelerationToVelocity(Entity entity, float deltaTime)
    {
        Debug.Assert(entity.HasComponent<AccelerationComponent>(), $"Entity {entity.Id} does not have a {nameof(AccelerationComponent)}.");
        Debug.Assert(entity.HasComponent<VelocityComponent>(), $"Entity {entity.Id} does not have a {nameof(VelocityComponent)}.");
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");

        if (deltaTime <= 0) return;
        if (!entity.HasComponent<AccelerationComponent>()) return;
        if (!entity.HasComponent<VelocityComponent>()) return;

        var accelerationComponent = entity.GetComponent<AccelerationComponent>();
        var velocityComponent = entity.GetComponent<VelocityComponent>();

        // TODO: add max velocity limit based on entity traits

        velocityComponent.VX += accelerationComponent.AX * deltaTime;
        velocityComponent.VY += accelerationComponent.AY * deltaTime;

        if (accelerationComponent.TotalAccelerationSquared > 0)
        {
            EnergyUtility.UseEnergy(entity, accelerationComponent.TotalAccelerationSquared * deltaTime);
        }
    }
}
