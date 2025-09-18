using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class VelocityComponentTest
{
    public class MaxVelocityTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void Should_SetCorrectly_When_SettingPositiveMaxVelocity(float velocity)
        {
            // Arrange
            var velocityComponent = new VelocityComponent
            {
                // Act
                MaxVelocity = velocity
            };
            // Assert
            Assert.Equal(velocity, velocityComponent.MaxVelocity);
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableFact]
            public void Should_ClampToZero_When_SettingNegativeMaxVelocity()
            {
                // Arrange
                var velocityComponent = new VelocityComponent
                {
                    // Act
                    MaxVelocity = -10
                };
                // Assert
                Assert.Equal(0, velocityComponent.MaxVelocity);
            }
        }
    }

    public class TotalVelocityTests
    {
        [Theory]
        [InlineData(3, 4, 5)] // 3-4-5 triangle
        [InlineData(5, 12, 13)] // 5-12-13 triangle
        [InlineData(8, 15, 17)] // 8-15-17 triangle
        [InlineData(0, 0, 0)] // Zero velocity
        public void Should_CalculateTotalVelocityCorrectly(float dx, float dy, float expectedTotalVelocity)
        {
            // Arrange
            var velocityComponent = new VelocityComponent { DX = dx, DY = dy };
            // Act
            var totalVelocity = velocityComponent.TotalVelocity;
            // Assert
            Assert.Equal(expectedTotalVelocity, totalVelocity, 3); // Allowing a small margin of error for floating point calculations
        }
    }

    public class TotalVelocitySquaredTests
    {
        [Theory]
        [InlineData(3, 4, 25)] // 3-4-5 triangle
        [InlineData(5, 12, 169)] // 5-12-13 triangle
        [InlineData(8, 15, 289)] // 8-15-17 triangle
        [InlineData(0, 0, 0)] // Zero velocity
        public void Should_CalculateTotalVelocitySquaredCorrectly(float dx, float dy, float expectedTotalVelocitySquared)
        {
            // Arrange
            var velocityComponent = new VelocityComponent { DX = dx, DY = dy };
            // Act
            var totalVelocitySquared = velocityComponent.TotalVelocitySquared;
            // Assert
            Assert.Equal(expectedTotalVelocitySquared, totalVelocitySquared);
        }
    }
}
