using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class MovementSystem : ISystem
{
    private Random random = new Random();

    private readonly int _worldWidth;
    private readonly int _worldHeight;

    public MovementSystem(int worldWidth, int worldHeight)
    {
        _worldWidth = worldWidth;
        _worldHeight = worldHeight;
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        foreach (var entity in ecsEngine.GetEntitiesWith<MovementComponent>())
        {
            var movement = entity.GetComponent<MovementComponent>();

            movement.DX += (int)(random.Next(-1, 2) * deltaTime);
            movement.DY += (int)(random.Next(-1, 2) * deltaTime);

            MovementUtility.MoveEntity(entity, _worldWidth, _worldHeight);
        }
    }
}
