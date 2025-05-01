using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class HealthComponentCoreTests
{
    [Fact]
    public void Should_InitializeWithCorrectValues()
    {
        // Arrange
        int health = 10, maxHealth = 20;

        // Act
        var healthComponent = new HealthComponent(maxHealth: maxHealth, health: health);

        // Assert
        Assert.Equal(health, healthComponent.Health);
        Assert.Equal(maxHealth, healthComponent.MaxHealth);
    }

    [Fact]
    public void Should_AllowUpdating_values()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Act
        healthComponent.MaxHealth = 25;
        healthComponent.Health = 15;

        // Assert
        Assert.Equal(15, healthComponent.Health);
        Assert.Equal(25, healthComponent.MaxHealth);
    }

    [Fact]
    public void Should_BeAlive_WhenHealthIsPositive()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Act
        var isAlive = healthComponent.IsAlive;

        // Assert
        Assert.True(isAlive);
    }

    [Fact]
    public void Should_NotBeAlive_WhenHealthIsZeroOrNegative()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 0);

        // Act
        var isAlive = healthComponent.IsAlive;

        // Assert
        Assert.False(isAlive);
    }

    [Fact]
    public void Should_ClampHealth_When_MaxHealthSmallerThanHealth()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 30, health: 20);

        // Act
        healthComponent.MaxHealth = 5;

        // Assert
        Assert.Equal(5, healthComponent.Health);
    }
}

public class HealthComponentDebugTests : DebugTest
{
    [SkippableFact]
    public void Should_ThrowException_When_NegativeHealth()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => healthComponent.Health = -5);
    }

    [SkippableFact]
    public void Should_ThrowException_When_HealthExceedsMaxHealth()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => healthComponent.Health = 25);
    }

    [SkippableFact]
    public void Should_ThrowException_When_MaxHealthNegative()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => healthComponent.MaxHealth = -5);
    }
}

public class HealthComponentReleaseTests : ReleaseTest
{
    [SkippableFact]
    public void Should_ClampHealth_When_NegativeHealth()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Act
        healthComponent.Health = -5;

        // Assert
        Assert.Equal(0, healthComponent.Health);
    }

    [SkippableFact]
    public void Should_ClampHealth_When_HealthExceedsMaxHealth()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);
        // Act
        healthComponent.Health = 25;

        // Assert
        Assert.Equal(healthComponent.MaxHealth, healthComponent.Health);
    }

    [SkippableFact]
    public void Should_NotBeAlive_WhenHealthIsNegative()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: -1);

        // Act
        var isAlive = healthComponent.IsAlive;

        // Assert
        Assert.False(isAlive);
    }

    [SkippableFact]
    public void Should_ClampMaxHealth_WhenNegative()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);
        // Act
        healthComponent.MaxHealth = -5;
        // Assert
        Assert.Equal(0, healthComponent.MaxHealth);
    }
}