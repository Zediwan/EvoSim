using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class EnergyUtilityTest
{
    public static IEnumerable<object[]> UseEnergyTestData => new List<object[]>
    {
        new object[]
        {
            1f,
            0f,
            new EnergyComponent(Energy: 100),
            new EnergyComponent(Energy:  99)
        },
        // Test with zero amount to gain - energy should remain unchanged
        new object[]
        {
            0f,
            0f,
            new EnergyComponent(Energy: 99),
            new EnergyComponent(Energy: 99)
        },
        // Test with negative amount to gain - energy should remain unchanged
        new object[]
        {
            -10f,
            0f,
            new EnergyComponent(Energy: 99),
            new EnergyComponent(Energy: 99)
        },
        // Test with amount to use greater than current energy - energy should be set to 0 and missing energy should be returned
        new object[]
        {
            150f,
            50f,
            new EnergyComponent(Energy: 100),
            new EnergyComponent(Energy:   0)
        }
    };
    
    [Theory, MemberData(nameof(UseEnergyTestData))]
    public void UseEnergyTest(float amountToUse, float expectedMissingEnergy, EnergyComponent energyComponent,
        EnergyComponent expectedEnergyComponent)
    {
        // Act
        var actualMissingEnergy = EnergyUtility.UseEnergy(energyComponent, amountToUse);

        // Assert
        Assert.Equal(expectedMissingEnergy, actualMissingEnergy);
        Assert.Equal(expectedEnergyComponent, energyComponent);
    }

    public static IEnumerable<object[]> GainEnergyTestData => new List<object[]>
    {
        new object[]
        {
            1f,
            new EnergyComponent(Energy:  99),
            new EnergyComponent(Energy: 100)
        },
        new object[]
        {
            1f,
            new EnergyComponent(Energy:  99, MaxEnergy: 100),
            new EnergyComponent(Energy: 100, MaxEnergy: 100)
        },
        // Test with amount to gain that would exceed MaxEnergy - energy should be capped at MaxEnergy
        new object[]
        {
            1f,
            new EnergyComponent(Energy: 99, MaxEnergy: 99),
            new EnergyComponent(Energy: 99, MaxEnergy: 99)
        },
        // Test with amount to gain that would exceed MaxEnergy - energy should be capped at MaxEnergy
        new object[]
        {
            10f,
            new EnergyComponent(Energy: 90, MaxEnergy: 99),
            new EnergyComponent(Energy: 99, MaxEnergy: 99)
        },
        // Test with zero amount to gain - energy should remain unchanged
        new object[]
        {
            0f,
            new EnergyComponent(Energy: 99),
            new EnergyComponent(Energy: 99)
        },
        // Test with negative amount to gain - energy should remain unchanged
        new object[]
        {
            -10f,
            new EnergyComponent(Energy: 99),
            new EnergyComponent(Energy: 99)
        }
    };

    [Theory, MemberData(nameof(GainEnergyTestData))]
    public void GainEnergyTest(float amountToGain, EnergyComponent energyComponent,
        EnergyComponent expectedEnergyComponent)
    {
        // Act
        EnergyUtility.GainEnergy(energyComponent, amountToGain);

        // Assert
        Assert.Equal(expectedEnergyComponent, energyComponent);
    }
}