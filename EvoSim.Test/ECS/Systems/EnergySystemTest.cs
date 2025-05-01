using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class EnergySystemTest
{
    [Fact]
    public void Should_InitializeWithDefaultValues()
    {
        // Act
        var energySystem = new EnergySystem();
        // Assert
        Assert.NotNull(energySystem);
    }

    [Fact]
    public void Should_InitializeWithCorrectValues()
    {
        // Act
        var energySystem = new EnergySystem(5);
        // Assert
        Assert.NotNull(energySystem);
    }

    [Fact]
    public void Should_UseEnergy_When_Updating()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem();
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent = entity.GetComponent<EnergyComponent>();
        Assert.Equal(49, energyComponent.Energy);
    }

    [Fact]
    public void Should_UseEnergy_When_UpdatingWithMultipleEntities()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity1 = ecsEngine.CreateEntity();
        var entity2 = ecsEngine.CreateEntity();
        entity1.AddComponent(new EnergyComponent(maxEnergy:100, energy:50));
        entity2.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem();
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent1 = entity1.GetComponent<EnergyComponent>();
        var energyComponent2 = entity2.GetComponent<EnergyComponent>();
        Assert.Equal(49, energyComponent1.Energy);
        Assert.Equal(49, energyComponent2.Energy);
    }

    [Fact]
    public void Should_UseEnergy_When_UpdatingWithDifferentDrainRate()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem(10);
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent = entity.GetComponent<EnergyComponent>();
        Assert.Equal(40, energyComponent.Energy);
    }

    [Fact]
    public void Should_UseEnergy_When_UpdatingWithDifferentDrainRateWithMultipleEntities()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity1 = ecsEngine.CreateEntity();
        var entity2 = ecsEngine.CreateEntity();
        entity1.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        entity2.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem(10);
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent1 = entity1.GetComponent<EnergyComponent>();
        var energyComponent2 = entity2.GetComponent<EnergyComponent>();
        Assert.Equal(40, energyComponent1.Energy);
        Assert.Equal(40, energyComponent2.Energy);
    }

    [Fact]
    public void Should_NotUseEnergy_When_UpdatingWithZeroDrainRate()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem(0);
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent = entity.GetComponent<EnergyComponent>();
        Assert.Equal(50, energyComponent.Energy);
    }

    [Fact]
    public void Should_NotUseEnergy_When_UpdatingWithZeroDrainRateWithMultipleEntities()
    {
        // Arrange
        var ecsEngine = new EcsEngine();
        var entity1 = ecsEngine.CreateEntity();
        var entity2 = ecsEngine.CreateEntity();
        entity1.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        entity2.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
        var energySystem = new EnergySystem(0);
        // Act
        energySystem.Update(ecsEngine, 1.0f);
        // Assert
        var energyComponent1 = entity1.GetComponent<EnergyComponent>();
        var energyComponent2 = entity2.GetComponent<EnergyComponent>();
        Assert.Equal(50, energyComponent1.Energy);
        Assert.Equal(50, energyComponent2.Energy);
    }
}
