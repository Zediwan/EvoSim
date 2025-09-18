using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class PositionSystem
{
    private readonly int _width;
    private readonly int _height;

    public PositionSystem(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Update(EcsEngine world, float deltaTime)
    {
        foreach (var entity in world.GetEntitiesWith(typeof(PositionComponent)))
        {
            PositionUtility.CalculateWraparoundPosition(entity.GetComponent<PositionComponent>(), _width, _height);
        }
    }
}
