using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class EnergySystem : ISystem
{
    private float _drainRate = 1;

    /// <summary>
    /// The rate at which energy is drained from entities per second. This value must be non-negative, and any attempt to set it to a negative value will be clamped to zero.
    /// </summary>
    /// <remarks>
    /// Negative values will be clamped to 0.
    /// </remarks>
    public float DrainRate
    {
        get => _drainRate;
        set
        {
            Debug.Assert(value >= 0, $"Drain rate ({value}) cannot be negative.");
            _drainRate = Math.Max(0, value);
        }
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        if (deltaTime <= 0) return;

        foreach (var entity in ecsEngine.GetEntitiesWith<EnergyComponent>())
        {
            var missingEnergy = EnergyUtility.UseEnergy(entity.GetComponent<EnergyComponent>(), DrainRate * deltaTime);

            if (missingEnergy > 0 && entity.HasComponent<HealthComponent>())
            {
                HealthUtility.TakeDamage(entity.GetComponent<HealthComponent>(), missingEnergy);
            }
        }
    }
}