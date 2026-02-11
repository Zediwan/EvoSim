using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class AccelerationComponentTest
{
    [Theory]
    [InlineData(0.0f, 0.0f)]
    [InlineData(10.0f, 10.0f)]
    [InlineData(-10.0f, 0.0f)]
    public void MaxAccelerationTest(float maxAcceleration, float expectedMaxAcceleration)
    {
        // Arrange
        var component = new AccelerationComponent();

        // Act
        component.MaxAcceleration = maxAcceleration;

        // Assert
        Assert.Equal(expectedMaxAcceleration, component.MaxAcceleration);
    }

    [Theory]
    [InlineData(3, 4, 5)] // 3-4-5 triangle
    [InlineData(5, 12, 13)] // 5-12-13 triangle
    [InlineData(8, 15, 17)] // 8-15-17 triangle
    [InlineData(0, 0, 0)] // Zero acceleration
    public void TotalAccelerationTest(float ax, float ay, float expectedTotalAcceleration)
    {
        // Arrange
        var component = new AccelerationComponent { AX = ax, AY = ay };

        // Act
        var totalAcceleration = component.TotalAcceleration;

        // Assert
        Assert.Equal(expectedTotalAcceleration, totalAcceleration,
            3); // Allowing a small margin of error for floating point calculations
    }

    [Theory]
    [InlineData(3, 4, 25)] // 3-4-5 triangle
    [InlineData(5, 12, 169)] // 5-12-13 triangle
    [InlineData(8, 15, 289)] // 8-15-17 triangle
    [InlineData(0, 0, 0)] // Zero acceleration
    public void TotalAccelerationSquaredTest(float ax, float ay,
        float expectedTotalAccelerationSquared)
    {
        // Arrange
        var component = new AccelerationComponent { AX = ax, AY = ay };

        // Act
        var totalAccelerationSquared = component.TotalAccelerationSquared;

        // Assert
        Assert.Equal(expectedTotalAccelerationSquared, totalAccelerationSquared,
            3); // Allowing a small margin of error for floating point calculations
    }
}