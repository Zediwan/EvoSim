using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Systems;

namespace EvoSim.Test.ECS.Systems;

public class HealthSystemTest
{
    public class ConstructorTests
    {
        public class CoreTests
        {
            [Fact]
            public void Should_InitializeWithCorrectValues()
            {
                // Act
                var healthSystem = new HealthSystem();
                // Assert
                Assert.NotNull(healthSystem);
            }
        }

        public class DebugTests : DebugTest
        {
            // Empty for future extension
        }

        public class ReleaseTests : ReleaseTest
        {
            // Empty for future extension
        }
    }

    public class UpdateTests
    {
        public class CoreTests
        {
            [Fact]
            public void Should_NotChangeHealth_When_Updating()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
                entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
                var healthSystem = new HealthSystem();
                // Act
                healthSystem.Update(ecsEngine, 1.0f);
                // Assert
                var healthComponent = entity.GetComponent<HealthComponent>();
                Assert.Equal(50, healthComponent.Health);
            }

            [Fact]
            public void Should_RemoveEntity_When_HealthIsZero()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                entity.AddComponent(new HealthComponent(maxHealth: 100, health: 0));
                entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
                var healthSystem = new HealthSystem();
                // Act
                healthSystem.Update(ecsEngine, 1.0f);
                // Assert
                Assert.DoesNotContain(entity, ecsEngine.GetEntitiesWith<HealthComponent>());
            }

            [Fact]
            public void Should_NotRemoveEntity_When_HealthIsPositive()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
                entity.AddComponent(new EnergyComponent(maxEnergy: 100, energy: 50));
                var healthSystem = new HealthSystem();
                // Act
                healthSystem.Update(ecsEngine, 1.0f);
                // Assert
                Assert.Contains(entity, ecsEngine.GetEntitiesWith<HealthComponent>());
            }
        }

        public class DebugTests : DebugTest
        {
            // Empty for future extension
        }

        public class ReleaseTests : ReleaseTest
        {
            // Empty for future extension
        }
    }
}
