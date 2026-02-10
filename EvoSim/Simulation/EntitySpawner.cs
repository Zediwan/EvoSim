using EvoSim.ECS.Components;
using EvoSim.ECS.Core;

namespace EvoSim.Simulation;

public class EntitySpawner(EcsEngine ecsEngine, int width, int height)
{
    private readonly Random _random = new();

    public float ChanceOfInitialAcceleration = 1f;
    public float InitialAcceleration = 10;

    public float ChanceOfInitialVelocity = 1;
    public float InitialVelocity = 10;
    
    public int ColorRangeMinR = 0;
    public int ColorRangeMaxR = 256;
    public int ColorRangeMinG = 0;
    public int ColorRangeMaxG = 256;
    public int ColorRangeMinB = 0;
    public int ColorRangeMaxB = 256;

    public void SpawnEntity()
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
            R = (byte)_random.Next(ColorRangeMinR, ColorRangeMaxR),
            G = (byte)_random.Next(ColorRangeMinG, ColorRangeMaxG),
            B = (byte)_random.Next(ColorRangeMinB, ColorRangeMaxB)
        });

        entity.AddComponent(new VelocityComponent
        {
            VX = Random.Shared.NextDouble() < ChanceOfInitialVelocity
                ? (float)(_random.NextDouble() - 0.5) * InitialVelocity
                : 0,
            VY = Random.Shared.NextDouble() < ChanceOfInitialVelocity
                ? (float)(_random.NextDouble() - 0.5) * InitialVelocity
                : 0
        });

        entity.AddComponent(new AccelerationComponent
        {
            AX = Random.Shared.NextDouble() < ChanceOfInitialAcceleration
                ? (float)(_random.NextDouble() - 0.5) * InitialAcceleration
                : 0,
            AY = Random.Shared.NextDouble() < ChanceOfInitialAcceleration
                ? (float)(_random.NextDouble() - 0.5) * InitialAcceleration
                : 0 
        });

        entity.AddComponent(new CombatComponent
        {
            Attack = _random.Next(100),
            Defense = _random.Next(100)
        });
    }
}