using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class AccelerationSystem : ISystem
{
    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<AccelerationComponent>())
        {
            // Randomly change acceleration
            AccelerationUtility.ApplyAcceleration(entity, (float)(Random.Shared.NextDouble() * 2 - 1), (float)(Random.Shared.NextDouble() * 2 - 1), deltaTime);
            // Apply acceleration to velocity
            AccelerationUtility.ApplyAccelerationToVelocity(entity, deltaTime);
        }
    }
}

