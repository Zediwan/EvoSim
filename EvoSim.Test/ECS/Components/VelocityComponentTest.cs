using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class VelocityComponentTest
{
    [Theory]
    [InlineData(3, 4, 5)] // 3-4-5 triangle
    [InlineData(5, 12, 13)] // 5-12-13 triangle
    [InlineData(8, 15, 17)] // 8-15-17 triangle
    [InlineData(0, 0, 0)] // Zero velocity
    public void TotalVelocityTest(float vx, float vy, float expectedTotalVelocity)
    {
        // Arrange
        var velocityComponent = new VelocityComponent { VX = vx, VY = vy };

        // Act
        var totalVelocity = velocityComponent.TotalVelocity;

        // Assert
        Assert.Equal(expectedTotalVelocity, totalVelocity, 3); // Allowing a small margin of error for floating point calculations
    }

    [Theory]
    [InlineData(3, 4, 25)] // 3-4-5 triangle
    [InlineData(5, 12, 169)] // 5-12-13 triangle
    [InlineData(8, 15, 289)] // 8-15-17 triangle
    [InlineData(0, 0, 0)] // Zero velocity
    public void TotalVelocitySquaredTests(float vx, float vy, float expectedTotalVelocitySquared)
    {
        // Arrange
        var velocityComponent = new VelocityComponent { VX = vx, VY = vy };

        // Act
        var totalVelocitySquared = velocityComponent.TotalVelocitySquared;

        // Assert
        Assert.Equal(expectedTotalVelocitySquared, totalVelocitySquared);
    }

    [Theory]
    [InlineData(0, 0, false)]
    [InlineData(3, 4, true)]
    [InlineData(-3, 4, true)]
    [InlineData(3, -4, true)]
    [InlineData(-3, -4, true)]
    [InlineData(1, 0, true)]
    [InlineData(-1, 0, true)]
    [InlineData(2, 0, true)]
    [InlineData(-2, 0, true)]
    [InlineData(float.Epsilon, 0, true)]
    [InlineData(0, float.Epsilon, true)]
    [InlineData(float.Epsilon, float.Epsilon, true)]
    [InlineData(-float.Epsilon, 0, true)]
    [InlineData(0, -float.Epsilon, true)]
    [InlineData(-float.Epsilon, -float.Epsilon, true)]

    public void HasVelocityTest(float vx, float vy, bool shouldHaveVelocity)
    {
        // Arrange
        var velocityComponent = new VelocityComponent { VX = vx, VY = vy };

        // Assert
        Assert.Equal(shouldHaveVelocity, velocityComponent.HasVelocity);
    }
}
