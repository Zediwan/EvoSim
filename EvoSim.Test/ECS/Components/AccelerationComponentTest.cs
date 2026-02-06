using EvoSim.ECS.Components;

namespace EvoSim.Test.ECS.Components;

public class AccelerationComponentTest
{
    public class MaxAccelerationTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        public void Should_SetCorrectly_When_SettingPositiveMaxAcceleration(float maxAcceleration)
        {
            // Arrange
            var component = new AccelerationComponent
            {
                // Act
                MaxAcceleration = maxAcceleration
            };
            // Assert
            Assert.Equal(maxAcceleration, component.MaxAcceleration);
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableFact]
            public void Should_ClampToZero_When_SettingNegativeMaxAcceleration()
            {
                // Arrange
                var component = new AccelerationComponent
                {
                    // Act
                    MaxAcceleration = -10
                };
                // Assert
                Assert.Equal(0, component.MaxAcceleration);
            }
        }
    }

    public class TotalAccelerationTests
    {
        [Theory]
        [InlineData(3, 4, 5)] // 3-4-5 triangle
        [InlineData(5, 12, 13)] // 5-12-13 triangle
        [InlineData(8, 15, 17)] // 8-15-17 triangle
        [InlineData(0, 0, 0)] // Zero acceleration
        public void Should_CalculateTotalAccelerationCorrectly(float ax, float ay, float expectedTotalAcceleration)
        {
            // Arrange
            var component = new AccelerationComponent { AX = ax, AY = ay };
            // Act
            var totalAcceleration = component.TotalAcceleration;
            // Assert
            Assert.Equal(expectedTotalAcceleration, totalAcceleration, 3); // Allowing a small margin of error for floating point calculations
        }
    }

    public class TotalAccelerationSquaredTests
    {
        [Theory]
        [InlineData(3, 4, 25)] // 3-4-5 triangle
        [InlineData(5, 12, 169)] // 5-12-13 triangle
        [InlineData(8, 15, 289)] // 8-15-17 triangle
        [InlineData(0, 0, 0)] // Zero acceleration
        public void Should_CalculateTotalAccelerationSquaredCorrectly(float ax, float ay, float expectedTotalAccelerationSquared)
        {
            // Arrange
            var component = new AccelerationComponent { AX = ax, AY = ay };
            // Act
            var totalAccelerationSquared = component.TotalAccelerationSquared;
            // Assert
            Assert.Equal(expectedTotalAccelerationSquared, totalAccelerationSquared, 3); // Allowing a small margin of error for floating point calculations
        }
    }
}
