using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class EnergySystem : ISystem
{
    private int _drainRate = 1;

    /// <summary>
    /// The rate at which energy is drained from entities per second. This value must be non-negative, and any attempt to set it to a negative value will be clamped to zero.
    /// </summary>
    public int DrainRate
    {
        get => _drainRate;
        set
{
            Debug.Assert(value >= 0, $"Drain rate ({value}) cannot be negative.");
            _drainRate = Math.Max(value, 0);
        }
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        Debug.Assert(drainRate >= 0, $"Drain rate ({drainRate}) cannot be negative.");

        foreach (var entity in ecsEngine.GetEntitiesWith<EnergyComponent>())
        {
            EnergyUtility.UseEnergy(entity, (DrainRate * deltaTime));
        }
    }
}
