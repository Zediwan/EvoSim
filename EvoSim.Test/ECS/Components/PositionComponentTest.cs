
using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class PositionComponentTest
{
    [Theory]
    [InlineData(10, 20, 10, 20)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(-5, -5, -5, -5)]
    [InlineData(null, null, 0, 0)]
    public void PositionTest(int? initialX, int? initialY, int expectedX, int expectedY)
    {
        // Arrange
        var component = new PositionComponent();

        // Act
        if(initialX.HasValue) component.X = initialX.Value;
        if(initialY.HasValue) component.Y = initialY.Value;

        // Assert
        Assert.Equal(expectedX, component.X);
        Assert.Equal(expectedY, component.Y);
    }
}
