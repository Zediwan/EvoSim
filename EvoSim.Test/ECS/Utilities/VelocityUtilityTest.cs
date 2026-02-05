using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Entities;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class VelocityUtilityTest
{
    public class ApplyVelocityToPositionTests
    {
        [Fact]
        public void Should_Apply_When_EntityHasAllComponents()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            var position = new PositionComponent { X = 10, Y = 20 };
            var velocity = new VelocityComponent { VX = 5.5f, VY = -3.2f };
            entity.AddComponent(position);
            entity.AddComponent(velocity);
            var expectedX = position.X + (int)velocity.VX; // 10 + 5 = 15
            var expectedY = position.Y + (int)velocity.VY; // 20 - 3 = 17
            // Act
            VelocityUtility.ApplyVelocityToPosition(entity);
            // Assert
            Assert.Equal(expectedX, position.X);
            Assert.Equal(expectedY, position.Y);
        }

        [Fact]
        public void Should_NotApply_When_VelocityIsZero()
        {
            // Arrange
            var position = new PositionComponent { X = 10, Y = 20 };
            var velocity = new VelocityComponent { VX = 0, VY = 0 };
            var expectedX = position.X; // 10
            var expectedY = position.Y; // 20
            // Act
            VelocityUtility.ApplyVelocityToPosition(position, velocity);
            // Assert
            Assert.Equal(expectedX, position.X);
            Assert.Equal(expectedY, position.Y);
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableFact]
            public void Should_NotApply_When_EntityMissingVelocityComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                var position = new PositionComponent { X = 10, Y = 20 };
                entity.AddComponent(position);
                var expectedX = position.X; // 10
                var expectedY = position.Y; // 20
                // Act
                VelocityUtility.ApplyVelocityToPosition(entity);
                // Assert
                Assert.Equal(expectedX, position.X);
                Assert.Equal(expectedY, position.Y);
            }

            [SkippableFact]
            public void Should_NotApply_When_EntityMissingPositionComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                var velocity = new VelocityComponent { VX = 5.5f, VY = -3.2f };
                entity.AddComponent(velocity);
                // Act & Assert
                VelocityUtility.ApplyVelocityToPosition(entity);
            }

            [SkippableFact]
            public void Should_NotApply_When_EntityMissingBothComponents()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                // Act & Assert
                VelocityUtility.ApplyVelocityToPosition(entity);
            }
        }
    }
}
