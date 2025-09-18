using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class VelocitySystem : ISystem
{
    public void Update(EcsEngine world, float deltaTime)
    {
        foreach (var entity in world.GetEntitiesWith(typeof(VelocityComponent), typeof(PositionComponent)))
        {
            VelocityUtility.ApplyVelocityToPosition(entity);
        }
    }
}

