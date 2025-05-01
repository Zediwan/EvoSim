using EvoSim.ECS.Core;
using EvoSim.ECS.Components;

namespace EvoSim.Simulation;

public class EntitySpawner
{
    private readonly EcsEngine _ecsEngine;
    private readonly Random _random;
    private readonly int _width;
    private readonly int _height;

    public EntitySpawner(EcsEngine ecsEngine, int width, int height)
    {
        _ecsEngine = ecsEngine;
        _width = width;
        _height = height;
        _random = new Random();
    }

    public void SpawnEntity()
    {
        var entity = _ecsEngine.CreateEntity();

        var maxHealth = _random.Next(100);
        var health = _random.Next(maxHealth);
        entity.AddComponent(new HealthComponent(maxHealth: maxHealth, health: health));

        var maxEnergy = _random.Next(100);
        var energy = _random.Next(maxEnergy);
        entity.AddComponent(new EnergyComponent
        {
            Energy = energy,
            MaxEnergy = maxEnergy
        });

        entity.AddComponent(new PositionComponent
        {
            X = _random.Next(_width),
            Y = _random.Next(_height)
        });

        entity.AddComponent(new ColorComponent()
        {
            R = (byte)_random.Next(256),
            G = (byte)_random.Next(256),
            B = (byte)_random.Next(256)
        });

        entity.AddComponent(new VelocityComponent
        {
            DX = (float)(_random.NextDouble() * 2 - 1), // Random value between -1 and 1
            DY = (float)(_random.NextDouble() * 2 - 1)  // Random value between -1 and 1
        });

        entity.AddComponent(new AccelerationComponent
        {
            AX = (float)(_random.NextDouble() * 2 - 1), // Random value between -1 and 1
            AY = (float)(_random.NextDouble() * 2 - 1)  // Random value between -1 and 1
        });

    }
}