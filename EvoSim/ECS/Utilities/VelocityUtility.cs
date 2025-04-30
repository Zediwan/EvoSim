using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class VelocityUtility
{
    public static void ApplyVelocityToPosition(Entity entity, int worldWidth, int worldHeight)
    {
        if (!entity.HasComponent<PositionComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have PositionComponent.");
        if (!entity.HasComponent<VelocityComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have VelocityComponent.");

        var positionComponent = entity.GetComponent<PositionComponent>();
        var velocityComponent = entity.GetComponent<VelocityComponent>();

        positionComponent.X += (int) velocityComponent.DX;
        positionComponent.Y += (int) velocityComponent.DY;

        // Wrap around the world, considering negative values
        positionComponent.X = ((positionComponent.X % worldWidth) + worldWidth) % worldWidth;
        positionComponent.Y = ((positionComponent.Y % worldHeight) + worldHeight) % worldHeight;
    }
}
