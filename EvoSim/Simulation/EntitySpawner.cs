using EvoSim.ECS.Core;
using EvoSim.ECS.Components;

namespace EvoSim.Simulation;

public class EntitySpawner(EcsEngine ecsEngine, int width, int height)
{
    private readonly Random _random = new();

    public void SpawnEntity(double chanceOfAcceleration = .5)
    {
        var entity = ecsEngine.CreateEntity();

        var maxHealth = _random.Next(100);
        var health = _random.Next(maxHealth);
        entity.AddComponent(new HealthComponent(maxHealth: maxHealth, health: health));

        var maxEnergy = _random.Next(100);
        var energy = _random.Next(maxEnergy);
        entity.AddComponent(new EnergyComponent(maxEnergy: maxEnergy, energy: energy));

        entity.AddComponent(new PositionComponent
        {
            X = _random.Next(width),
            Y = _random.Next(height)
        });

        entity.AddComponent(new ColorComponent
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

        var ac = new AccelerationComponent();
        if (Random.Shared.NextDouble() < chanceOfAcceleration)
        {
            ac.AX = (float)(_random.NextDouble() * 2 - 1); // Random value between -1 and 1
            ac.AY = (float)(_random.NextDouble() * 2 - 1); // Random value between -1 and 1
        }
        entity.AddComponent(ac);
    }
}