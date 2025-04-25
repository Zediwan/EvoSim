using EvoSim.ECS.Components;
using EvoSim.ECS.Entities;
using Microsoft.Extensions.Logging;

namespace EvoSim.ECS.Utilities;

public static class EnergyUtility
{
    public static void UseEnergy(Entity entity, int amount, ILogger logger = null)
    {
        if (!entity.HasComponent<EnergyComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");

        var energy = entity.GetComponent<EnergyComponent>();
        energy.Energy -= amount;

        logger?.LogInformation("Entity {EntityId} used {Amount} energy. Remaining: {Energy}",
            entity.Id, amount, energy.Energy);

        if (energy.Energy < 0)
        {
            int excess = -energy.Energy;
            energy.Energy = 0;

            logger?.LogWarning("Entity {EntityId} depleted energy and will lose {Excess} health.", entity.Id, excess);

            if (entity.HasComponent<HealthComponent>())
            {
                HealthUtility.TakeDamage(entity, excess, logger);
            }
        }
    }

    public static void GainEnergy(Entity entity, int amount, ILogger logger = null)
    {
        if (!entity.HasComponent<EnergyComponent>())
            throw new InvalidOperationException($"Entity {entity.Id} does not have EnergyComponent.");

        var energy = entity.GetComponent<EnergyComponent>();
        energy.Energy = Math.Min(energy.Energy + amount, energy.MaxEnergy);

        logger?.LogInformation("Entity {EntityId} gained {Amount} energy. Total: {Energy}",
            entity.Id, amount, energy.Energy);
    }
}