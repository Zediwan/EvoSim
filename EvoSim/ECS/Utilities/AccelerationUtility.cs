using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public class AccelerationUtility
{
    public static void ApplyAcceleration(Entity entity, float accelerationX, float accelerationY, float deltaTime)
    {
        if (!entity.HasComponent<AccelerationComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have AccelerationComponent.");

        var accelerationComponent = entity.GetComponent<AccelerationComponent>();
        accelerationComponent.AX += accelerationX * deltaTime;
        accelerationComponent.AY += accelerationY * deltaTime;
    }

    public static void ApplyAccelerationToVelocity(Entity entity, float deltaTime)
    {
        if (!entity.HasComponent<AccelerationComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have AccelerationComponent.");
        if (!entity.HasComponent<VelocityComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have VelocityComponent.");

        var accelerationComponent = entity.GetComponent<AccelerationComponent>();
        var velocityComponent = entity.GetComponent<VelocityComponent>();

        velocityComponent.DX += accelerationComponent.AX * deltaTime;
        velocityComponent.DY += accelerationComponent.AY * deltaTime;

        if (accelerationComponent.TotalAccelerationSquared > 0)
        {
            EnergyUtility.UseEnergy(entity, accelerationComponent.TotalAccelerationSquared * deltaTime);
        }
    }
}
