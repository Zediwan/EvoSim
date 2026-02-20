using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class HealthComponentTest
{
    [Theory]
    [InlineData(10.0f, 20.0f, null, 15.0f, 10.0f, 15.0f)]
    [InlineData(10.0f, 20.0f, null, 5.0f, 5.0f, 5.0f)]
    [InlineData(10.0f, 20.0f, null, -5.0f, 0.0f, 0.0f)]
    [InlineData(10.0f, 20.0f, 25.0f, null, 20.0f, 20.0f)]
    public void MaxHealthTest(float initialHealth, float initialMaxHealth, float? newHealth, float? newMaxHealth,
        float expectedHealth, float expectedMaxHealth)
    {
        // Arrange
        var healthComponent = new HealthComponent(Health: initialHealth, MaxHealth: initialMaxHealth);

        // Act
        if (newMaxHealth.HasValue) healthComponent.MaxHealth = newMaxHealth.Value;
        if (newHealth.HasValue) healthComponent.Health = newHealth.Value;

        // Assert
        Assert.Equal(expectedHealth, healthComponent.Health);
        Assert.Equal(expectedMaxHealth, healthComponent.MaxHealth);
    }

    [Theory]
    [InlineData(10.0f, 20.0f, true)]
    [InlineData(0.0f, 20.0f, false)]
    [InlineData(-10.0f, 20.0f, false)]
    public void IsAliveTest(float health, float maxHealth, bool expected)
    {
        // Arrange
        var healthComponent = new HealthComponent(MaxHealth: maxHealth, Health: health);

        // Act
        var isAlive = healthComponent.IsAlive;

        // Assert
        Assert.Equal(expected, isAlive);
    }
}