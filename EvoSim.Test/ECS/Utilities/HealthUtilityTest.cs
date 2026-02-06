using EvoSim.ECS.Components;
using EvoSim.ECS.Core;
using EvoSim.ECS.Utilities;

namespace EvoSim.Test.ECS.Utilities;

public class HealthUtilityTest
{
    public class TakeDamageTest()
    {
        [Fact]
        public void Should_TakeDamage_When_EntityHasAllComponents()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            const float damageToTake = 10;
            // Act
            HealthUtility.TakeDamage(entity, damageToTake);
            // Assert
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(40, healthComponent.Health);
        }

        [Fact]
        public void Should_TakeDamage_When_ValidParameters()
        {
            // Arrange
            var health = new HealthComponent(maxHealth: 100, health: 50);
            const float damageToTake = 10;
            // Act
            HealthUtility.TakeDamage(health, damageToTake);
            // Assert
            Assert.Equal(40, health.Health);
        }

        [Fact]
        public void Should_NotTakeDamage_When_AmountIsZero()
        {
            // Arrange
            var health = new HealthComponent(maxHealth: 100, health: 50);
            const float damageToTake = 0;
            // Act
            HealthUtility.TakeDamage(health, damageToTake);
            // Assert
            Assert.Equal(50, health.Health);
        }

        [Fact]
        public void Should_SetHealthToZero_When_DamageExceedsCurrentHealth()
        {
            // Arrange
            var health = new HealthComponent(maxHealth: 100, health: 50);
            const float damageToTake = 60;
            // Act
            HealthUtility.TakeDamage(health, damageToTake);
            // Assert
            Assert.Equal(0, health.Health);
        }

        public class ReleaseTests() : ReleaseTest
        {
            [SkippableFact]
            public void Should_NotTakeDamage_When_AmountIsNegative()
            {
                // Arrange
                var health = new HealthComponent(maxHealth: 100, health: 50);
                const float damageToTake = -10;
                // Act
                HealthUtility.TakeDamage(health, damageToTake);
                // Assert
                Assert.Equal(50, health.Health);
            }

            [SkippableFact]
            public void Should_NotTakeDamage_When_EntityLacksHealthComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                const float damageToTake = 10;
                // Act
                HealthUtility.TakeDamage(entity, damageToTake);
                // Assert
                Assert.False(entity.HasComponent<HealthComponent>());
            }

            [SkippableFact]
            public void Should_NotTakeDamage_When_HealthComponentIsDead()
            {
                // Arrange
                var health = new HealthComponent(maxHealth: 100, health: 0);
                const float damageToTake = 10;
                // Act
                HealthUtility.TakeDamage(health, damageToTake);
                // Assert
                Assert.Equal(0, health.Health);
            }
        }
    }

    public class HealTest()
    {
        [Fact]
        public void Should_Heal_When_ValidParameters()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            const float amountToHeal = 20;
            // Act
            HealthUtility.Heal(entity, amountToHeal);
            // Assert
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(70, healthComponent.Health);
        }

        [Fact]
        public void Should_NotHeal_When_AmountIsZero()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            const float amountToHeal = 0;
            // Act
            HealthUtility.Heal(entity, amountToHeal);
            // Assert
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(50, healthComponent.Health);
        }

        [Fact]
        public void Should_NotExceedMaxHealth_When_Healing()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 90));
            const float amountToHeal = 20;
            // Act
            HealthUtility.Heal(entity, amountToHeal);
            // Assert
            var healthComponent = entity.GetComponent<HealthComponent>();
            Assert.Equal(100, healthComponent.Health);
        }

        public class ReleaseTests() : ReleaseTest
        {
            [SkippableFact]
            public void Should_NotHeal_When_AmountIsNegative()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
                const float amountToHeal = -10;
                // Act
                HealthUtility.Heal(entity, amountToHeal);
                // Assert
                var healthComponent = entity.GetComponent<HealthComponent>();
                Assert.Equal(50, healthComponent.Health);
            }

            [SkippableFact]
            public void Should_NotHeal_When_EntityLacksHealthComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                const float amountToHeal = 10;
                // Act
                HealthUtility.Heal(entity, amountToHeal);
                // Assert
                Assert.False(entity.HasComponent<HealthComponent>());
            }
        }
    }

    public class IsDeadTest()
    {
        [Fact]
        public void Should_ReturnTrue_When_HealthIsZero()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 0));
            // Act
            var isDead = !entity.GetComponent<HealthComponent>().IsAlive;
            // Assert
            Assert.True(isDead);
        }
        [Fact]
        public void Should_ReturnFalse_When_HealthIsAboveZero()
        {
            // Arrange
            var ecsEngine = new EcsEngine();
            var entity = ecsEngine.CreateEntity();
            entity.AddComponent(new HealthComponent(maxHealth: 100, health: 50));
            // Act
            var isDead = !entity.GetComponent<HealthComponent>().IsAlive;
            // Assert
            Assert.False(isDead);
        }

        public class ReleaseTests : ReleaseTest
        {
            [SkippableFact]
            public void Should_ReturnFalse_When_EntityLacksHealthComponent()
            {
                // Arrange
                var ecsEngine = new EcsEngine();
                var entity = ecsEngine.CreateEntity();
                // Act & Assert
                Assert.True(HealthUtility.IsDead(entity));
            }
        }
    }
}
