using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class PositionSystemTest
{
    public class ConstructorTests
    {
        [Fact]
        public void Should_CreateInstance_When_WidthAndHeightAreGreaterThanZero()
        {
            // Act
            var posSystem = new PositionSystem(100, 100);
            // Assert
            Assert.NotNull(posSystem);
            Assert.Equal(100, posSystem.Width);
            Assert.Equal(100, posSystem.Height);
        }

        [Theory]
        [InlineData(0, 100)]
        [InlineData(100, 0)]
        [InlineData(0, 0)]
        [InlineData(-100, 100)]
        [InlineData(100, -100)]
        [InlineData(-100, -100)]
        public void Should_ThrowArgumentOutOfRangeException_When_WidthOrHeightIsLessThanOrEqualToZero(int width, int height)
        {
            // Act
            var exception = Record.Exception(() => new PositionSystem(width, height));
            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentOutOfRangeException>(exception);
        }

    }

    public class UpdateTests
    {
        [Fact]
        public void Should_UpdatePositions_WithWraparound()
        {
            // Arrange
            var world = new EcsEngine();
            var entity1 = world.CreateEntity();
            var entity2 = world.CreateEntity();
            entity1.AddComponent(new PositionComponent { X = 150, Y = 50 });
            entity2.AddComponent(new PositionComponent { X = -10, Y = 200 });
            var positionSystem = new PositionSystem(100, 100);
            // Act
            positionSystem.Update(world, 0.016f); // Assuming a frame time of ~16ms
            // Assert
            var pos1 = entity1.GetComponent<PositionComponent>();
            var pos2 = entity2.GetComponent<PositionComponent>();
            Assert.Equal(50, pos1.X);
            Assert.Equal(50, pos1.Y);
            Assert.Equal(90, pos2.X);
            Assert.Equal(0, pos2.Y);
        }
    }
}
