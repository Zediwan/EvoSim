using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;

namespace EvoSim.ECS.Systems;

public class EnergySystem : ISystem
{
    private readonly int _drainRate;

    public EnergySystem(int drainRate = 1)
    {
        _drainRate = drainRate;
    }

    public void Update(EcsEngine ecsEngine, float deltaTime)
    {
        if (_drainRate == 0)
        {
#if DEBUG
            return;
#else
            return;
#endif
        }

        foreach (var entity in ecsEngine.GetEntitiesWith<EnergyComponent>())
        {
            EnergyUtility.UseEnergy(entity, (_drainRate * deltaTime));
        }
    }
}
