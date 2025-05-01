using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class HealthComponent : IComponent
{
    private float _health;
    public float Health
    {
        get => _health;
        set
        {
#if DEBUG
            if (value > MaxHealth)
                throw new ArgumentOutOfRangeException(nameof(Health), "Health cannot exceed MaxHealth.");
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(Health), "Health cannot be negative.");
#endif
            _health = Math.Clamp(value, 0, MaxHealth);
        }
    }
    private float _maxHealth;

    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
#if DEBUG
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(MaxHealth), "MaxHealth cannot be negative.");
#endif
            _maxHealth = Math.Max(0, value);
            if (Health > MaxHealth)
                Health = MaxHealth;
        }
    }

    public bool IsAlive => Health > 0;


    public HealthComponent(float maxHealth = 0, float health = 0)
    {
        MaxHealth = maxHealth;
        Health = health;
    }
}