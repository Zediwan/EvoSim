using System.Diagnostics;
using EvoSim.ECS.Components;

namespace EvoSim.ECS.Utilities;

public static class EnergyUtility
{
    /// <summary>
    /// Uses Energy. If the amount of energy to use exceeds the current energy, it will use all remaining energy and return the missing amount.
    /// </summary>
    /// <param name="energyComponent">The EnergyComponent to use energy from.</param>
    /// <param name="amount">The amount of energy to use.</param>
    /// <returns>The amount of energy that could not be used (i.e., the missing amount).</returns>
    public static float UseEnergy(EnergyComponent energyComponent, float amount)
    {
        Debug.Assert(amount >= 0, $"Amount to use ({amount}) cannot be negative.");
        if (amount <= 0) return 0;

        var missingEnergy = Math.Max(0, amount - energyComponent.Energy);
        energyComponent.Energy -= amount;

        return missingEnergy;
    }

    /// <summary>
    /// Gains Energy. 
    /// </summary>
    /// <remarks>
    /// Excess energy handling is managed in <see cref="EnergyComponent.Energy"/>.
    /// </remarks>
    /// <param name="energyComponent">The EnergyComponent to gain energy for.</param>
    /// <param name="amount">The amount of energy to gain.</param>
    public static void GainEnergy(EnergyComponent energyComponent, float amount)
    {
        Debug.Assert(amount >= 0, $"Amount to gain ({amount}) cannot be negative.");
        if (amount <= 0) return;

        energyComponent.Energy += amount;

        Console.WriteLine($"Entity gained {amount} energy. Total: {energyComponent.Energy}");
    }
}