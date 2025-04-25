using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class HealthComponent : IComponent
{
    public int Health;
    public int MaxHealth;
    public bool IsAlive => Health > 0;
}