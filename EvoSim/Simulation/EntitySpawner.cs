using EvoSim.ECS.Components;
using EvoSim.ECS.Core;

namespace EvoSim.Simulation;

public class EntitySpawner(EcsEngine ecsEngine, int width, int height)
{
    private readonly Random _random = new();

    public float ChanceOfInitialAcceleration = 1f;
    public float ChanceOfInitialVelocity = 1;

    public int ColorRangeMaxB = 256;
    public int ColorRangeMaxG = 256;
    public int ColorRangeMaxR = 256;
    public int ColorRangeMinB = 0;
    public int ColorRangeMinG = 0;
    public int ColorRangeMinR = 0;
    public float InitialAcceleration = 10;
    public float InitialVelocity = 10;

    public void SpawnEntity()
    {
        var entity = ecsEngine.CreateEntity();

        #region Color

        entity.AddComponent(new ColorComponent(
            R: (byte)_random.Next(ColorRangeMinR, ColorRangeMaxR),
            G: (byte)_random.Next(ColorRangeMinG, ColorRangeMaxG),
            B: (byte)_random.Next(ColorRangeMinB, ColorRangeMaxB))
        );

        #endregion


        #region Health

        var maxHealth = _random.Next(100);
        var health = _random.Next(maxHealth);
        entity.AddComponent(new HealthComponent(Health: health, MaxHealth: maxHealth));

        #endregion


        #region Energy

        var maxEnergy = _random.Next(100);
        var energy = _random.Next(maxEnergy);
        entity.AddComponent(new EnergyComponent(Energy: energy, MaxEnergy: maxEnergy));

        #endregion


        #region Position

        entity.AddComponent(new PositionComponent(X: _random.Next(width), Y: _random.Next(height)));

        #endregion


        #region Velocity

        float vx = 0;
        float vy = 0;

        if (_random.NextDouble() < ChanceOfInitialVelocity)
        {
            vx = (float)(_random.NextDouble() - 0.5) * InitialVelocity;
            vy = (float)(_random.NextDouble() - 0.5) * InitialVelocity;
        }

        entity.AddComponent(new VelocityComponent(VX: vx, VY: vy));

        #endregion


        #region Acceleration

        float ax = 0;
        float ay = 0;
        if (_random.NextDouble() < ChanceOfInitialAcceleration)
        {
            ax = (float)(_random.NextDouble() - 0.5) * InitialAcceleration;
            ay = (float)(_random.NextDouble() - 0.5) * InitialAcceleration;
        }

        entity.AddComponent(new AccelerationComponent(AX: ax, AY: ay));

        #endregion


        #region Combat

        entity.AddComponent(new CombatComponent
        {
            Attack = _random.Next(100),
            Defense = _random.Next(100)
        });

        #endregion
    }
}