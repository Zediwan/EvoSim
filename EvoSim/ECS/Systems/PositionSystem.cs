using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class PositionSystem(int width, int height) : ISystem
{
    public void Update(EcsEngine world, float deltaTime)
    {
        foreach (var entity in world.GetEntitiesWith(typeof(PositionComponent)))
        {
            PositionUtility.CalculateWraparoundPosition(entity.GetComponent<PositionComponent>(), width, height);
        }
    }
}
