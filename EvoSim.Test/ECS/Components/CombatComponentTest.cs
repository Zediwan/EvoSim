using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class CombatComponentTest
{
    [Theory]
    [InlineData(null, 0.0f)]
    [InlineData(0.0f, 0.0f)]
    [InlineData(10.0f, 10.0f)]
    [InlineData(-10.0f, 0.0f)]
    public void AttackTest(float? attack, float expectedAttack)
    {
        // Arrange
        var component = new CombatComponent();

        // Act
        if (attack.HasValue) component.Attack = attack.Value;

        // Assert
        Assert.Equal(expectedAttack, component.Attack);
    }

    [Theory]
    [InlineData(null, 0.0f)]
    [InlineData(0.0f, 0.0f)]
    [InlineData(10.0f, 10.0f)]
    [InlineData(-10.0f, 0.0f)]
    public void DefenseTest(float? defense, float expectedDefense)
    {
        // Arrange
        var component = new CombatComponent();

        // Act
        if (defense.HasValue) component.Defense = defense.Value;

        // Assert
        Assert.Equal(expectedDefense, component.Defense);
    }
}