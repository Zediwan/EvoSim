using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class VelocitySystem : ISystem
{
    private readonly int _width;
    private readonly int _height;

    public VelocitySystem(int width, int height)
    {
        _width = width;
        _height = height;
    }

    public void Update(EcsEngine world, float deltaTime)
    {
        foreach (var entity in world.GetEntitiesWith(typeof(VelocityComponent), typeof(PositionComponent)))
        {
            VelocityUtility.ApplyVelocityToPosition(entity, _width, _height);
        }
    }
}

