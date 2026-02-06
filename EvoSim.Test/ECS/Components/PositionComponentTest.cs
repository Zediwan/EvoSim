
using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class PositionComponentTest
{
    [Fact]
    public void PositionComponent_Initialization_Works()
    {
        // Arrange
        const int x = 10;
        const int y = 20;
        // Act
        var positionComponent = new PositionComponent { X = x, Y = y };
        // Assert
        Assert.Equal(x, positionComponent.X);
        Assert.Equal(y, positionComponent.Y);
    }

    [Fact]
    public void PositionComponent_DefaultInitialization_Works()
    {
        // Act
        var positionComponent = new PositionComponent();
        // Assert
        Assert.Equal(0.0f, positionComponent.X);
        Assert.Equal(0.0f, positionComponent.Y);
    }

    [Fact]
    public void PositionComponent_SetValues_Works()
    {
        // Arrange
        var positionComponent = new PositionComponent();
        const int newX = 15;
        const int newY = 25;
        // Act
        positionComponent.X = newX;
        positionComponent.Y = newY;
        // Assert
        Assert.Equal(newX, positionComponent.X);
        Assert.Equal(newY, positionComponent.Y);
    }
}
