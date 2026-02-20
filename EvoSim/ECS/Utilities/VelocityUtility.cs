using EvoSim.ECS.Components;

namespace EvoSim.ECS.Utilities;

public static class VelocityUtility
{
    public static void ApplyVelocityToPosition(float deltaTime, PositionComponent positionComponent, VelocityComponent velocityComponent)
    {
        if (deltaTime <= 0) return;
        if (!velocityComponent.HasVelocity) return; // No movement if velocity is zero

        ClampVelocityToMax(velocityComponent);

        positionComponent.X += (int)(velocityComponent.VX * deltaTime);
        positionComponent.Y += (int)(velocityComponent.VY * deltaTime);
    }

    /// <summary>
    /// Clamps the velocity to the maximum allowed value if MaxVelocity is set.
    /// </summary>
    /// <param name="velocityComponent">The velocity component to clamp.</param>
    public static void ClampVelocityToMax(VelocityComponent velocityComponent)
    {
        var (clampedX, clampedY) = VectorUtility.Clamp(velocityComponent.VX, velocityComponent.VY, velocityComponent.MaxVelocity);
        velocityComponent.VX = clampedX;
        velocityComponent.VY = clampedY;
    }
}