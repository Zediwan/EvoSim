using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class EnergySystemTest
{
    [Theory]
    [InlineData(0.0f, 0.0f)]
    [InlineData(5.0f, 5.0f)]
    [InlineData(null, 1.0f)] // Null drain rate should use default value of 1.0f
    [InlineData(-1.0f, 0.0f)] // Negative drain rate should be clamped to zero
    public void DrainRateTest(float? drainRate, float expectedDrainRate)
    {
        // Arrange
        var energySystem = new EnergySystem();

        // Act
        if (drainRate.HasValue) energySystem.DrainRate = drainRate.Value;

        // Assert
        Assert.Equal(expectedDrainRate, energySystem.DrainRate);
    }

    [Theory]

    #region Tests with no drain rate set (using default)

    [InlineData(null, 1.0f, 50.0f, 100.0f, 49.0f)]
    [InlineData(null, 1.0f, 0.0f, 100.0f, 0.0f)]
    [InlineData(null, 1.0f, 50.0f, 50.0f, 49.0f)]

    #endregion

    #region Tests with zero drain rate (no energy should be drained)

    [InlineData(0.0f, 1.0f, 50.0f, 100.0f, 50.0f)]
    [InlineData(0.0f, 1.0f, 0.0f, 100.0f, 0.0f)]
    [InlineData(0.0f, 1.0f, 0.0f, 50.0f, 0.0f)]

    #endregion

    #region Tests with different drain rate

    [InlineData(2.0f, 1.0f, 0.0f, 100.0f, 0.0f)]
    [InlineData(2.0f, 1.0f, 50.0f, 100.0f, 48.0f)]
    [InlineData(2.0f, 1.0f, 50.0f, 50.0f, 48.0f)]

    #endregion

    #region Tests with zero delta Time

    [InlineData(2.0f, 0.0f, 50.0f, 100.0f, 50.0f)]
    [InlineData(2.0f, 2.0f, 0.0f, 100.0f, 0.0f)]
    [InlineData(2.0f, 2.0f, 50.0f, 50.0f, 46.0f)]

    #endregion

    public void UpdateTest(float? drainRate, float deltaTime, float initialEnergy, float initialMaxEnergy,
        float expectedEnergyPostUpdate)
    {
        // Arrange
        var energySystem = new EnergySystem();
        if (drainRate.HasValue) energySystem.DrainRate = drainRate.Value;

        var ecsEngine = new EcsEngine();

        var entity1 = ecsEngine.CreateEntity();
        entity1.AddComponent(new EnergyComponent(energy: initialEnergy, maxEnergy: initialMaxEnergy));

        var entity2 = ecsEngine.CreateEntity();
        entity2.AddComponent(new EnergyComponent(energy: initialEnergy, maxEnergy: initialMaxEnergy));

        // Act
        energySystem.Update(ecsEngine, deltaTime);

        // Assert
        var energyComponent = entity1.GetComponent<EnergyComponent>();
        Assert.Equal(expectedEnergyPostUpdate, energyComponent.Energy);

        var energyComponent2 = entity2.GetComponent<EnergyComponent>();
        Assert.Equal(expectedEnergyPostUpdate, energyComponent2.Energy);
    }
}