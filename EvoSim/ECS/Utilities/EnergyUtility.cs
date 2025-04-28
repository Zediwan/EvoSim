using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class EnergyUtility
{
    public static void UseEnergy(Entity entity, float amount)
    {
        if (!entity.HasComponent<EnergyComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");

        var energy = entity.GetComponent<EnergyComponent>();
        energy.Energy -= amount;

        Console.WriteLine($"Entity {entity.Id} used {amount} energy. Remaining: {energy.Energy}");

        if (energy.Energy < 0)
        {
            var excess = -energy.Energy;
            energy.Energy = 0;

            Console.WriteLine($"Entity {entity.Id} depleted energy and will lose {excess} health.");

            if (entity.HasComponent<HealthComponent>())
            {
                HealthUtility.TakeDamage(entity, excess);
            }
        }
    }

    public static void GainEnergy(Entity entity, float amount)
    {
        if (!entity.HasComponent<EnergyComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");

        var energy = entity.GetComponent<EnergyComponent>();
        energy.Energy = Math.Min(energy.Energy + amount, energy.MaxEnergy);

        Console.WriteLine($"Entity {entity.Id} gained {amount} energy. Total: {energy.Energy}");
    }
}