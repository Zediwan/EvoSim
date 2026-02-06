using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;
using System.Diagnostics;

namespace EvoSim.ECS.Utilities;

public static class EnergyUtility
{
    public static void UseEnergy(Entity entity, float amount)
    {
        Debug.Assert(entity.HasComponent<EnergyComponent>(), $"Entity {entity.Id} does not have a {nameof(EnergyComponent)}.");
        Debug.Assert(amount >= 0, $"Amount to use ({amount}) cannot be negative.");

        if (!entity.HasComponent<EnergyComponent>()) return;
        var energyComponent = entity.GetComponent<EnergyComponent>();

        amount = Math.Max(amount, 0);
        var newEnergy = energyComponent.Energy - amount;

        Console.WriteLine($"Entity {entity.Id} used {amount} energy. Remaining: {energyComponent.Energy}");

        if (newEnergy > 0)
        {
            energyComponent.Energy = newEnergy;
            return;
        }

        energyComponent.Energy = 0;
        var damageTaken = -newEnergy;

        Console.WriteLine($"Entity {entity.Id} depleted energy and will lose {damageTaken} health instead.");

        if (entity.HasComponent<HealthComponent>())
        {
            HealthUtility.TakeDamage(entity, damageTaken);
            return;
        }

        Console.WriteLine($"Entity {entity.Id} does not have a {nameof(HealthComponent)}.");
    }

    public static void GainEnergy(Entity entity, float amount)
    {
        Debug.Assert(entity.HasComponent<EnergyComponent>(), $"Entity {entity.Id} does not have a {nameof(EnergyComponent)}.");
        Debug.Assert(amount >= 0, $"Amount to gain ({amount}) cannot be negative.");

        if (!entity.HasComponent<EnergyComponent>()) return;
        var energyComponent = entity.GetComponent<EnergyComponent>();

        amount = Math.Max(amount, 0);
        energyComponent.Energy += amount;

        Console.WriteLine($"Entity {entity.Id} gained {amount} energy. Total: {energyComponent.Energy}");
    }
}