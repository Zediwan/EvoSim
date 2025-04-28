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

        var maxHealth = _random.Next(1000);
        var health = _random.Next(maxHealth);
        entity.AddComponent(new HealthComponent
        {
            Health = health,
            MaxHealth = maxHealth
        });

        var maxEnergy = _random.Next(1000);
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

        entity.AddComponent(new MovementComponent
        {
            DX = _random.Next(-1, 2),
            DY = _random.Next(-1, 2)
        });

        entity.AddComponent(new ColorComponent()
        {
            R = (byte)_random.Next(256),
            G = (byte)_random.Next(256),
            B = (byte)_random.Next(256)
        });
    }
}