using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class HealthComponentTest
{
    [Fact]
    public void HealthTest()
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: 20, health: 10);

        // Act
        healthComponent.Health = -5;

        // Assert
        Assert.Equal(0, healthComponent.Health);
    }

    [Theory]
    [InlineData(10, 20, true)]
    [InlineData(0, 20, false)]
    [InlineData(-10, 20, false)]
    public void IsAliveTest(int health, int maxHealth, bool expected)
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: maxHealth, health: health);

        // Act
        var isAlive = healthComponent.IsAlive;

        // Assert
        Assert.Equal(expected, isAlive);
    }

    [Theory]
    [InlineData(10, 20, null, 15, 10, 15)]
    [InlineData(10, 20, null, 5, 5, 5)]
    [InlineData(10, 20, null, -5, 0, 0)]
    [InlineData(10, 20, 25, null, 20, 20)]
    public void MaxHealthTest(int initialHealth, int initialMaxHealth, int? newHealth, int? newMaxHealth,
        int expectedHealth, int expectedMaxHealth)
    {
        // Arrange
        var healthComponent = new HealthComponent(maxHealth: initialMaxHealth, health: initialHealth);

        // Act
        if (newMaxHealth.HasValue)
        {
            healthComponent.MaxHealth = newMaxHealth.Value;
        }

        if (newHealth.HasValue)
        {
            healthComponent.Health = newHealth.Value;
        }

        // Assert
        Assert.Equal(expectedHealth, healthComponent.Health);
        Assert.Equal(expectedMaxHealth, healthComponent.MaxHealth);
    }
}