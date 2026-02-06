using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;
using System.Diagnostics;

namespace EvoSim.ECS.Systems;

public class VelocitySystem : ISystem
{
    public void Update(EcsEngine world, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in world.GetEntitiesWith(typeof(VelocityComponent), typeof(PositionComponent)))
        {
            VelocityUtility.ApplyVelocityToPosition(
                entity.GetComponent<PositionComponent>(),
                entity.GetComponent<VelocityComponent>(), 
                deltaTime
                );
        }
    }
}