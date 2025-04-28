using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;
public static class MovementUtility
{
    public static void MoveEntity(Entity entity, int worldWidth, int worldHeight)
    {
        if (!entity.HasComponent<PositionComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have PositionComponent.");
        if (!entity.HasComponent<MovementComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have MovementComponent.");

        var position = entity.GetComponent<PositionComponent>();
        var movement = entity.GetComponent<MovementComponent>();
        position.X += movement.DX;
        position.Y += movement.DY;

        var totalMovement = Math.Abs(movement.DX) + Math.Abs(movement.DY);
        if (totalMovement > 0)
        {
            EnergyUtility.UseEnergy(entity, totalMovement);
        }

        // Wrap around the world, considering negative values
        position.X = ((position.X % worldWidth) + worldWidth) % worldWidth;
        position.Y = ((position.Y % worldHeight) + worldHeight) % worldHeight;
    }
}
