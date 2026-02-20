using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class EnergySystemTest
{
    [Theory]
    [InlineData( 0.0f, 0.0f)]
    [InlineData( 5.0f, 5.0f)]
    [InlineData(null,  1.0f)] // Null drain rate should use default value of 1.0f
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

    public static IEnumerable<object[]> UpdateTestDataNoDrainRateDefined => new List<object[]>
    {
        new object[] { null, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            new EnergyComponent(Energy: 49, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { null, 1f,
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { null, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            new EnergyComponent(Energy: 49, MaxEnergy: 50),
            null,
            null,
        },
    };

    public static IEnumerable<object[]> UpdateTestDataZeroDrainRate => new List<object[]>
    {
        new object[] { 0.0f, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { 0.0f, 1f,
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { 0.0f, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            null,
            null,
        },
    };

    public static IEnumerable<object[]> UpdateTestDataZeroDeltaTime => new List<object[]>
    {
        new object[] { 2.0f, 0f,
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { 2.0f, 0f,
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            null,
            null,
        },
        new object[] { 2.0f, 0f,
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            null,
            null,
        },
    };

    public static IEnumerable<object[]> UpdateTestData => new List<object[]>
    {
        new object[] { 2.0f, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 100),
            new EnergyComponent(Energy: 48, MaxEnergy: 100),
            null,
            null
        },
        new object[] { 2.0f, 1f,
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            new EnergyComponent(Energy: 0, MaxEnergy: 100),
            null,
            null
        },
        new object[] { 2.0f, 1f,
            new EnergyComponent(Energy: 50, MaxEnergy: 50),
            new EnergyComponent(Energy: 48, MaxEnergy: 50),
            null,
            null
        },
    };

    public static IEnumerable<object[]> UpdateTestDataHealthComponent => new List<object[]>
    {
        new object[] { 2.0f, 1f,
            new EnergyComponent(Energy:   0, MaxEnergy: 100),
            new EnergyComponent(Energy:   0, MaxEnergy: 100),
            new HealthComponent(Health: 100, MaxHealth: 100),
            new HealthComponent(Health:  98, MaxHealth: 100)
        },
        new object[] { 2.0f, 1f,
            new EnergyComponent(Energy:   1, MaxEnergy:  50),
            new EnergyComponent(Energy:   0, MaxEnergy:  50),
            new HealthComponent(Health: 100, MaxHealth: 100),
            new HealthComponent(Health:  99, MaxHealth: 100)
        },
    };


    [Theory]
    [MemberData(nameof(UpdateTestData))]
    [MemberData(nameof(UpdateTestDataZeroDeltaTime))]
    [MemberData(nameof(UpdateTestDataZeroDrainRate))]
    [MemberData(nameof(UpdateTestDataNoDrainRateDefined))]
    [MemberData(nameof(UpdateTestDataHealthComponent))]
    public void UpdateTest(float? drainRate, float deltaTime,
        EnergyComponent energyComponent, EnergyComponent expectedEnergyComponent,
        HealthComponent? healthComponent, HealthComponent? expectedHealthComponent)
    {
        // Arrange
        var energySystem = new EnergySystem();
        if (drainRate.HasValue) energySystem.DrainRate = drainRate.Value;

        var ecsEngine = new EcsEngine();

        var entity = ecsEngine.CreateEntity();
        entity.AddComponent(energyComponent);
        if (healthComponent != null)
        {
            entity.AddComponent(healthComponent);
        }

        // Act
        energySystem.Update(ecsEngine, deltaTime);

        // Assert
        Assert.Equal(expectedEnergyComponent, energyComponent);
        if (healthComponent != null)
        {
            Assert.Equal(expectedHealthComponent, healthComponent);
        }
    }
}