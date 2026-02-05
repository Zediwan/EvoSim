using EvoSim.ECS.Components;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class PositionUtilityTest
{
    public class ApplyWraparoundTests
    {
        [Fact]
        public void Should_NotWraparound_When_InsideBounds()
        {
            // Arrange
            var positionComponent = new PositionComponent { X = 50, Y = 50 };
            var worldWidth = 100;
            var worldHeight = 100;
            // Act
            PositionUtility.ApplyWraparound(positionComponent, worldWidth, worldHeight);
            // Assert
            Assert.Equal(50, positionComponent.X);
        }

        [Theory]
        [InlineData(10, -10, 10, 90)]
        [InlineData(-10, 10, 90, 10)]
        [InlineData(-10, -10, 90, 90)]
        [InlineData(110, 10, 10, 10)]
        [InlineData(10, 110, 10, 10)]
        [InlineData(110, 120, 10, 20)]
        public void Should_Wraparound_When_OutsideBounds(int x, int y, int expectedX, int expectedY)
        {
            // Arrange
            var positionComponent = new PositionComponent { X = x, Y = y };
            var worldWidth = 100;
            var worldHeight = 100;
            // Act
            PositionUtility.ApplyWraparound(positionComponent, worldWidth, worldHeight);
            // Assert
            Assert.Equal(expectedX, positionComponent.X);
            Assert.Equal(expectedY, positionComponent.Y);
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableTheory]
            [InlineData(100, 0, 10, 0)] // Invalid world height (0), no wraparound
            [InlineData(100, 0, -10, 0)] // Invalid world height (0), negative out of bounds x but skip wraparound
            [InlineData(100, 0, 110, 0)] // Invalid world height (0), positive out of bounds x but skip wraparound
            [InlineData(100, 0, 10, -10)] // Invalid world height (0), negative y but skip wraparound
            [InlineData(100, 0, 10, 10)] // Invalid world height (0), positive out of bounds y but skip wraparound
            [InlineData(0, 100, 0, 10)] // Invalid world width (0), no wraparound
            [InlineData(0, 100, -10, 10)] // Invalid world height (0), negative out of bounds x but skip wraparound
            [InlineData(0, 100, 10, 10)] // Invalid world height (0), positive out of bounds x but skip wraparound
            [InlineData(0, 100, 0, -10)] // Invalid world height (0), negative out of bounds y but skip wraparound
            [InlineData(0, 100, 0, 110)] // Invalid world height (0), positive out of bounds y but skip wraparound
            [InlineData(0, 0, 0, 0)] // Invalid world width and height (0), no wraparound
            [InlineData(0, 0, -10, 0)] // Invalid world width and height (0), negative out of bounds x but skip wraparound
            [InlineData(0, 0, 10, 0)] // Invalid world width and height (0), positive out of bounds x but skip wraparound
            [InlineData(0, 0, 0, -10)] // Invalid world width and height (0), negative out of bounds y but skip wraparound
            [InlineData(0, 0, 0, 10)] // Invalid world width and height (0), positive out of bounds y but skip wraparound
            [InlineData(100, -10, 10, 0)] // Invalid (negative) world height, no wraparound
            [InlineData(100, -10, -20, 0)] // Invalid (negative) world height, negative out of bounds x but skip wraparound
            [InlineData(100, -10, 120, 0)] // Invalid (negative) world height, positive out of bounds x but skip wraparound
            [InlineData(100, -10, 10, -10)] // Invalid (negative) world height, negative out of bounds y but skip wraparound
            [InlineData(100, -10, 10, 110)] // Invalid (negative) world height, positive out of bounds y but skip wraparound
            [InlineData(-10, 100, 0, 10)] // Invalid (negative) world width, no wraparound
            [InlineData(-10, 100, -20, 10)] // Invalid (negative) world width, negative out of bounds x but skip wraparound
            [InlineData(-10, 100, 120, 10)] // Invalid (negative) world width, positive out of bounds x but skip wraparound
            [InlineData(-10, 100, 0, -10)] // Invalid (negative) world width, negative out of bounds y but skip wraparound
            [InlineData(-10, 100, 0, 110)] // Invalid (negative) world width, positive out of bounds y but skip wraparound
            [InlineData(-10, -10, 0, 0)] // Invalid (negative) world width and height, no wraparound
            [InlineData(-10, -10, -10, 0)] // Invalid (negative) world width and height, negative out of bounds x but skip wraparound
            [InlineData(-10, -10, 10, 0)] // Invalid (negative) world width and height, positive out of bounds x but skip wraparound
            [InlineData(-10, -10, 0, -10)] // Invalid (negative) world width and height, negative out of bounds y but skip wraparound
            [InlineData(-10, -10, 0, 10)] // Invalid (negative) world width and height, positive out of bounds y but skip wraparound
            public void Should_Skip_When_WorldParametersInvalid(int worldWidth, int worldHeight, int x, int y)
            {
                // Arrange
                var positionComponent = new PositionComponent { X = x, Y = y };
                // Act
                PositionUtility.ApplyWraparound(positionComponent, worldWidth, worldHeight);
                // Assert
                Assert.Equal(x, positionComponent.X);
                Assert.Equal(y, positionComponent.Y);
            }
        }
    }
}

