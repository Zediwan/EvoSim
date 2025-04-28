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
        EcsEngine.AddSystem(new EnergySystem());
        EcsEngine.AddSystem(new HealthSystem());
        EcsEngine.AddSystem(new MovementSystem(width, height));
    }

    public void InitializeEntities(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _spawner.SpawnEntity();
        }
    }

    public void Update(float deltaTime)
    {
        EcsEngine.Update(deltaTime);
    }
}
