using System.Diagnostics;
using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class EnergySystem(int drainRate = 1) : ISystem
{
    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        Debug.Assert(deltaTime >= 0, $"Delta time ({deltaTime}) cannot be negative.");
        Debug.Assert(drainRate >= 0, $"Drain rate ({drainRate}) cannot be negative.");

        foreach (var entity in ecsEngine.GetEntitiesWith<EnergyComponent>())
        {
            EnergyUtility.UseEnergy(entity, (drainRate * deltaTime));
        }
    }
}
