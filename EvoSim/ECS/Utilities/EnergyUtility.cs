using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;

namespace EvoSim.ECS.Utilities;

public static class EnergyUtility
{
    public static void UseEnergy(Entity entity, float amount)
    {
        if (!entity.HasComponent<EnergyComponent>())
        {
#if DEBUG
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");
#else
            return;
#endif
        }

        if (amount <= 0)
        {
#if DEBUG
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount to use cannot be negative.");
#else
            return;
#endif
        }


        var energyComponent = entity.GetComponent<EnergyComponent>();
        var newEnergy = energyComponent.Energy - amount;

        Console.WriteLine($"Entity {entity.Id} used {amount} energy. Remaining: {energyComponent.Energy}");

        if (newEnergy < 0)
        {
            energyComponent.Energy = 0;
            var damageTaken = -newEnergy;

            Console.WriteLine($"Entity {entity.Id} depleted energy and will lose {damageTaken} health.");

            if (entity.HasComponent<HealthComponent>())
            {
                HealthUtility.TakeDamage(entity, damageTaken);
            }
        }
        else
        {
            energyComponent.Energy = newEnergy;
        }
    }

    public static void GainEnergy(Entity entity, float amount)
    {
        if (!entity.HasComponent<EnergyComponent>())
        {
#if DEBUG
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");
#else
            return;
#endif
        }

        if (amount <= 0)
        {
#if DEBUG
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount to gain cannot be negative.");
#else
            return;
#endif
        }

        var energyComponent = entity.GetComponent<EnergyComponent>();
        energyComponent.Energy += amount;

        Console.WriteLine($"Entity {entity.Id} gained {amount} energy. Total: {energyComponent.Energy}");
    }
}