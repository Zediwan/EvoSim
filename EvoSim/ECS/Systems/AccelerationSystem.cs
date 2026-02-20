using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class AccelerationSystem : ISystem
{
    /// <summary>
    /// Energy used per unit of acceleration applied.  Set to 0 to disable energy usage for acceleration.
    /// </summary>
    public float AccelerationEnergyRatio = 1;

    /// <summary>
    /// Maximum angle (in degrees) that can be applied to an entity's rotation. Set to 0 to disable random rotation changes.
    /// </summary>
    public int MaxRandomMovementRotationAngle;

    public bool RandomMovementEnabled = true;

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in ecsEngine.GetEntitiesWith(typeof(AccelerationComponent), typeof(VelocityComponent)))
        {
            var accelerationComponent = entity.GetComponent<AccelerationComponent>();
            var accelerationApplied = 0f;

            // Randomly change acceleration
            if (RandomMovementEnabled)
            {
                accelerationApplied += AccelerationUtility.ApplyRandomAcceleration(deltaTime, accelerationComponent, MaxRandomMovementRotationAngle);
            }

            // Apply acceleration to velocity
            accelerationApplied += AccelerationUtility.ApplyAccelerationToVelocity(deltaTime, accelerationComponent, entity.GetComponent<VelocityComponent>());

            if (accelerationApplied > 0 && AccelerationEnergyRatio > 0 && entity.HasComponent<EnergyComponent>())
            {
                var missingEnergy = EnergyUtility.UseEnergy(entity.GetComponent<EnergyComponent>(), accelerationApplied * AccelerationEnergyRatio);

                if (missingEnergy > 0 && entity.HasComponent<HealthComponent>())
                {
                    HealthUtility.TakeDamage(entity.GetComponent<HealthComponent>(), missingEnergy);
                }
            }
        }
    }
}