using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class PositionSystemTest
{
    [Theory]
    [InlineData(100, 100, null)]
    [InlineData(0, 100, typeof(ArgumentOutOfRangeException))]
    [InlineData(100, 0, typeof(ArgumentOutOfRangeException))]
    [InlineData(0, 0, typeof(ArgumentOutOfRangeException))]
    [InlineData(-100, 100, typeof(ArgumentOutOfRangeException))]
    [InlineData(100, -100, typeof(ArgumentOutOfRangeException))]
    [InlineData(-100, -100, typeof(ArgumentOutOfRangeException))]
    public void WidthHeightTest(int width, int height, Type? expectedExceptionType)
    {
        // Act
        var exception = Record.Exception(() => new PositionSystem(width, height));

        // Assert
        if (expectedExceptionType != null)
        {
            Assert.NotNull(exception);
            Assert.IsType(expectedExceptionType, exception);
        }
        else
        {
            Assert.Null(exception);
        }
    }

    [Theory]
    [InlineData(1, 100, 100, 150, 50, -10, 200, 50, 50, 90, 0)]
    public void UpdateTest(float deltaTime, int width, int height, int x1, int y1, int x2, int y2, int expectedX1,
        int expectedY1, int expectedX2, int expectedY2)
    {
        // Arrange
        var positionSystem = new PositionSystem(width, height);

        var world = new EcsEngine();

        var entity1 = world.CreateEntity();
        entity1.AddComponent(new PositionComponent { X = x1, Y = y1 });

        var entity2 = world.CreateEntity();
        entity2.AddComponent(new PositionComponent { X = x2, Y = y2 });

        // Act
        positionSystem.Update(world, deltaTime);

        // Assert
        var pos1 = entity1.GetComponent<PositionComponent>();
        Assert.Equal(expectedX1, pos1.X);
        Assert.Equal(expectedY1, pos1.Y);

        var pos2 = entity2.GetComponent<PositionComponent>();
        Assert.Equal(expectedX2, pos2.X);
        Assert.Equal(expectedY2, pos2.Y);
    }
}