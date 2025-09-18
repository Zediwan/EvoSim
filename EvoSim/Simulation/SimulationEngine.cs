using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Simulation;

public class SimulationEngine
{
    public EcsEngine EcsEngine { get; }
    private readonly EntitySpawner _spawner;

    public SimulationEngine(int width, int height)
    {
        EcsEngine = new EcsEngine();
        _spawner = new EntitySpawner(EcsEngine, width, height);

        // Register ECS systems
        EcsEngine.AddSystem(new EnergySystem(drainRate: 2));
        EcsEngine.AddSystem(new HealthSystem());
        EcsEngine.AddSystem(new AccelerationSystem());
        EcsEngine.AddSystem(new VelocitySystem());
        EcsEngine.AddSystem(new PositionSystem(width, height));
    }

    public void InitializeEntities(int count)
    {
        for (var i = 0; i < count; i++)
        {
            _spawner.SpawnEntity();
        }
    }

    public void Update(float deltaTime)
    {
        EcsEngine.Update(deltaTime);
    }
}
