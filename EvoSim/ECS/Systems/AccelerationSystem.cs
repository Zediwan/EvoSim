using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class AccelerationSystem : ISystem
{
    private const int MAX_ROTATION_ANGLE = 45; // degrees

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<AccelerationComponent>())
        {
            var accelerationComponent = entity.GetComponent<AccelerationComponent>();
            var (rotX, rotY) = VectorUtility.GetRotationVector(accelerationComponent.AX, accelerationComponent.AY,
                Random.Shared.NextDouble() * MAX_ROTATION_ANGLE * 2 - MAX_ROTATION_ANGLE);
            // Randomly change acceleration
            AccelerationUtility.ApplyAcceleration(entity, rotX, rotY, deltaTime);
            // Apply acceleration to velocity
            AccelerationUtility.ApplyAccelerationToVelocity(entity, deltaTime);
        }
    }
}

