using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class EnergyComponentTest
{
    [Theory]
    [InlineData(10.0f, 20.0f, -5.0f, null, 0.0f, 20.0f)]
    [InlineData(10.0f, 20.0f, 25.0f, null, 20.0f, 20.0f)]
    [InlineData(10.0f, 20.0f, null, -5.0f, 0.0f, 0.0f)]
    [InlineData(10.0f, 20.0f, null, 5.0f, 5.0f, 5.0f)]
    [InlineData(10.0f, 20.0f, 15.0f, 25.0f, 15.0f, 25.0f)]
    [InlineData(10.0f, 20.0f, null, null, 10.0f, 20.0f)]
    public void MaxEnergyTest(float initialEnergy, float initialMaxEnergy, float? newEnergy, float? newMaxEnergy,
        float expectedEnergy, float expectedMaxEnergy)
    {
        // Arrange
        var energyComponent = new EnergyComponent(Energy: initialEnergy, MaxEnergy: initialMaxEnergy);

        // Act
        if (newEnergy.HasValue) energyComponent.Energy = newEnergy.Value;
        if (newMaxEnergy.HasValue) energyComponent.MaxEnergy = newMaxEnergy.Value;

        // Assert
        Assert.Equal(expectedEnergy, energyComponent.Energy);
        Assert.Equal(expectedMaxEnergy, energyComponent.MaxEnergy);
    }
}