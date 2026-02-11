using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class VelocityComponentTest
{
    [Theory]
    [InlineData(3.0f, 4.0f, 5.0f)] // 3-4-5 triangle
    [InlineData(5.0f, 12.0f, 13.0f)] // 5-12-13 triangle
    [InlineData(8.0f, 15.0f, 17.0f)] // 8-15-17 triangle
    [InlineData(0.0f, 0.0f, 0.0f)] // Zero velocity
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
    [InlineData(3.0f, 4.0f, 25.0f)] // 3-4-5 triangle
    [InlineData(5.0f, 12.0f, 169.0f)] // 5-12-13 triangle
    [InlineData(8.0f, 15.0f, 289.0f)] // 8-15-17 triangle
    [InlineData(0.0f, 0.0f, 0.0f)] // Zero velocity
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
    [InlineData(0.0f, 0.0f, false)]
    [InlineData(3.0f, 4.0f, true)]
    [InlineData(-3.0f, 4.0f, true)]
    [InlineData(3.0f, -4.0f, true)]
    [InlineData(-3.0f, -4.0f, true)]
    [InlineData(1.0f, 0.0f, true)]
    [InlineData(-1.0f, 0.0f, true)]
    [InlineData(2.0f, 0.0f, true)]
    [InlineData(-2.0f, 0.0f, true)]
    [InlineData(float.Epsilon, 0.0f, true)]
    [InlineData(0.0f, float.Epsilon, true)]
    [InlineData(float.Epsilon, float.Epsilon, true)]
    [InlineData(-float.Epsilon, 0.0f, true)]
    [InlineData(0.0f, -float.Epsilon, true)]
    [InlineData(-float.Epsilon, -float.Epsilon, true)]

    public void HasVelocityTest(float vx, float vy, bool shouldHaveVelocity)
    {
        // Arrange
        var velocityComponent = new VelocityComponent { VX = vx, VY = vy };

        // Assert
        Assert.Equal(shouldHaveVelocity, velocityComponent.HasVelocity);
    }
}
