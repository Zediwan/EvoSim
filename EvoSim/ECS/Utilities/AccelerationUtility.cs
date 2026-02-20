using System.Diagnostics;
using EvoSim.ECS.Components;

namespace EvoSim.ECS.Utilities;

public static class AccelerationUtility
{
    /// <summary>
    /// Applies the given acceleration to the acceleration component over the specified delta time.
    /// </summary>
    /// <param name="deltaTime">The time over which to apply the acceleration.</param>
    /// <param name="accelerationComponent">The acceleration component to modify.</param>
    /// <param name="accelerationX">The acceleration to apply in the X direction.</param>
    /// <param name="accelerationY">The acceleration to apply in the Y direction.</param>
    public static void ApplyAcceleration(float deltaTime, AccelerationComponent accelerationComponent, float accelerationX = 0.0f, float accelerationY = 0.0f)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        if (accelerationX == 0 && accelerationY == 0) return;

        accelerationComponent.AX += accelerationX * deltaTime;
        accelerationComponent.AY += accelerationY * deltaTime;
    }

    /// <summary>
    /// Applies the given acceleration to the velocity component over the specified delta time, modifying the velocity accordingly.
    /// </summary>
    /// <param name="deltaTime">The time over which to apply the acceleration.</param>
    /// <param name="accelerationComponent">The acceleration component to use.</param>
    /// <param name="velocityComponent">The velocity component to modify.</param>
    /// <returns>The total acceleration applied.</returns>
    public static float ApplyAccelerationToVelocity(float deltaTime, AccelerationComponent accelerationComponent, VelocityComponent velocityComponent)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return 0;

        if (!accelerationComponent.HasAcceleration) return 0;

        velocityComponent.VX += accelerationComponent.AX * deltaTime;
        velocityComponent.VY += accelerationComponent.AY * deltaTime;

        return accelerationComponent.TotalAcceleration * deltaTime;
    }

    /// <summary>
    /// Clamps the acceleration to the maximum allowed value if MaxAcceleration is set.
    /// </summary>
    /// <param name="accelerationComponent">The acceleration component to clamp.</param>
    public static void ClampAccelerationToMax(AccelerationComponent accelerationComponent)
    {
        var (clampedX, clampedY) = VectorUtility.Clamp(accelerationComponent.AX, accelerationComponent.AY, accelerationComponent.MaxAcceleration);
        accelerationComponent.AX = clampedX;
        accelerationComponent.AY = clampedY;
    }

    /// <summary>
    /// Applies a random acceleration to the acceleration component based on the given rotation and delta time.
    /// </summary>
    /// <remarks>
    /// The random acceleration has a maximum magnitude of 1.
    /// </remarks>
    /// <param name="deltaTime">The time over which to apply the acceleration.</param>
    /// <param name="accelerationComponent">The acceleration component to modify.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The total random acceleration applied.</returns>
    public static float ApplyRandomAcceleration(float deltaTime, AccelerationComponent accelerationComponent, float rotation)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return 0;

        if (rotation == 0) return 0;

        var (randomRotationX, randomRotationY) = VectorUtility.GetRandomUnitRotationVector(accelerationComponent.AX, accelerationComponent.AY, rotation);
        var (movementX, movementY) = VectorUtility.Scale(randomRotationX, randomRotationY, Random.Shared.NextDouble());

        accelerationComponent.AX += movementX * deltaTime;
        accelerationComponent.AY += movementY * deltaTime;

        return (float)Math.Sqrt(movementX * movementX + movementY * movementY) * deltaTime;
    }
}