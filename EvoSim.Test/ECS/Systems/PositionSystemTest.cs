using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class PositionSystemTest
{
    [Theory]
    [InlineData( 100,  100, null)]
    [InlineData(   0,  100, typeof(ArgumentOutOfRangeException))]
    [InlineData( 100,    0, typeof(ArgumentOutOfRangeException))]
    [InlineData(   0,    0, typeof(ArgumentOutOfRangeException))]
    [InlineData(-100,  100, typeof(ArgumentOutOfRangeException))]
    [InlineData( 100, -100, typeof(ArgumentOutOfRangeException))]
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

    public static IEnumerable<object[]> UpdateTestData => new List<object[]>
    {
        new object[] { 1f,
            100, 100,
            new PositionComponent { X = 150, Y = 50 },
            new PositionComponent { X =  50, Y = 50 }
        },
        new object[] { 1f,
            100, 100,
            new PositionComponent { X = -10, Y = 200 },
            new PositionComponent { X =  90, Y =   0 }
        },
        #region No delta time
        new object[] { 0f,
            100, 100,
            new PositionComponent { X = 150, Y =  50 },
            new PositionComponent { X = 150, Y =  50 }
        },
        new object[] { 0f,
            100, 100,
            new PositionComponent { X = -10, Y = 200 },
            new PositionComponent { X = -10, Y = 200 }
        }
        #endregion
    };

    [Theory]
    [MemberData(nameof(UpdateTestData))]
    public void UpdateTest(float deltaTime, int width, int height, PositionComponent positionComponent, PositionComponent expectedPositionComponent)
    {
        // Arrange
        var positionSystem = new PositionSystem(width, height);

        var world = new EcsEngine();

        var entity = world.CreateEntity();
        entity.AddComponent(positionComponent);

        // Act
        positionSystem.Update(world, deltaTime);

        // Assert
        Assert.Equal(expectedPositionComponent, positionComponent);
    }
}