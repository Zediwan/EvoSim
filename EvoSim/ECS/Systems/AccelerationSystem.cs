using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;
using System.Diagnostics;

namespace EvoSim.ECS.Systems;

public class AccelerationSystem : ISystem
{
    private const int MaxRotationAngle = 90; // degrees

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in ecsEngine.GetEntitiesWith<AccelerationComponent>())
        {
            var accelerationComponent = entity.GetComponent<AccelerationComponent>();

            var (rotX, rotY) = VectorUtility.GetRotationVector(accelerationComponent.AX, accelerationComponent.AY,
                Random.Shared.NextDouble() * MaxRotationAngle * 2 - MaxRotationAngle);

            // Randomly change acceleration
            AccelerationUtility.ApplyAcceleration(entity, rotX, rotY, deltaTime);

            // Apply acceleration to velocity
            AccelerationUtility.ApplyAccelerationToVelocity(entity, deltaTime);
        }
    }
}