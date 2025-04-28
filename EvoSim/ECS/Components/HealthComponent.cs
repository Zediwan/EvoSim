using EvoSim.ECS.Core;

namespace EvoSim.ECS.Components;

public class HealthComponent : IComponent
{
    public float Health;
    public float MaxHealth;
    public bool IsAlive => Health > 0;
}